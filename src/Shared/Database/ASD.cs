#region copyright

// Copyright (c) 2020 Project Agonyl

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
        public bool AccountExists(string username)
        {
            using (var conn = this.GetConnection())
            using (var mc = new MySqlCommand("SELECT `c_id` FROM `account` WHERE `c_id` = @username", conn))
            {
                mc.Parameters.AddWithValue("@username", username);

                using (var reader = mc.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        /// <summary>
        /// Returns true if accounts exists.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public bool AccountExists(string username, string password)
        {
            using (var conn = this.GetConnection())
            using (var mc = new MySqlCommand("SELECT `c_id` FROM `account` WHERE `c_id` = @username AND `c_headera` = @password", conn))
            {
                mc.Parameters.AddWithValue("@username", username);
                mc.Parameters.AddWithValue("@password", password);

                using (var reader = mc.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        public int AccountIdByUsername(string username)
        {
            using (var conn = this.GetConnection())
            using (var mc = new MySqlCommand("SELECT `c_id` FROM `account` WHERE `c_id` = @username", conn))
            {
                mc.Parameters.AddWithValue("@username", username);

                using (var reader = mc.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["c_id"].ToString());
                    }
                }

                return 0;
            }
        }

        /// <summary>
        /// Returns all characters of an account.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="username"></param>
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
        /// Returns true if a character with the given name exists on account.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="name"></param>
        public bool CharacterExists(string username, string name)
        {
            using (var conn = this.GetConnection())
            {
                using (var mc = new MySqlCommand("SELECT LOWER(`c_id`) FROM `charac0` WHERE `c_sheadera` = @accountId AND `c_id` = LOWER(@name)", conn))
                {
                    mc.Parameters.AddWithValue("@accountId", username);
                    mc.Parameters.AddWithValue("@name", name);

                    using (var reader = mc.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if a character with the given name exists.
        /// </summary>
        /// <param name="name"></param>
        public bool CharacterExists(string name)
        {
            using (var conn = this.GetConnection())
            {
                using (var mc = new MySqlCommand("SELECT * FROM `charac0` WHERE `c_id` = @name", conn))
                {
                    mc.Parameters.AddWithValue("@name", name);

                    using (var reader = mc.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if a character with the given name exists.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Model.Charac0 GetCharacterModel(string name)
        {
            using (var conn = this.GetConnection())
            {
                using (var mc = new MySqlCommand("SELECT * FROM `charac0` WHERE `c_id` = @name", conn))
                {
                    mc.Parameters.AddWithValue("@name", name);

                    using (var reader = mc.ExecuteReader())
                    {
                        reader.Read();
                        return new Model.Charac0
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

        /// <summary>
        /// Returns character count of an account.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="username"></param>
        public long CharacterCount(string username)
        {
            using (var conn = this.GetConnection())
            {
                using (var mc = new MySqlCommand("SELECT COUNT(*) FROM `charac0` WHERE `c_sheadera` = @name AND `c_status` = 'A'", conn))
                {
                    mc.Parameters.AddWithValue("@name", username);
                    return (long)mc.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Creates a character.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="stats"></param>
        /// <param name="body"></param>
        /// <param name="location"></param>
        /// <param name="level"></param>
        /// <param name="town"></param>
        public bool CreateCharacter(string username, string name, byte type, string stats, string body, string location, int level)
        {
            using (var conn = this.GetConnection())
            {
                using (var mc = new MySqlCommand("INSERT INTO `charac0`(`c_id`, `c_sheadera`, `c_sheaderb`, `c_sheaderc`, `c_headera`, `c_headerb`, `c_headerc`, `d_cdate`, `d_udate`, `c_status`, `m_body`) VALUES(@name, @username, @type, @level, @stats, @location, '0', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 'A', @body)", conn))
                {
                    mc.Parameters.AddWithValue("@name", name);
                    mc.Parameters.AddWithValue("@username", username);
                    mc.Parameters.AddWithValue("@type", type.ToString());
                    mc.Parameters.AddWithValue("@level", level.ToString());
                    mc.Parameters.AddWithValue("@stats", stats);
                    mc.Parameters.AddWithValue("@location", location);
                    mc.Parameters.AddWithValue("@body", body);
                    return mc.ExecuteNonQuery() != 0;
                }
            }
        }

        /// <summary>
        /// Deletes a character.
        /// </summary>
        /// <param name="name"></param>
        public bool DeleteCharacter(string name)
        {
            using (var conn = this.GetConnection())
            {
                using (var mc = new MySqlCommand("UPDATE `charac0` SET `c_status` = 'X' WHERE `c_id` = @name", conn))
                {
                    mc.Parameters.AddWithValue("@name", name);
                    return mc.ExecuteNonQuery() != 0;
                }
            }
        }
    }
}
