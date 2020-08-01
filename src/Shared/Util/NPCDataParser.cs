#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.IO;
using System.Linq;
using Agonyl.Shared.Data.Game;

namespace Agonyl.Shared.Util
{
    public class NPCDataParser : BinaryFileParser
    {
        public NPCDataParser(string filePath)
            : base(filePath)
        {
        }

        public void ParseData(ref NPCData npcData)
        {
            var fileBytes = File.ReadAllBytes(this.FilePath);
            npcData.Name = System.Text.Encoding.Default.GetString(fileBytes.Take(20).ToArray());
            npcData.Id = Functions.BytesToUInt16(fileBytes.Skip(20).Take(2).ToArray());
            npcData.RespawnRate = Functions.BytesToUInt16(fileBytes.Skip(22).Take(2).ToArray());
            npcData.AttackTypeInfo = fileBytes[24];
            npcData.TargetSelectionInfo = fileBytes[25];
            npcData.Defense = fileBytes[26];
            npcData.AdditionalDefense = fileBytes[27];
            for (var i = 0; i < 3; i++)
            {
                npcData.Attacks[i] = new NPCAttack()
                {
                    Range = fileBytes[28 + (i * 8)],
                    Area = fileBytes[29 + (i * 8)],
                    Damage = Functions.BytesToUInt16(fileBytes.Skip(32 + (i * 8)).Take(2).ToArray()),
                    AdditionalDamage = Functions.BytesToUInt16(fileBytes.Skip(34 + (i * 8)).Take(2).ToArray()),
                };
            }

            npcData.AttackSpeedLow = Functions.BytesToUInt16(fileBytes.Skip(52).Take(2).ToArray());
            npcData.AttackSpeedHigh = Functions.BytesToUInt16(fileBytes.Skip(54).Take(2).ToArray());
            npcData.MovementSpeed = Functions.BytesToUInt16(fileBytes.Skip(56).Take(2).ToArray());
            npcData.Level = fileBytes[60];
            npcData.PlayerExp = Functions.BytesToUInt16(fileBytes.Skip(61).Take(2).ToArray());
            npcData.Appearance = fileBytes[63];
            npcData.HP = Functions.BytesToUInt32(fileBytes.Skip(64).Take(4).ToArray());
            npcData.BlueAttackDefense = Functions.BytesToUInt16(fileBytes.Skip(70).Take(2).ToArray());
            npcData.RedAttackDefense = Functions.BytesToUInt16(fileBytes.Skip(72).Take(2).ToArray());
            npcData.GreyAttackDefense = Functions.BytesToUInt16(fileBytes.Skip(74).Take(2).ToArray());
            npcData.MercenaryExp = Functions.BytesToUInt16(fileBytes.Skip(76).Take(2).ToArray());
        }
    }
}
