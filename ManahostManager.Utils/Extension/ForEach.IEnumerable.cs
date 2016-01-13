using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManahostManager.Utils.Extension
{
    public static class ExtensionIEnumerable
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
            return enumeration;
        }
    }
}
