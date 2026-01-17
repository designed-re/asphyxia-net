using System.Xml.Linq;
using luna.Utils;
using luna.Utils.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KFC_EXD
{
    [Route("kfc/6")]
    [ApiController]
    public class PlayController : ControllerBase
    {
        [HttpPost, XrpcCall("game.sv6_play_s")] //todo impl this
        public async Task<ActionResult<EamuseXrpcData>> PlayS([FromBody] EamuseXrpcData data)
        {
            data.Document = new XDocument(new XElement("response", new XElement("game", new XAttribute("status", 0))));
            return data;
        }

        [HttpPost, XrpcCall("game.sv6_play_e")] //todo impl this
        public async Task<ActionResult<EamuseXrpcData>> PlayE([FromBody] EamuseXrpcData data)
        {
            data.Document = new XDocument(new XElement("response", new XElement("game", new XAttribute("status", 0))));
            return data;
        }
    }
}
