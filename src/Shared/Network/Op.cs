#region copyright
// Copyright (c) 2018 Project Agonyl
#endregion

using System.Collections.Generic;
using System.Reflection;

namespace Agonyl.Shared.Network
{
	public static class Op
	{
		public const int C2L_LOGIN = 0xE0; // Size 56
		public const int C2L_SERVER_DETAILS = 0xE1; // Size 11
		public const int L2C_MESSAGE = 0xE2; // Size 92
		public const int L2C_SERVER_LIST = 0xE3;  // Size 111
		public const int L2C_LOGIN_OK = 0xE4;  // Size 10
		public const int L2C_SERVER_DETAILS = 0xE5;  // Size 34

		private static readonly Dictionary<int, int> _sizes = new Dictionary<int, int>();
		private static readonly Dictionary<int, string> _names = new Dictionary<int, string>();

		static Op()
		{
			_sizes[C2L_LOGIN] = 56;
			_sizes[C2L_SERVER_DETAILS] = 11;

			foreach (var field in typeof(Op).GetFields(BindingFlags.Public | BindingFlags.Static))
				_names[(int)field.GetValue(null)] = field.Name;
		}

		public static int GetSize(int op)
		{
			if (!_sizes.TryGetValue(op, out var size))
				return -1;
			return size;
		}

		public static string GetName(int op)
		{
			if (!_names.TryGetValue(op, out var name))
				return "?";
			return name;
		}
	}
}
