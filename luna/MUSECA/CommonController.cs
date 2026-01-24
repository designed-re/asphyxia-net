using eAmuseCore.KBinXML;
using luna.Utils;
using luna.Utils.Formatters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace MUSECA
{
    [Route("pix")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        [HttpPost, XrpcCall("game_3.common")]
        public ActionResult<EamuseXrpcData> Common([FromBody] EamuseXrpcData data)
        {

            string model = data.Document.Element("call").Attribute("model").Value.Split(':')[4];
            Console.WriteLine($"Museca common request: {model}");

            // data preparation
            MusecaMDB mdb = JsonConvert.DeserializeObject<MusecaMDB>(
                System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Data", "museca_mdb.json")));

            XElement gameElement = new XElement("game_3", new XAttribute("status", "0"));

            XElement musicLimitedElement = new XElement("music_limited");
            foreach (var music in mdb.Music)
            {
                XElement infoElement = new XElement("info",
                    new KS32("music_id", music.MusicId.Content[0]),
                    new KU8("music_type", byte.Parse(music.MusicType.Content[0].ToString())),
                    new KU8("limited", 3));

                musicLimitedElement.Add(infoElement);
            }

            gameElement.Add(musicLimitedElement);

            var events = new uint[] {1, 83, 130, 194, 195, 98, 145, 146, 147, 148, 149, 56, 86, 105, 140, 211, 143};

            XElement eventElement = new XElement("event");

            foreach (var @event in events)
            {
                XElement infoElement = new XElement("info",
                    new KU32("event_id", @event));
                
                eventElement.Add(infoElement);
            }

            gameElement.Add(eventElement);


            XDocument document = new XDocument(new XElement("response", gameElement));
            data.Document = document;
            GC.Collect();
            return data;
        }
    }
}
