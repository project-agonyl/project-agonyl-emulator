#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

namespace Agonyl.Game.Data
{
    public class Account
    {
        /// <summary>
        /// Account's username.
        /// </summary>
        public string Username { get; set; }

        public Account()
        {
        }

        public Account(string username)
        {
            this.Username = username;
        }
    }
}
