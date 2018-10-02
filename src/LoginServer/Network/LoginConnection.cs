#region copyright
// Copyright (c) 2018 Project Agonyl
#endregion

using Agonyl.Shared.Network;

namespace Agonyl.Login.Network
{
	public class LoginConnection : Connection
	{
		/// <summary>
		/// Username of the current connection.
		/// </summary>
		public string Username { get; set; }

		public LoginConnection()
		{
			this.ShouldDecrypt = false;
		}

		public override void Send(Packet packet)
		{
			if (_socket == null || this.State == ConnectionState.Closed)
				return;
			var buffer = new byte[packet.Length];
			packet.Build(ref buffer, 0);
			_socket.Send(buffer);
		}

		/// <summary>
		/// Handles login server packets.
		/// </summary>
		/// <param name="packet"></param>
		protected override void HandlePacket(Packet packet)
		{
			LoginPacketHandler.Instance.Handle(this, packet);
		}

		/// <summary>
		/// Cleans up connection, incl. account and characters.
		/// </summary>
		protected override void CleanUp()
		{
			return;
		}
	}
}
