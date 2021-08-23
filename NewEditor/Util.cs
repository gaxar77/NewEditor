using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaxar77.NewEditor
{
    static class Util
    {
        public static string FirstOrDefaultStringIgnoreCase(this IEnumerable<string> items,
            string stringToMatch)
        {
            return items.FirstOrDefault(str =>
                String.Equals(str, stringToMatch,
                    StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
