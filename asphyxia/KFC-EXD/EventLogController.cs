using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using asphyxia.Utils;
using eAmuseCore.KBinXML;
using asphyxia.Utils.Formatters;

namespace KFC_EXD
{
    [Route("kfc/6")]
    [ApiController]
    public class EventLogController : ControllerBase
    {
        [HttpPost, XrpcCall("eventlog.write")]
        public ActionResult<EamuseXrpcData> EventLog([FromBody] EamuseXrpcData data)
        {
            Console.WriteLine(data.Document);


            XElement logElement = new XElement("eventlog", new XAttribute("status", "0"));
            logElement.Add(new KS64("gamesession", 1));
            logElement.Add(new KS32("logsendflg", 0));
            logElement.Add(new KS32("logerrlevel", 0));
            logElement.Add(new KS32("evtidnosendflg", 0));
            data.Document = new XDocument(new XElement("response", logElement));
            return data;
        }
    }
}