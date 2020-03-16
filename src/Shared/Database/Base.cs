#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using MySql.Data.MySqlClient;

namespace Agonyl.Shared.Database
{
    public abstract class Base
    {
        protected string _connectionString;
        protected string _dbName;

        /// <summary>
        /// Sets connection string and calls TestConnection.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="db"></param>
        /// <exception cref="Exception">Thrown if connection couldn't be established.</exception>
        public void Init(string host, string user, string pass, string db)
        {
            this._connectionString = string.Format("server={0}; database={1}; uid={2}; password={3}; charset=utf8; pooling=true; min pool size=0; max pool size=100;", host, db, user, pass);
            this._dbName = db;
            this.TestConnection();
        }

        /// <summary>
        /// Tests connection.
        /// </summary>
        /// <exception cref="Exception">Thrown if connection couldn't be established.</exception>
        public void TestConnection()
        {
            MySqlConnection conn = null;
            try
            {
                conn = this.GetConnection();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Return the current database name.
        /// </summary>
        public string GetDbName()
        {
            return this._dbName;
        }

        /// <summary>
        /// Returns a valid connection.
        /// </summary>
        protected MySqlConnection GetConnection()
        {
            if (this._connectionString == null)
            {
                throw new Exception(this._dbName + " database connection has not been initialized.");
            }

            var result = new MySqlConnection(this._connectionString);
            result.Open();
            return result;
        }
    }
}
