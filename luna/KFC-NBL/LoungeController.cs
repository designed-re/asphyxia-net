using System.Xml.Linq;
using luna.Utils;
using luna.Utils.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KFC_NBL
{
    [Route("kfc/7")]
    [ApiController]
    public class LoungeController : ControllerBase
    {
        [HttpPost, XrpcCall("game.sv7_lounge")] //todo impl this
        public async Task<ActionResult<EamuseXrpcData>> Lounge([FromBody] EamuseXrpcData data) //maybe this is online multiplayer?
        {
            data.Document = new XDocument(new XElement("response",
                new XElement("game", new XAttribute("status", 0),
                    new XElement("interval", new XAttribute("__type", "u32"), 30))));
            return data;
        }
    }
}
