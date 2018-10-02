#region copyright
// Copyright (c) 2018 Project Agonyl
#endregion

using System;
using MySql.Data.MySqlClient;

namespace Agonyl.Shared.Database
{
	public class AgonylDb
	{
		private string _connectionString;

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
			_connectionString = string.Format("server={0}; database={1}; uid={2}; password={3}; charset=utf8; pooling=true; min pool size=0; max pool size=100;", host, db, user, pass);
			this.TestConnection();
		}

		/// <summary>
		/// Returns a valid connection.
		/// </summary>
		protected MySqlConnection GetConnection()
		{
			if (_connectionString == null)
				throw new Exception("Database connection has not been initialized.");

			var result = new MySqlConnection(_connectionString);
			result.Open();
			return result;
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
					conn.Close();
			}
		}

		/// <summary>
		/// Returns true if accounts exists.
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		public bool AccountExists(string username)
		{
			using (var conn = this.GetConnection())
			using (var mc = new MySqlCommand("SELECT `username` FROM `account` WHERE `username` = @username", conn))
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
			using (var mc = new MySqlCommand("SELECT `username` FROM `account` WHERE `username` = @username AND `password` = @password", conn))
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
			using (var mc = new MySqlCommand("SELECT `id` FROM `account` WHERE `username` = @username", conn))
			{
				mc.Parameters.AddWithValue("@username", username);

				using (var reader = mc.ExecuteReader())
					if (reader.Read())
						return Convert.ToInt32(reader["id"].ToString());
				return 0;
			}
		}

		public MySqlDataReader GetCharacterList(int id)
		{
			using (var conn = this.GetConnection())
			using (var mc = new MySqlCommand("SELECT * FROM `character` WHERE `account_id` = @id", conn))
			{
				mc.Parameters.AddWithValue("@id", id);
				using (var reader = mc.ExecuteReader())
					return reader;
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
