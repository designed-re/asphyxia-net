using asphyxia.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using asphyxia.Utils;

namespace asphyxia.Controllers.Core
{
    [Route("core")]
    [ApiController]
    public class PcbEventController : ControllerBase
    {
        [HttpPost, XrpcCall("pcbevent.put")]
        public ActionResult<EamuseXrpcData> Put([FromBody] EamuseXrpcData data)
        {
            // TODO: log these, maybe?
            Console.WriteLine(data.Document);

            data.Document = new XDocument(new XElement("response", new XElement("pcbevent")));
            return data;
        }
    }
}
