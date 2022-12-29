using System.Xml.Linq;
using asphyxia.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asphyxia.Controllers.KFC._6
{
    [Route("kfc/6")]
    [ApiController]
    public class FrozenController : ControllerBase
    {
        [HttpPost, XrpcCall("game.sv6_frozen")]
        public async Task<ActionResult<EamuseXrpcData>> Frozen([FromBody] EamuseXrpcData data)
        {
            data.Document =
                new XDocument(new XElement("response", new XElement("game", new XAttribute("status", "0"))));
            return data;
        }
    }
}
