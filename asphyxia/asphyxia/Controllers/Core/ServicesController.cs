﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using asphyxia.Formatters;
using asphyxia.Utils;
using Microsoft.Extensions.Options;

namespace asphyxia.Controllers.Core
{
    [Route("core")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly HostConfig _config;
        public ServicesController(IOptions<HostConfig> config)
        {
            _config = config.Value;
        }

        [HttpPost, XrpcCall("services.get")]
        public ActionResult<EamuseXrpcData> Get([FromBody] EamuseXrpcData data, [FromQuery] string model)
        {
            Console.WriteLine(data.Document);
            // string url = "http://localhost:8083";
            string url = Request.Scheme + "://" + Request.Host.Host + ":" + (Request.Host.Port ?? 8083);
            string coreUrl = url + "/core";
            string modelUrl;
            string[] modelItems;

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
            };
            if (model.StartsWith("KFC"))
            {
                modelItems = HandlerGenerator.GenerateHandlers("",
                    new[]
                    {
                        "local", "local2", "lobby", "lobby2"
                    });
                modelItems = modelItems.Concat(HandlerGenerator.GenerateHandlers("game.sv6_",
                    new[]
                    {
                        "common", "new", "load", "load_m", "save", "save_m", "save_c", "frozen", "buy", "print",
                        "hiscore", "load_r", "save_ap", "load_ap", "lounge", "shop", "save_e", "save_mega", "play_e",
                        "play_s", "entry_s", "entry_e", "exception"
                    })).ToArray();

                // modelItems = new string[] { };
                modelUrl = url + "/kfc/6";
            }
            else return NotFound();

            XElement servicesElement = new XElement("services",
                new XAttribute("expire", "600"),
                new XAttribute("method", "get"),
                new XAttribute("mode", "operation"),
                new XAttribute("status", "0"));

            foreach (string coreItem in coreItems)
                servicesElement.Add(new XElement("item", new XAttribute("name", coreItem), new XAttribute("url", coreUrl)));

            foreach (string modelItem in modelItems)    
                servicesElement.Add(new XElement("item", new XAttribute("name", modelItem), new XAttribute("url", modelUrl)));

            servicesElement.Add(new XElement("item", new XAttribute("name", "ntp"), new XAttribute("url", "ntp://pool.ntp.org/")));
            servicesElement.Add(new XElement("item", new XAttribute("name", "keepalive"), new XAttribute("url", $"http://127.0.0.1/keepalive?pa=127.0.0.1&ia=127.0.0.1&ga=127.0.0.1&ma=127.0.0.1&t1=2&t2=10")));

            data.Document = new XDocument(new XElement("response", servicesElement));
            Console.WriteLine(data.Document);
            return data;
        }
    }
}
