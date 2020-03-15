#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System.Collections.Generic;
using Agonyl.Shared.Util;
using Agonyl.Shared.Util.Config;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Agonyl.Shared.Database
{
    public class Redis
    {
        private string _connectionString;

        /// <summary>
        /// Redis server connection
        /// </summary>
        public ConnectionMultiplexer Connection { get; protected set; }

        /// <summary>
        /// Redis database
        /// </summary>
        public IDatabase Db { get; protected set; }

        public Redis(string host, int port, string password = "")
        {
            if (password.Length != 0)
            {
                this._connectionString = string.Format("{0}:{1},password={2},defaultDatabase=0", host, port, password);
            }
            else
            {
                this._connectionString = string.Format("{0}:{1},defaultDatabase=0", host, port);
            }

            this.Initialize();
        }

        /// <summary>
        /// Initializes connection.
        /// </summary>
        /// <exception cref="Exception">Thrown if connection couldn't be established.</exception>
        public void Initialize()
        {
            Log.Info("Initializing redis connection...");
            this.Connection = ConnectionMultiplexer.Connect(this._connectionString);
            this.Db = this.Connection.GetDatabase();
        }

        /// <summary>
        /// Resets logged in account list
        /// </summary>
        public void ResetLoggedInAccountList()
        {
            this.Db.StringSet(Constants.KEY_LOGGED_IN_ACCOUNTS, JsonConvert.SerializeObject(new List<string>()));
        }

        /// <summary>
        /// Returns whether username exists in the logged in account list
        /// </summary>
        /// <param name="username"></param>
        public bool IsLoggedIn(string username)
        {
            var accountList = JsonConvert.DeserializeObject<List<string>>(this.Db.StringGet(Constants.KEY_LOGGED_IN_ACCOUNTS));
            return accountList.Contains(username);
        }

        /// <summary>
        /// Adds username to logged in account list
        /// </summary>
        /// <param name="username"></param>
        public void AddLoggedInAccount(string username)
        {
            var accountList = JsonConvert.DeserializeObject<List<string>>(this.Db.StringGet(Constants.KEY_LOGGED_IN_ACCOUNTS));
            if (!accountList.Contains(username))
            {
                accountList.Add(username);
            }

            this.Db.StringSet(Constants.KEY_LOGGED_IN_ACCOUNTS, JsonConvert.SerializeObject(accountList));
        }

        /// <summary>
        /// Removes username to logged in account list
        /// </summary>
        /// <param name="username"></param>
        public void RemoveLoggedInAccount(string username)
        {
            var accountList = JsonConvert.DeserializeObject<List<string>>(this.Db.StringGet(Constants.KEY_LOGGED_IN_ACCOUNTS));
            if (accountList.Contains(username))
            {
                accountList.Remove(username);
            }

            this.Db.StringSet(Constants.KEY_LOGGED_IN_ACCOUNTS, JsonConvert.SerializeObject(accountList));
        }
    }
}
