﻿#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Agonyl.Shared.Database
{
    /// <summary>
    /// Base class for simplified MySQL commands.
    /// </summary>
    public abstract class SimpleCommand : IDisposable
    {
        protected MySqlCommand _mc;
        protected Dictionary<string, object> _set;

        /// <summary>
        /// Initializes internal objects.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        protected SimpleCommand(string command, MySqlConnection conn, MySqlTransaction trans = null)
        {
            this._mc = new MySqlCommand(command, conn, trans);
            this._set = new Dictionary<string, object>();
        }

        /// <summary>
        /// Adds a parameter that's not handled by Set.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddParameter(string name, object value)
        {
            this._mc.Parameters.AddWithValue(name, value);
        }

        /// <summary>
        /// Sets value for field.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public void Set(string field, object value)
        {
            this._set[field] = value;
        }

        /// <summary>
        /// Executes command.
        /// </summary>
        public abstract int Execute();

        /// <summary>
        /// Disposes internal, wrapped objects.
        /// </summary>
        public void Dispose()
        {
            this._mc.Dispose();
        }
    }

    /// <summary>
    /// Wrapper around MySqlCommand, for easier, cleaner updating.
    /// </summary>
    /// <remarks>
    /// Include one {0} where the set statements normally would be.
    /// It's automatically inserted, based on what was passed to "Set".
    /// </remarks>
    /// <example>
    /// <code>
    /// using (var conn = AuraDb.Instance.Connection)
    /// using (var cmd = new UpdateCommand("UPDATE `accounts` SET {0} WHERE `accountId` = @accountId", conn))
    /// {
    /// 	cmd.AddParameter("@accountId", account.Id);
    /// 	cmd.Set("authority", (byte)account.Authority);
    /// 	cmd.Set("lastlogin", account.LastLogin);
    /// 	cmd.Set("banReason", account.BanReason);
    /// 	cmd.Set("banExpiration", account.BanExpiration);
    ///
    /// 	cmd.Execute();
    /// }
    /// </code>
    /// </example>
    public class UpdateCommand : SimpleCommand
    {
        /// <summary>
        /// Creates new update command.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        public UpdateCommand(string command, MySqlConnection conn, MySqlTransaction trans = null)
            : base(command, conn, trans)
        {
        }

        /// <summary>
        /// Runs MySqlCommand.ExecuteNonQuery.
        /// </summary>
        public override int Execute()
        {
            // Build setting part
            var sb = new StringBuilder();
            foreach (var parameter in this._set.Keys)
            {
                sb.AppendFormat("`{0}` = @{0}, ", parameter);
            }

            // Add setting part
            this._mc.CommandText = string.Format(this._mc.CommandText, sb.ToString().Trim(' ', ','));

            // Add parameters
            foreach (var parameter in this._set)
            {
                this._mc.Parameters.AddWithValue("@" + parameter.Key, parameter.Value);
            }

            // Run
            return this._mc.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Wrapper around MySqlCommand, for easier, cleaner inserting.
    /// </summary>
    /// <remarks>
    /// Include one {0} where the "(...) VALUES (...) part normally would be.
    /// It's automatically inserted, based on what was passed to "Set".
    /// </remarks>
    /// <example>
    /// <code>
    /// using (var cmd = new InsertCommand("INSERT INTO `keywords` {0}", conn, transaction))
    /// {
    /// 	cmd.Set("creatureId", creature.CreatureId);
    /// 	cmd.Set("keywordId", keywordId);
    ///
    /// 	cmd.Execute();
    /// }
    /// </code>
    /// </example>
    public class InsertCommand : SimpleCommand
    {
        /// <summary>
        /// Returns last insert id.
        /// </summary>
        public long LastId { get { return this._mc.LastInsertedId; } }

        /// <summary>
        /// Creates new insert command.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="conn"></param>
        /// <param name="transaction"></param>
        public InsertCommand(string command, MySqlConnection conn, MySqlTransaction transaction = null)
            : base(command, conn, transaction)
        {
        }

        /// <summary>
        /// Runs MySqlCommand.ExecuteNonQuery.
        /// </summary>
        public override int Execute()
        {
            // Build values part
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            foreach (var parameter in this._set.Keys)
            {
                sb1.AppendFormat("`{0}`, ", parameter);
                sb2.AppendFormat("@{0}, ", parameter);
            }

            // Add values part
            var values = "(" + (sb1.ToString().Trim(' ', ',')) + ") VALUES (" + (sb2.ToString().Trim(' ', ',')) + ")";
            this._mc.CommandText = string.Format(this._mc.CommandText, values);

            // Add parameters
            foreach (var parameter in this._set)
            {
                this._mc.Parameters.AddWithValue("@" + parameter.Key, parameter.Value);
            }

            // Run
            return this._mc.ExecuteNonQuery();
        }
    }
}
