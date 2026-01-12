using asphyxia.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using asphyxia.Utils;

namespace asphyxia.Controllers.Core
{
    [Route("core")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost, XrpcCall("message.get")]
        public ActionResult<EamuseXrpcData> Get([FromBody] EamuseXrpcData data)
        {
            // data.Document = new XDocument(new XElement("response",
            //     new XElement("message", new XAttribute("expire", "300"), new XAttribute("status", "0"),
            //         new XElement("item", new XAttribute("end", "86400"), new XAttribute("name", "sys.mainte"),
            //             new XAttribute("start", "0")),
            //         new XElement("item", new XAttribute("end", "86400"), new XAttribute("name", "sys.eacoin.mainte"),
            //             new XAttribute("start", "0"))))); //todo: enable this to maintenance mode
            data.Document = new XDocument(new XElement("response",
                new XElement("message", new XAttribute("expire", "300"), new XAttribute("status", "0"))));

            return data;
            }
    }
}
