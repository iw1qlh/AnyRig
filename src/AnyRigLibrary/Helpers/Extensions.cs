
/* Unmerged change from project 'AnyRigLibrary (net6.0)'
Before:
using AnyRigBase.Models;
using AnyRigLibrary.Models;
After:
using AnyRigLibrary.Models;
using AnyRigBase.Models;
*/
using AnyRigLibrary.Models;
using System;

namespace AnyRigLibrary.Helpers
{
    public static class Extensions
    {
        public static RigParam ToRigParam(this string value)
        {
            RigParam result = RigParam.UNKNOWN;

            foreach (RigParam rp in (RigParam[])Enum.GetValues(typeof(RigParam)))
            {
                if (rp.ToString() == value)
                {
                    result = rp;
                    break;
                }
            }

            return result;

        }

        public static bool? ToBool(this string value)
        {
            if (string.Compare(value, "ON", ignoreCase: true) == 0)
                return true;
            if (string.Compare(value, "OFF", ignoreCase: true) == 0)
                return false;
            return null;
        }

    }
}
