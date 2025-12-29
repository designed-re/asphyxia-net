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
            XElement logElement = new XElement("eventlog", new XAttribute("status", "0"));
            logElement.Add(new XElement("gamesession", "1", new XAttribute("__type", "s64")));
            logElement.Add(new XElement("logsendflg", "0", new XAttribute("__type", "s32")));
            logElement.Add(new XElement("loggerrlevel", "0", new XAttribute("__type", "s32")));
            logElement.Add(new XElement("evtidnosendflg", "0", new XAttribute("__type", "s32")));
            data.Document = new XDocument(new XElement("response", logElement));
            return data;
        }
    }
}
