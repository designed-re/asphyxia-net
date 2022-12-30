using System.Xml.Linq;
using asphyxia.Formatters;
using asphyxia.Models;
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

            if (card?.Player.Name is not null)
            {
                data.Document = new XDocument(new XElement("response", new XElement("game", new XAttribute("status", "1"))));
                return data;
            }

            Player player = card.Player;
            player.Name = gameElement.Element("name").Value;
            ctx.Cards.Update(card);
            ctx.Players.Update(player);
            await ctx.SaveChangesAsync();

            data.Document = new XDocument(new XElement("response", new XElement("game", new XAttribute("status", "0"))));

            return data;
        }
    }
}
