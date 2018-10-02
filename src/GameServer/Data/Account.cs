#region copyright
// Copyright (c) 2018 Project Agonyl
#endregion

namespace Agonyl.Game.Data
{
	public class Account
	{
		/// <summary>
		/// Account's username.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Account's ID.
		/// </summary>
		public int Id { get; set; }

		public Account()
		{
		}

		public Account(int id, string username)
		{
			this.Username = username;
			this.Id = id;
		}
	}
}
