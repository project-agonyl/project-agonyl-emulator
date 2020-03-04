#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using Agonyl.Shared.Util;

namespace Agonyl.Game
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			try
			{
				GameServer.Instance.Run();
			}
			catch (Exception ex)
			{
				Log.Error("Error on startup: {0}, {1}", ex.GetType().Name, ex.Message);
				CliUtil.Exit(1, true);
			}
		}
	}
}