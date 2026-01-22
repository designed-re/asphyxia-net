using System.Runtime.CompilerServices;
using System.Xml.Linq;
using luna.Utils;
using luna.Utils.Formatters;
using luna.Utils.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KFC_EXD
{
    [Route("kfc/6")]
    [ApiController]
    public class FrozenController : ControllerBase
    {
        private readonly AsphyxiaContext ctx;

        public FrozenController(AsphyxiaContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpPost, XrpcCall("game.sv6_frozen")]
        public async Task<ActionResult<EamuseXrpcData>> Frozen([FromBody] EamuseXrpcData data)
        {
            XElement gameElement = data.Document.Element("call").Element("game");
            string refid = gameElement.Element("refid").Value;

            Card? card = await ctx.Cards.SingleOrDefaultAsync(x =>
                x.RefId == refid);
            if (card.SvProfile?.Name is null)
            {
                    data.Document =
                        new XDocument(new XElement("response", new XElement("game", new XAttribute("status", "0")))); 
                return data;
            }
            data.Document =
                new XDocument(new XElement("response", new XElement("game", new XAttribute("status", "0"))));
            return data;
        }
    }
}
