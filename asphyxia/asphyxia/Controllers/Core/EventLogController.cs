using asphyxia.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using eAmuseCore.KBinXML;

namespace asphyxia.Controllers.KFC._6
{
    [Route("kfc/6")]
    [ApiController]
    public class EventLogController : ControllerBase
    {
        [HttpPost, XrpcCall("eventlog.write")]
        public ActionResult<EamuseXrpcData> EventLog([FromBody] EamuseXrpcData data)
        {
            Console.WriteLine(data.Document);

            Webhook.SendEmbed(Webhook.CreateEmbed("eventlog.write", data.Document.ToString(), data.Document.Element("call").Attribute("srcid").Value));

            /*
             *<?xml version="1.0" encoding="ASCII"?>
               <response>
                   <eventlog status="0">
                       <gamesession __type="s64">1</gamesession>
                       <logsendflg __type="s32">0</logsendflg>
                       <logerrlevel __type="s32">0</logerrlevel>
                       <evtidnosendflg __type="s32">0</evtidnosendflg>
                   </eventlog>
               </response>
               
             */

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
