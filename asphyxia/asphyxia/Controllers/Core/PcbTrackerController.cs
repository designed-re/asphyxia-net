using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using asphyxia.Utils;
using eAmuseCore.KBinXML;
using asphyxia.Utils.Formatters;

namespace asphyxia.Controllers.Core
{
    [Route("core")]
    [ApiController]
    public class PcbTrackerController : ControllerBase
    {
        //    <pcbtracker ecenable="1" eclimit="0" expire="1200" limit="0" status="0" time="1742044281"/>


        [HttpPost, XrpcCall("pcbtracker.alive")]
        public ActionResult<EamuseXrpcData> Alive([FromBody] EamuseXrpcData data, [FromQuery] string model)
        {
            data.Document = new XDocument(new XElement("response", new XElement("pcbtracker",
                new XAttribute("ecenable", "1"),
                new XAttribute("eclimit", "0"),
                new XAttribute("expire", "1200"),
                new XAttribute("status", "0"),
                new XAttribute("time", ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds().ToString())
            )));

            return data;
        }
    }
}
