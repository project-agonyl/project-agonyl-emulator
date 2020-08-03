#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Agonyl.Game.Data;

namespace Agonyl.Game
{
    public class WorldManager
    {
        public const int HeartbeatTime = 500;
        public const int Second = 1000;
        public const int Minute = Second * 60;
        public const int Hour = Minute * 60;
        public const int PingInterval = Second * 5;

        private int _characterHandles;
        private int _monsterHandles;
        private Dictionary<ushort, Map> _mapsId;
        private object _mapsLock = new object();

        private Timer _heartbeatTimer;
        private Timer _clientPing;

        /// <summary>
        /// Returns the amount of maps in the world.
        /// </summary>
        public int Count { get { lock (this._mapsLock) { return this._mapsId.Count; } } }

        /// <summary>
        /// Creates new world manager.
        /// </summary>
        public WorldManager()
        {
            this._mapsId = new Dictionary<ushort, Map>();
        }

        /// <summary>
        /// Initializes heartbeat timer.
        /// </summary>
        private void SetUpHeartbeat()
        {
            var now = DateTime.Now;

            // Start timer on the next HeartbeatTime
            // (eg on the next full 500 ms) and run it regularly afterwards.
            this._heartbeatTimer = new Timer(this.Heartbeat, null, HeartbeatTime - ((now.Ticks / 10000) % HeartbeatTime), HeartbeatTime);
        }

        /// <summary>
        /// Handles regularly occuring events and raises time events.
        /// </summary>
        /// <remarks>
        /// On the first call all time events are raised,
        /// because lastHeartbeat is 0, and the events depend on the time
        /// since the last heartbeat. This also ensures that they aren't
        /// called multiple times.
        /// </remarks>
        private void Heartbeat(object _)
        {
            this.UpdateEntities();
        }

        /// <summary>
        /// Updates all entities on all maps.
        /// </summary>
        private void UpdateEntities()
        {
            lock (this._mapsLock)
            {
                foreach (var map in this._mapsId.Values)
                {
                    map.UpdateEntities();
                }
            }
        }

        private void SetUpClientPing()
        {
            this._clientPing = new Timer(this.ClientPing, null, PingInterval, PingInterval);
        }

        private void ClientPing(object _)
        {
            foreach (var map in this._mapsId.Values)
            {
                map.PingCharacters();
            }
        }

        public int CreateCharacterHandle()
        {
            return Interlocked.Increment(ref this._characterHandles);
        }

        public int CreateMonsterHandle()
        {
            return Interlocked.Increment(ref this._monsterHandles);
        }

        /// <summary>
        /// Initializes world.
        /// </summary>
        public void Initialize()
        {
            foreach (var map in GameServer.Instance.GameData.Maps)
            {
                this._mapsId.Add(map.Key, new Map(map.Value.Id, map.Value.Name));
            }

            this.SetUpHeartbeat();
            this.SetUpClientPing();
        }

        /// <summary>
        /// Returns map by id, or null if it doesn't exist.
        /// </summary>
        /// <param name="mapId"></param>
        public Map GetMap(ushort mapId)
        {
            Map result;
            lock (this._mapsLock)
            {
                this._mapsId.TryGetValue(mapId, out result);
            }

            return result;
        }

        /// <summary>
        /// Returns all Characters that are currently online.
        /// </summary>
        public Character[] GetCharacters()
        {
            lock (this._mapsLock)
            {
                return this._mapsId.Values.SelectMany(a => a.GetCharacters()).ToArray();
            }
        }

        /// <summary>
        /// Returns all online characters that match the given predicate.
        /// </summary>
        public Character[] GetCharacters(Func<Character, bool> predicate)
        {
            lock (this._mapsLock)
            {
                return this._mapsId.Values.SelectMany(a => a.GetCharacters(predicate)).ToArray();
            }
        }
    }
}
