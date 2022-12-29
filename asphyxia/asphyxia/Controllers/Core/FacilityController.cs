using asphyxia.Formatters;
using eAmuseCore.KBinXML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Linq;

namespace asphyxia.Controllers.Core
{
    [Route("core")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        [HttpPost, XrpcCall("facility.get")]
        public ActionResult<EamuseXrpcData> Get([FromBody] EamuseXrpcData data)
        {
            var facilityReq = data.Document.Element("call").Element("facility");
            string requestedEncoding = facilityReq.Attribute("encoding").Value;
            string method = facilityReq.Attribute("method").Value;

            data.Document = new XDocument(new XElement("response", new XElement("facility",
                new XElement("location",
                    new KStr("id", "53BDC526"),
                    new KStr("country", "KR"),
                    new KStr("region", "1"),
                    new KStr("name", "TestDesigned"),
                    new KU8("type", 0)
                ),
                new XElement("line",
                    new KStr("id", ""),
                    new KU8("class", 0)
                ),
                new XElement("portfw",
                    new KIP4("globalip", HttpContext.Connection.RemoteIpAddress),
                    new KU16("globalport", 5700),
                    new KU16("privateport", 5700)
                ),
                new XElement("public",
                    new KU8("flag", 0),
                    new KStr("name", ""),
                    new KStr("latitude", ""),
                    new KStr("longitude", "")
                ),
                new XElement("share",
                    new XElement("eacoin",
                        new KS32("notchamount", 0),
                        new KS32("notchcount", 0),
                        new KS32("supplylimit", 100000)
                    ),
                    new XElement("url",
                        new KStr("eapass", "http://1eagate.573.jp/"),
                        new KStr("arcadefan", "http://1eagate.573.jp/"),
                        new KStr("konaminetdx", "http://1eagate.573.jp/"),
                        new KStr("konamiid", "http://1eagate.573.jp/"),
                        new KStr("eagate", "http://1eagate.573.jp/")
                    )
                )
            ))); ;

            if (requestedEncoding == "Shift-JIS")
            {
                data.Encoding = Encoding.GetEncoding(932);
            }
            else
            {
                Console.WriteLine("Unknown encoding requested, ignoring.");
            }

            return data;
        }
    }
}
