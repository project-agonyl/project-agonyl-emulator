#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System.Collections.Generic;
using System.IO;

namespace Agonyl.Shared.Util
{
    public class BinaryFileParser
    {
        public string FilePath { get; protected set; }

        protected BinaryFileParser(string filePath)
        {
            this.FilePath = filePath;
            if (!File.Exists(this.FilePath))
            {
                throw new FileNotFoundException("Data file '" + this.FilePath + "' couldn't be found.", this.FilePath);
            }
        }
    }
}
