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
            return this.GetMBodyPart("WEAR");
        }

        protected string GetMBodyPart(string StartsWith)
        {
            if (this.m_body == null)
            {
                return "";
            }
            var mBodyArray = this.m_body.Split("\\_1".ToCharArray());
            foreach (var item in mBodyArray)
            {
                if (item.StartsWith(StartsWith))
                {
                    return item;
                }
            }
            return "";
        }
    }
}
