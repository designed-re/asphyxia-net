using System;
using System.Collections.Generic;
using System.Text;

namespace luna.Utils
{
    public class VersionUtil
    {
        /// <summary>
        /// Gets the Sound Voltex version from eAmuse info
        /// </summary>
        /// <param name="model">Model string in format like "KFC:J:A:20250224:x"</param>
        /// <param name="method">Method string like "sv6_load"</param>
        /// <returns>Version number (1-7 or -6), or 0 if unknown</returns>
        public static int GetVersion(string model, string method)
        {
            try
            {
                var modelParts = model.Split(':');
                
                // Extract date code from model (index 4)
                if (modelParts.Length <= 4)
                    return 0;

                if (!int.TryParse(modelParts[4], out int dateCode))
                    return 0;

                if (dateCode <= 2013052900) return 1;
                if (dateCode <= 2014112000) return 2;
                if (dateCode <= 2016121200) return 3;
                if (method.StartsWith("sv4")) return 4;
                if (method.StartsWith("sv5")) return 5;
                if (dateCode >= 2025122400) return 7;
                if (dateCode >= 2021083100) return -6;
                if (method.StartsWith("sv6")) return 6;
                
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets absolute value of version (converts -6 to 6)
        /// </summary>
        public static int GetAbsoluteVersion(string model, string method)
        {
            return Math.Abs(GetVersion(model, method));
        }

        /// <summary>
        /// Extracts date code from model string
        /// </summary>
        public static int GetDateCode(string model)
        {
            try
            {
                var modelParts = model.Split(':');
                if (modelParts.Length <= 4)
                    return 0;

                if (int.TryParse(modelParts[4], out int dateCode))
                    return dateCode;

                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}

