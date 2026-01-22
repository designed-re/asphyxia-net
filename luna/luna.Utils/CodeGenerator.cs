using luna.Utils.Models;
using Microsoft.EntityFrameworkCore;

namespace luna.Utils
{
    public class CodeGenerator
    {
        public static string GetCode(AsphyxiaContext context)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            gen:
            string code = r.Next(1, 9999).ToString("D4") + "-" + r.Next(1, 9999).ToString("D4");

            if (context.SvProfiles.Any(x => x.Code == code)) goto gen;

            return code;

        }
    }
}
