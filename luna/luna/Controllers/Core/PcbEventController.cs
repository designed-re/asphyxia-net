using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using luna.Utils;
using luna.Utils.Formatters;

namespace luna.Controllers.Core
{
    [Route("core")]
    [ApiController]
    public class PcbEventController : ControllerBase
    {
        [HttpPost, XrpcCall("pcbevent.put")]
        public ActionResult<EamuseXrpcData> Put([FromBody] EamuseXrpcData data)
        {
            // TODO: log these, maybe?

            data.Document = new XDocument(new XElement("response", new XElement("pcbevent")));
            return data;
        }
    }
}
