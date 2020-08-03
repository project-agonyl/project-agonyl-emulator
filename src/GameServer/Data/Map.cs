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
        public const int VisibleBlock = 15;

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
        /// <returns></returns>
        public Character[] GetCharacters(Func<Character, bool> predicate)
        {
            lock (this._characters)
            {
                return this._characters.Values.Where(predicate).ToArray();
            }
        }
    }
}
