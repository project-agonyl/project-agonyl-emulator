#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Agonyl.Shared.Data.Game;

namespace Agonyl.Shared.Util
{
    public class IT0exParser : BinaryFileParser
    {
        public IT0exParser(string filePath)
            : base(filePath)
        {
        }
    }
}
