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
            /*
             *           <id __type="str">ea</id>
                <country __type="str">AX</country>
                <region __type="str">1</region>
                <name __type="str">CORE</name>
                <type __type="u8">0</type>
                <countryname __type="str">UNKNOWN</countryname>
                <countryjname __type="str">�s��</countryjname>
                <regionname __type="str">CORE</regionname>
                <regionjname __type="str">CORE</regionjname>
                <customercode __type="str">AXUSR</customercode>
                <companycode __type="str">AXCPY</companycode>
                <latitude __type="s32">6666</latitude>
                <longitude __type="s32">6666</longitude>
                <accuracy __type="u8">0</accuracy>
             */
            data.Document = new XDocument(new XElement("response", new XElement("facility",
                new XElement("location",
                    new KStr("id", "ea"),//"53BDC526"),
                    new KStr("country", "JP"),
                    new KStr("region", "1"),
                    new KStr("name", "LUNALIGHT"),
                    new KU8("type", 0),
                    new KStr("countryname", "JAPAN"),
                    new KStr("countryjname", "日本"),
                    new KStr("regionname", "Tokyo"),
                    new KStr("regionjname", "Tokyo"),
                    new KStr("customercode", "LUNA"),
                    new KStr("companycode", "LUNA"),
                    new KS32("latitude", 1273),
                    new KS32("longitude", 363),
                    new KU8("accuracy", 0)
                ),
                new XElement("line",
                    new KStr("id", "F"),
                    new KU8("class", 0)
                ),
                new XElement("portfw",
                    new KIP4("globalip", HttpContext.Connection.RemoteIpAddress),
                    new KU16("globalport", 5700),
                    new KU16("privateport", 5700)
                ),
                new XElement("public",
                    new KU8("flag", 1),
                    new KStr("name", "Test"),
                    new KStr("latitude", "127.3"),
                    new KStr("longitude", "36.3")
                ),
                new XElement("share",
                    new XElement("eacoin",
                        new KS32("notchamount", 0),
                        new KS32("notchcount", 0),
                        new KS32("supplylimit", 100000)
                    ),
                    new XElement("url",
                        new KStr("eapass", "http://eagate.lunalight.place"),
                        new KStr("arcadefan", "http://eagate.lunalight.place"),
                        new KStr("konaminetdx", "http://eagate.lunalight.place"),
                        new KStr("konamiid", "http://eagate.lunalight.place"),
                        new KStr("eagate", "http://eagate.lunalight.place")
                    )
                    /*
                                             new KStr("eapass", "http://p.eagate.573.jp/"),
                       new KStr("arcadefan", "http://p.eagate.573.jp/"),
                       new KStr("konaminetdx", "http://p.eagate.573.jp/"),
                       new KStr("konamiid", "http://p.eagate.573.jp/"),
                       new KStr("eagate", "http://p.eagate.573.jp/")
                     */
                )
            ))); ;

            if (requestedEncoding == "Shift-JIS")
            {
                data.Encoding = Encoding.GetEncoding(932);
            }
            else
            {
                Console.WriteLine($"Unknown encoding requested, ignoring. {requestedEncoding}");
            }

            return data;
        }
    }
}
