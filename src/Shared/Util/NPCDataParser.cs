#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.IO;
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
            npcData.Name = System.Text.Encoding.Default.GetString(Functions.SkipAndTakeLinqShim(ref fileBytes, 20)).Trim();
            npcData.Id = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 20));
            npcData.RespawnRate = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 22));
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
                    Damage = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 32 + (i * 8))),
                    AdditionalDamage = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 34 + (i * 8))),
                };
            }

            npcData.AttackSpeedLow = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 52));
            npcData.AttackSpeedHigh = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 54));
            npcData.MovementSpeed = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 56));
            npcData.Level = fileBytes[60];
            npcData.PlayerExp = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 61));
            npcData.Appearance = fileBytes[63];
            npcData.HP = Functions.BytesToUInt32(Functions.SkipAndTakeLinqShim(ref fileBytes, 4, 64));
            npcData.BlueAttackDefense = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 70));
            npcData.RedAttackDefense = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 72));
            npcData.GreyAttackDefense = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 74));
            npcData.MercenaryExp = Functions.BytesToUInt16(Functions.SkipAndTakeLinqShim(ref fileBytes, 2, 76));
        }
    }
}
