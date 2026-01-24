using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using luna.Utils;
using Microsoft.Extensions.Options;
using luna.Utils.Formatters;
using luna.Utils.Models;

namespace luna.Controllers.Core
{
    [Route("core")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly HostConfig _config;
        private readonly AsphyxiaContext _context;
        public ServicesController(HostConfig config, AsphyxiaContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost, XrpcCall("services.get")]
        public ActionResult<EamuseXrpcData> Get([FromBody] EamuseXrpcData data, [FromQuery] string model)
        {
            
            var req = data.Document.Element("call");
            var pcbId = req.Attribute("srcid");

            // string url = "http://localhost:8083";
            string url = Request.Scheme + "://" + Request.Host.Host + ":" + (Request.Host.Port ?? 80);
            string coreUrl = url + "/core";
            string modelUrl = url + "/core";
            string[] modelItems = new string[]{};

            string[] coreItems = new[]
{
                "cardmng",
                "facility",
                "message",
                "numbering",
                "package",
                "pcbevent",
                "pcbtracker",
                "pkglist",
                "posevent",
                "userdata",
                "userid",
                "eacoin",
                "dlstatus",
                "netlog",
                "sidmgr",
                "globby" //maybe this thing important to run game
            };
            if (model.StartsWith("KFC"))
            {
                var version = VersionUtil.GetAbsoluteVersion(model, "");
                modelItems = HandlerGenerator.GenerateHandlers("",
                    new[]
                    {
                        "local", "local2", "lobby", "lobby2"
                    });
                modelItems = modelItems.Concat(HandlerGenerator.GenerateHandlers($"game.sv{version}_",
                    new[]
                    {
                        "common", "new", "load", "load_m", "save", "save_m", "save_c", "frozen", "buy", "print",
                        "hiscore", "load_r", "save_ap", "load_ap", "lounge", "shop", "save_e", "save_mega", "play_e",
                        "play_s", "entry_s", "entry_e", "exception"
                    })).ToArray();
            
                // modelItems = new string[] { };
                modelUrl = url + $"/kfc/{version}";
            }
            else if (model.StartsWith("PIX"))
            {
                modelItems = HandlerGenerator.GenerateHandlers("",
                    new[]
                    {
                        "local", "local2", "lobby", "lobby2"
                    });
                modelItems = modelItems.Concat(HandlerGenerator.GenerateHandlers($"game_3.",
                    new[]
                    {
                        "common", "new", "load", "load_m", "save", "save_m", "frozen", "hiscore", "lounge", "shop", "exception"
                    })).ToArray();

                // modelItems = new string[] { };
                modelUrl = url + $"/pix";
            }
            else return NotFound();

            var fact = _context.Facilities.SingleOrDefault(x=> x.PCBId == pcbId.Value);

            var isAuthed = fact is not null || !_config.EnforcePCBId;

            XElement servicesElement = new XElement("services",
                new XAttribute("expire", "600"),
                new XAttribute("method", "get"),
                new XAttribute("mode", "operation"),
                new XAttribute("status", isAuthed ? "0" : "400"));

            servicesElement.Add(new XElement("item", new XAttribute("name", "ntp"), new XAttribute("url", "ntp://pool.ntp.org/")));
            servicesElement.Add(new XElement("item", new XAttribute("name", "keepalive"), new XAttribute("url", $"http://127.0.0.1/keepalive?pa=127.0.0.1&ia=127.0.0.1&ga=127.0.0.1&ma=127.0.0.1&t1=2&t2=10")));

            if (!isAuthed)
            {
                data.Document = new XDocument(new XElement("response", servicesElement));
                return data;
            }

            foreach (string coreItem in coreItems)
                servicesElement.Add(new XElement("item", new XAttribute("name", coreItem), new XAttribute("url", coreUrl)));

            if (modelItems.Length != 0)
            {
                foreach (string modelItem in modelItems)
                    servicesElement.Add(new XElement("item", new XAttribute("name", modelItem), new XAttribute("url", modelUrl)));
            }

            
            data.Document = new XDocument(new XElement("response", servicesElement));
            return data;
        }
    }
}
