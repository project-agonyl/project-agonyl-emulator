#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using MySql.Data.MySqlClient;

namespace Agonyl.Shared.Database
{
    /// <summary>
    /// Extensions for the MySqlDataReader.
    /// </summary>
    public static class MySqlDataReaderExtension
    {
        /// <summary>
        /// Returns true if value at index is null.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        private static bool IsDBNull(this MySqlDataReader reader, string index)
        {
            return reader.IsDBNull(reader.GetOrdinal(index));
        }

        /// <summary>
        /// Same as GetString, except for a is null check. Returns null if NULL.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        public static string GetStringSafe(this MySqlDataReader reader, string index)
        {
            if (IsDBNull(reader, index))
            {
                return null;
            }
            else
            {
                return reader.GetString(index);
            }
        }

        /// <summary>
        /// Returns DateTime of the index, or DateTime.MinValue, if value is null.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        public static DateTime GetDateTimeSafe(this MySqlDataReader reader, string index)
        {
            return reader[index] as DateTime? ?? DateTime.MinValue;
        }
    }
}
