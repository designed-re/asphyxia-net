using asphyxia.Formatters;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace asphyxia.Controllers.Core
{
    [Route("core")]
    [ApiController]
    public class PcbTrackerController : ControllerBase
    {
        [HttpPost, XrpcCall("pcbtracker.alive")]
        public ActionResult<EamuseXrpcData> Alive([FromBody] EamuseXrpcData data, [FromQuery] string model)
        {
            data.Document = new XDocument(new XElement("response", new XElement("pcbtracker",
                new XAttribute("ecenable", "1")
            )));

            return data;
        }
    }
}
