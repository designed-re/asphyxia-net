using System.Xml.Linq;
using asphyxia.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asphyxia.Controllers.KFC._6
{
    [Route("kfc/6")]
    [ApiController]
    public class LoadController : ControllerBase
    {
        [HttpPost, XrpcCall("game.sv6_load")]
        public async Task<ActionResult<EamuseXrpcData>> DataLoad([FromBody] EamuseXrpcData data)
        {
            //todo check card is exists
            // Console.WriteLine(data.Document);
            Console.WriteLine(
                $"Requested sv6_load with CardId: {data.Document.Element("call").Element("game").Element("cardid").Value}");

            data.Document = new XDocument(new XElement("response",
                new XElement("game", new XAttribute("status", "0"),
                    new XElement("result", new XAttribute("__type", "u8"), 1))));
            // Console.WriteLine(data.Document);
            return data;
        }
        
    }
}
