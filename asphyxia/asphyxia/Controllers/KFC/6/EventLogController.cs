using asphyxia.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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
                new XElement("gamesession", new XAttribute("__type", "s64"), 1),
                new XElement("logsendflg", new XAttribute("__type", "s32"), 0),
                new XElement("loggerrlevel", new XAttribute("__type", "s32"), 0),
                new XElement("evtidnosendflg", new XAttribute("__type", "s32"), 0))));
            return data;
        }
    }
}
