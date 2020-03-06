#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System.Collections.Generic;
using System.IO;

namespace Agonyl.Game.Util
{
    public abstract class BinaryFileParser
    {
        public string FilePath { get; protected set; }

        protected BinaryFileParser(string FilePath)
        {
            this.FilePath = FilePath;
            if (!File.Exists(this.FilePath))
            {
                throw new FileNotFoundException("Data file '" + this.FilePath + "' couldn't be found.", this.FilePath);
            }
        }
    }
}
