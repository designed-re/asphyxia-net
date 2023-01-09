using System.Xml.Linq;
using asphyxia.Formatters;
using asphyxia.Models;
using asphyxia.Utils;
using eAmuseCore.KBinXML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asphyxia.Controllers.KFC._6
{
    [Route("kfc/6")]
    [ApiController]
    public class NewController : ControllerBase
    {
        private readonly AsphyxiaContext ctx;
        
        public NewController(AsphyxiaContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpPost, XrpcCall("game.sv6_new")]
        public async Task<ActionResult<EamuseXrpcData>> New([FromBody] EamuseXrpcData data)
        {
            Console.WriteLine(data.Document);
            XElement gameElement = data.Document.Element("call").Element("game");
            Card? card = await ctx.Cards.SingleOrDefaultAsync(x =>
                x.RefId == gameElement.Element("refid").Value);
            if (card.SvProfile?.Name is not null)
            {
                data.Document = new XDocument(new XElement("response", new XElement("game", new XAttribute("status", "0"), new KU8("result", 1))));
                return data;
            }

            SvProfile profile = new()
            {
                Card = card.Id,
                Name = gameElement.Element("name").Value,
                Code = CodeGenerator.GetCode(ctx),
                KacId = gameElement.Element("name").Value
            };

            card.SvProfile = profile;
            ctx.SvProfiles.Update(profile);
            ctx.Cards.Update(card);
            await ctx.SaveChangesAsync();

            data.Document = new XDocument(new XElement("response", new XElement("game", new XAttribute("status", "0"), new KU8("result", 0))));

            return data;
        }
    }
}
