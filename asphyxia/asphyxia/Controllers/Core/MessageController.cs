using asphyxia.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace asphyxia.Controllers.Core
{
    [Route("core")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost, XrpcCall("message.get")]
        public ActionResult<EamuseXrpcData> Get([FromBody] EamuseXrpcData data)
        {
            data.Document = new XDocument(new XElement("response", new XElement("message")));

            return data;
        }
    }
}
