using System.Linq;
using System.Xml.Linq;
using luna.Utils;
using luna.Utils.Formatters;
using luna.Utils.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace luna.Controllers.Core
{
    [Route("core")]
    [ApiController]
    public class EacoinController : ControllerBase
    {
        private readonly AsphyxiaContext _context;

        public EacoinController(AsphyxiaContext context)
        {
            _context = context;
        }

        [HttpPost, XrpcCall("eacoin.checkin")]
        public async Task<ActionResult<EamuseXrpcData>> CheckIn([FromBody] EamuseXrpcData data)
        {
            XElement responseElement = new("response");
            XElement eacoinElement = new("eacoin", new XAttribute("status", 0));

            Card card = await _context.Cards.SingleOrDefaultAsync(x =>
                x.CardId == data.Document.Element("call").Element("eacoin").Element("cardid").Value);
            string session = string.Concat(Guid.NewGuid().ToString("N").Take(16));
            card.PaseliSession = session;
            await _context.SaveChangesAsync();

            eacoinElement.Add(new XElement("balance", new XAttribute("__type", "s32"), card.Paseli),
                new XElement("sessid", new XAttribute("__type", "str"), session),
                new XElement("acstatus", new XAttribute("__type", "u8"), 0),
                new XElement("sequence", new XAttribute("__type", "s16"), 1),
                new XElement("acid", new XAttribute("__type", "str"), session),
                new XElement("acname", new XAttribute("__type", "str"), session));
            responseElement.Add(eacoinElement);

            data.Document = new XDocument(responseElement);
            return data;
        }

        [HttpPost, XrpcCall("eacoin.consume")]
        public async Task<ActionResult<EamuseXrpcData>> Consume([FromBody] EamuseXrpcData data)
        {
            XElement responseElement = new("response");

            Card? card = await _context.Cards.SingleOrDefaultAsync(x =>
                x.PaseliSession == data.Document.Element("call").Element("eacoin").Element("sessid").Value);


            int balance = card?.Paseli ?? 0;
            int payment = int.Parse(data.Document.Element("call").Element("eacoin").Element("payment").Value);

            XElement eacoinElement;
            if (balance < payment || card is null)
            {
                eacoinElement = new("eacoin", new XAttribute("status", 0));
                eacoinElement.Add(new XElement("balance", new XAttribute("__type", "s32"), balance),
                    new XElement("autocharge", new XAttribute("__type", "u8"), 0),
                    new XElement("acstatus", new XAttribute("__type", "u8"), 1));
                responseElement.Add(eacoinElement);
                data.Document = new XDocument(responseElement);
                return data;
            }

            card.Paseli -= payment;
            await _context.SaveChangesAsync();

            eacoinElement = new("eacoin", new XAttribute("status", 0));

            eacoinElement.Add(new XElement("balance", new XAttribute("__type", "s32"), balance-payment),
                new XElement("autocharge", new XAttribute("__type", "u8"), 0),
                new XElement("acstatus", new XAttribute("__type", "u8"), 0));
            responseElement.Add(eacoinElement);

            data.Document = new XDocument(responseElement);
            return data;
        }
    }
}
