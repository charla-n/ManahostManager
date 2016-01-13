using System;

namespace ManahostManager.Utils
{
    public class TypeOfName
    {
        public static String GetNameFromType<T>()
        {
            return typeof(T).Name;
        }
    }
}