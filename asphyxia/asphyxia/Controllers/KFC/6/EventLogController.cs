using asphyxia.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using eAmuseCore.KBinXML;

namespace asphyxia.Controllers.Core
{
    [Route("core")]
    [ApiController]
    public class EventLogController : ControllerBase
    {
        [HttpPost, XrpcCall("eventlog.write")]
        public ActionResult<EamuseXrpcData> List([FromBody] EamuseXrpcData data)
        {
            data.Document = new(new XElement("response", new XElement("eventlog",
                new KS64("gamesession", 1),
                new KS32("logsendflg", 0),
                new KS32("loggerrlevel", 0),
                new KS32("evtidnosendflg", 0))));
            return data;
        }
    }
}
