using System;

namespace ManahostManager.Utils
{
    public class KeyGeneratorUtils
    {
        // http://madskristensen.net/post/generate-unique-strings-and-numbers-in-c
        public static String GenerateKey()
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return String.Format("{0:x}", i - DateTime.Now.Ticks);
        }
    }
}