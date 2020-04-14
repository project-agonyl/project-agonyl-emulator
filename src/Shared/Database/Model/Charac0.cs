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

        public string GetInventory()
        {
            return this.GetMBodyPart("_1INVEN");
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
