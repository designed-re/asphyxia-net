using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Xml.Linq;
using asphyxia.Models;
using asphyxia.Utils;
using Microsoft.EntityFrameworkCore;
using ByteArrayHelper = asphyxia.Helpers.ByteArrayHelper;
using asphyxia.Utils.Formatters;

namespace asphyxia.Controllers.Core
{
    [Route("core")]
    [ApiController]
    public class CardmngController : ControllerBase
    {
        private readonly AsphyxiaContext ctx;

        public CardmngController(AsphyxiaContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpPost, XrpcCall("cardmng.inquire")]
        public ActionResult<EamuseXrpcData> Inquire([FromBody] EamuseXrpcData data)
        {

            XElement cardmng = data.Document.Element("call")?.Element("cardmng");

            string cardId = cardmng.Attribute("cardid").Value.ToUpper();
            string cardType = cardmng.Attribute("cardtype").Value;
            string update = cardmng.Attribute("update").Value;


            Webhook.SendEmbed(Webhook.CreateEmbed("cardmng.inquire", data.Document.ToString(), "card id: " + cardId));

            var card = ctx.Cards.SingleOrDefault(c => c.CardId == cardId);
            
            if (card != null)
            {
                data.Document = new XDocument(new XElement("response", new XElement("cardmng",
                    new XAttribute("binded", "1"),
                    new XAttribute("dataid", card.RefId),
                    new XAttribute("ecflag", "1"),
                    new XAttribute("newflag", "0"),
                    new XAttribute("expired", "0"),
                    new XAttribute("refid", card.RefId)
                )));
            }
            else
            {
                data.Document = new XDocument(new XElement("response", new XElement("cardmng",
                    new XAttribute("status", "112")
                )));
            }

            return data;
        }
        
        [HttpPost, XrpcCall("cardmng.authpass")]
        public async Task<ActionResult<EamuseXrpcData>> Authpass([FromBody] EamuseXrpcData data)
        {
            XElement cardmng = data.Document.Element("call").Element("cardmng");

            string pass = cardmng.Attribute("pass").Value;
            string refId = cardmng.Attribute("refid").Value.ToUpper();

            Webhook.SendEmbed(Webhook.CreateEmbed("cardmng.authpass", data.Document.ToString(), $"card id: {refId} pass: {pass}"));


            Card card = await ctx.Cards
                .SingleOrDefaultAsync(c => c.RefId == refId);

            int status;
            if (card != null && card.Pass == pass)
                status = 0;
            else
                status = 116;

            data.Document = new XDocument(new XElement("response", new XElement("cardmng",
                new XAttribute("status", status)
            )));

            return data;
        }

        [HttpPost, XrpcCall("cardmng.getrefid")]
        public async Task<ActionResult<EamuseXrpcData>> GetRefId([FromBody] EamuseXrpcData data)
        {
            XElement cardmng = data.Document.Element("call").Element("cardmng");

            string cardId = cardmng.Attribute("cardid").Value.ToUpper();
            string passwd = cardmng.Attribute("passwd").Value;

            Webhook.SendEmbed(Webhook.CreateEmbed("cardmng.getrefid", data.Document.ToString(), $"card id: {cardId} pass: {passwd}"));


            if (await ctx.Cards.AnyAsync(c => c.CardId == cardId))
            {
                data.Document = new XDocument(new XElement("response", new XElement("cardmng")));
                return data;
            }

            Random rng = new Random();

            byte[] dataId = new byte[8];
            byte[] refId = new byte[8];
            rng.NextBytes(dataId);
            rng.NextBytes(refId);
            
            Card card = new Card()
            {
                CardId = cardId,
                RefId = ByteArrayHelper.ToHexString(refId),
                CardNo = cardId,
                Pass = passwd
            };
            ctx.Cards.Add(card);

            await ctx.SaveChangesAsync();

            data.Document = new XDocument(new XElement("response", new XElement("cardmng",
                    new XAttribute("dataid", card.RefId),
                    new XAttribute("refid", card.RefId)
            )));

            return data;
        }
    }
}
