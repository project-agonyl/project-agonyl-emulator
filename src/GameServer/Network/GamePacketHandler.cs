#region copyright
// Copyright (c) 2018 Project Agonyl
#endregion

using Agonyl.Shared.Network;

namespace Agonyl.Game.Network
{
	public class GamePacketHandler : PacketHandler<GameConnection>
	{
		public static readonly GamePacketHandler Instance = new GamePacketHandler();
	}
}
