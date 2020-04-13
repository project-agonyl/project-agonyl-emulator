#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using Agonyl.Shared.Network;

namespace Agonyl.Shared.Database.Model
{
    public class Charac0
    {
        public string c_id { get; set; }

        public string c_sheadera { get; set; }

        public string c_sheaderb { get; set; }

        public string c_sheaderc { get; set; }

        public string c_headera { get; set; }

        public string c_headerb { get; set; }

        public string c_headerc { get; set; }

        public string m_body { get; set; }

        public string GetWear()
        {
            return this.GetMBodyPart("_1WEAR");
        }

        public string GetExp()
        {
            return this.GetMBodyPart("EXP");
        }

        public string GetSkill()
        {
            return this.GetMBodyPart("_1SKILL");
        }

        public string GetPk()
        {
            return this.GetMBodyPart("_1PK");
        }

        public string GetRTime()
        {
            return this.GetMBodyPart("_1RTM");
        }

        public string GetSInfo()
        {
            return this.GetMBodyPart("_1SINFO");
        }

        public string GetLore()
        {
            return this.GetMBodyPart("_1LORE");
        }

        public CHARACTER_INFO GetMsgCharacterInfo()
        {
            var info = default(CHARACTER_INFO);
            info.Name = this.c_id;
            info.Type = Convert.ToByte(this.c_sheaderb);
            info.Level = Convert.ToUInt16(this.c_sheaderc);
            info.Exp = Convert.ToUInt32(this.GetExp());
            info.MapIndex = Convert.ToUInt32(this.c_headerb.Split(';')[0]);
            info.CellIndex = Convert.ToUInt32(this.c_headerb.Split(';')[1]);
            info.SkillList = default(SKILL_INFO);
            info.SkillList.Skill0 = Convert.ToUInt32(this.GetSkill().Split(';')[0]);
            info.SkillList.Skill1 = Convert.ToUInt32(this.GetSkill().Split(';')[1]);
            info.SkillList.Skill2 = Convert.ToUInt32(this.GetSkill().Split(';')[2]);
            info.PKCount = Convert.ToUInt32(this.GetPk());
            info.RTime = Convert.ToUInt32(this.GetRTime());
            Util.Log.Info("PK is " + info.PKCount + "RTime is " + info.RTime);
            info.SInfo = default(SOCIAL_INFO);
            info.SInfo.Nation = 0; // For now all are temoz
            info.SInfo.Rank = 0;
            info.SInfo.KnightIndex = 0;
            info.Money = Convert.ToUInt32(this.c_headerc);
            info.StoredHp = Convert.ToUInt32(this.c_headera.Split(';')[8]); // Check if correct
            info.StoredMp = Convert.ToUInt32(this.c_headera.Split(';')[9]); // Check if correct
            info.Lore = Convert.ToUInt32(this.GetLore());
            return info;
        }

        public CHARACTER_STAT GetMsgCharacterStat()
        {
            var stat = default(CHARACTER_STAT);
            stat.Str = Convert.ToUInt16(this.c_headera.Split(';')[0]);
            stat.Magic = Convert.ToUInt16(this.c_headera.Split(';')[1]);
            stat.Dex = Convert.ToUInt16(this.c_headera.Split(';')[2]);
            stat.Vit = Convert.ToUInt16(this.c_headera.Split(';')[3]);
            stat.Mana = Convert.ToUInt16(this.c_headera.Split(';')[4]);
            stat.Point = Convert.ToUInt16(this.c_headera.Split(';')[5]);
            stat.HP = Convert.ToUInt16(this.c_headera.Split(';')[6]);
            stat.MP = Convert.ToUInt16(this.c_headera.Split(';')[7]);
            stat.HPCapacity = Convert.ToUInt32(this.c_headera.Split(';')[8]);
            stat.MPCapacity = Convert.ToUInt32(this.c_headera.Split(';')[9]);
            stat.CalculatedStat = default(CHARACTER_CALCULATED_STAT);
            return stat;
        }

        protected string GetMBodyPart(string startsWith)
        {
            if (this.m_body == null)
            {
                return string.Empty;
            }

            foreach (var item in this.m_body.Split('\\'))
            {
                if (item.StartsWith(startsWith))
                {
                    return item.Replace(startsWith + "=", string.Empty);
                }
            }

            return string.Empty;
        }
    }
}
