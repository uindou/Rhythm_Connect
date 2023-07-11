using UnityEditor;
using UnityEngine;

namespace RC.Util
{
    public class RcExtension
    {
        public static int? IntTryParse(string str)
        {
            if (int.TryParse(str, out int result))
            {
                return result;
            }
            return null;
        }

        public static double? DoubleTryParse(string str)
        {
            if (double.TryParse(str, out double result))
            {
                return result;
            }
            return null;
        }
    }
}