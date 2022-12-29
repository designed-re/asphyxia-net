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
            data.Document = new(new XElement("gamesession", (long)1), new XElement("logsendflg", 0), new XElement("loggerrlevel", 0), new XElement("evtidnosendflg", 0));
            return data;
        }
    }
}
