using System;
using Agonyl.Shared.Util;

namespace Agonyl.Login
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				LoginServer.Instance.Run();
			}
			catch (Exception ex)
			{
				Log.Error("Error on startup: {0}, {1}", ex.GetType().Name, ex.Message);
				CliUtil.Exit(1, true);
			}
		}
	}
}
