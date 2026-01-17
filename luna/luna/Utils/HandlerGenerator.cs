using luna.Models;

namespace luna.Utils
{
    public class HandlerGenerator
    {
        public static string[] GenerateHandlers(string prefix, string[] endpoints)
        {
            return endpoints.Select(e => prefix+e).ToArray();
        }
    }
}
