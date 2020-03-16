#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

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

        protected string GetMBodyPart(string startsWith)
        {
            if (this.m_body == null)
            {
                return string.Empty;
            }

            foreach (string item in this.m_body.Split('\\'))
            {
                if (item.StartsWith(startsWith))
                {
                    return item;
                }
            }

            return string.Empty;
        }
    }
}
