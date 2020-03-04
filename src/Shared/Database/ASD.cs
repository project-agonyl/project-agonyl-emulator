#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Agonyl.Shared.Database
{
    public class ASD : Base
    {
        /// <summary>
        /// Returns true if accounts exists.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool AccountExists(string username)
        {
            using (var conn = this.GetConnection())
            using (var mc = new MySqlCommand("SELECT `c_id` FROM `account` WHERE `c_id` = @username", conn))
            {
                mc.Parameters.AddWithValue("@username", username);

                using (var reader = mc.ExecuteReader())
                    return reader.HasRows;
            }
        }

        /// <summary>
        /// Returns true if accounts exists.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool AccountExists(string username, string password)
        {
            using (var conn = this.GetConnection())
            using (var mc = new MySqlCommand("SELECT `c_id` FROM `account` WHERE `c_id` = @username AND `c_headera` = @password", conn))
            {
                mc.Parameters.AddWithValue("@username", username);
                mc.Parameters.AddWithValue("@password", password);

                using (var reader = mc.ExecuteReader())
                    return reader.HasRows;
            }
        }

        public int AccountIdByUsername(string username)
        {
            using (var conn = this.GetConnection())
            using (var mc = new MySqlCommand("SELECT `c_id` FROM `account` WHERE `c_id` = @username", conn))
            {
                mc.Parameters.AddWithValue("@username", username);

                using (var reader = mc.ExecuteReader())
                    if (reader.Read())
                        return Convert.ToInt32(reader["c_id"].ToString());
                return 0;
            }
        }

        public IEnumerable<Model.Charac0> GetCharacterList(string username)
        {
            using (var conn = this.GetConnection())
            {
                using (var mc = new MySqlCommand("SELECT * FROM `charac0` WHERE `c_sheadera` = @id AND `c_status` = 'A'", conn))
                {
                    mc.Parameters.AddWithValue("@id", username);
                    using (var reader = mc.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Model.Charac0
                            {
                                c_id = reader["c_id"].ToString(),
                                c_sheadera = reader["c_sheadera"].ToString(),
                                c_sheaderb = reader["c_sheaderb"].ToString(),
                                c_sheaderc = reader["c_sheaderc"].ToString(),
                                c_headera = reader["c_headera"].ToString(),
                                c_headerb = reader["c_headerb"].ToString(),
                                c_headerc = reader["c_headerc"].ToString(),
                                m_body = reader["m_body"].ToString(),
                            };
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates new account with given information.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if name or password is empty.</exception>
        public bool CreateAccount(string name, string password)
        {
            // 			if (string.IsNullOrWhiteSpace(name))
            // 				throw new ArgumentNullException("name");
            //
            // 			if (string.IsNullOrWhiteSpace(password))
            // 				throw new ArgumentNullException("password");
            //
            // 			// Wrap password in BCrypt
            // 			password = BCrypt.HashPassword(password, BCrypt.GenerateSalt());
            //
            // 			using (var conn = this.GetConnection())
            // 			using (var cmd = new InsertCommand("INSERT INTO `accounts` {0}", conn))
            // 			{
            // 				cmd.Set("name", name);
            // 				cmd.Set("password", password);
            //
            // 				try
            // 				{
            // 					cmd.Execute();
            // 					return true;
            // 				}
            // 				catch (Exception ex)
            // 				{
            // 					Log.Exception(ex, "Failed to create account '{0}'.", name);
            // 				}
            // 			}

            return false;
        }

        /// <summary>
        /// Returns true if a character with the given name exists on account.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CharacterExists(long accountId, string name)
        {
            using (var conn = this.GetConnection())
            using (var mc = new MySqlCommand("SELECT `characterId` FROM `characters` WHERE `accountId` = @accountId AND `name` = @name", conn))
            {
                mc.Parameters.AddWithValue("@accountId", accountId);
                mc.Parameters.AddWithValue("@name", name);

                using (var reader = mc.ExecuteReader())
                    return reader.HasRows;
            }
        }

        /// <summary>
        /// Returns true if team name exists.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool TeamNameExists(string teamName)
        {
            using (var conn = this.GetConnection())
            using (var mc = new MySqlCommand("SELECT `accountId` FROM `accounts` WHERE `teamName` = @teamName", conn))
            {
                mc.Parameters.AddWithValue("@teamName", teamName);

                using (var reader = mc.ExecuteReader())
                    return reader.HasRows;
            }
        }

        /// <summary>
        /// Changes team name for account.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool UpdateTeamName(long accountId, string teamName)
        {
            using (var conn = this.GetConnection())
            using (var cmd = new UpdateCommand("UPDATE `accounts` SET {0} WHERE `accountId` = @accountId", conn))
            {
                cmd.AddParameter("@accountId", accountId);
                cmd.Set("teamName", teamName);

                return cmd.Execute() > 0;
            }
        }
    }
}
