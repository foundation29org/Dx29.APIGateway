using System;

namespace Dx29.Tools
{
    static public class IDGenerator
    {
        static private Random _random = new Random();

        static public string GenerateID(string prefix = "uid")
        {
            var date = DateTime.UtcNow;
            return $"{prefix}-{date:yyMMddHHmmssffff}{_random.Next(0, 9999):0000}";
        }
    }
}
