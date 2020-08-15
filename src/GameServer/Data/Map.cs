#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.Collections.Generic;
using System.Linq;
using Agonyl.Shared.Network;

namespace Agonyl.Game.Data
{
    public class Map
    {
        public ushort Id { get; private set; }

        public string Name { get; private set; }

        /// <summary>
        /// Range a character can see.
        /// </summary>
        public const int VisibleBlock = 9;

        public Map(ushort id, string name = "")
        {
            this._characters = new Dictionary<int, Character>();

            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// Collection of characters.
        /// <para>Key: <see cref="Character.Handle"/></para>
        /// <para>Value: <see cref="Character"/></para>
        /// </summary>
        private Dictionary<int, Character> _characters;

        /// <summary>
        /// Updates all world entities.
        /// </summary>
        public void UpdateEntities()
        {
            // TODO: Understand ZoneServer logic and build the feature
        }

        public void PingCharacters()
        {
            foreach (var character in this._characters.Values)
            {
                if (character.CurrentState == Shared.Const.PlayerState.STANDBY)
                {
                    continue;
                }

                var msg = new MSG_CHK_TIMETICK();
                character.CurrentServerTick = (uint)(int.MaxValue & (Environment.TickCount - character.Handle));
                msg.TickCount = character.CurrentTickCount++;
                msg.ServerTick = character.CurrentServerTick;
                character.GameConnection.NoEncryptSend(msg.Serialize());
                Shared.Util.Log.Info("Sent ping to " + character.Name);
            }
        }

        /// <summary>
        /// Adds character to map.
        /// </summary>
        /// <param name="character"></param>
        public void AddCharacter(Character character)
        {
            character.Map = this;
            character.MapId = this.Id;

            lock (this._characters)
            {
                this._characters[character.Handle] = character;
            }
        }

        /// <summary>
        /// Removes character from map.
        /// </summary>
        /// <param name="character"></param>
        public void RemoveCharacter(Character character)
        {
            lock (this._characters)
            {
                _ = this._characters.Remove(character.Handle);
            }

            character.Map = null;
        }

        /// <summary>
        /// Returns character by handle, or null if it doesn't exist.
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public Character GetCharacter(int handle)
        {
            Character result;
            lock (this._characters)
            {
                _ = this._characters.TryGetValue(handle, out result);
            }

            return result;
        }

        /// <summary>
        /// Returns all characters on this map.
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public Character[] GetCharacters()
        {
            lock (this._characters)
            {
                return this._characters.Values.ToArray();
            }
        }

        /// <summary>
        /// Returns all characters on this map that match the given predicate.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Character[] GetCharacters(Func<Character, bool> predicate)
        {
            lock (this._characters)
            {
                return this._characters.Values.Where(predicate).ToArray();
            }
        }

        /// <summary>
        /// Broadcasts packet to all characters on map.
        /// </summary>
        /// <param name="packet"></param>
        public virtual void Broadcast(Packet packet)
        {
            lock (this._characters)
            {
                foreach (var character in this._characters.Values)
                {
                    character.GameConnection.Send(packet);
                }
            }
        }

        public virtual void Broadcast(byte[] packet)
        {
            lock (this._characters)
            {
                foreach (var character in this._characters.Values)
                {
                    character.GameConnection.Send(packet);
                }
            }
        }

        /// <summary>
        /// Broadcasts packet to all characters on map, that are within
        /// visible range of source.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="source"></param>
        /// <param name="includeSource"></param>
        public virtual void Broadcast(Packet packet, Character source, bool includeSource = true)
        {
            lock (this._characters)
            {
                foreach (var character in this._characters.Values.Where(a => (includeSource || a != source) && a.CurrentPostion.InRange(source.CurrentPostion, VisibleBlock)))
                {
                    character.GameConnection.Send(packet);
                }
            }
        }

        public virtual void Broadcast(byte[] packet, Character source, bool includeSource = true)
        {
            lock (this._characters)
            {
                foreach (var character in this._characters.Values.Where(a => (includeSource || a != source) && a.CurrentPostion.InRange(source.CurrentPostion, VisibleBlock)))
                {
                    character.GameConnection.Send(packet);
                }
            }
        }
    }
}
