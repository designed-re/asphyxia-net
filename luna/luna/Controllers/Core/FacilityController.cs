using eAmuseCore.KBinXML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Linq;
using luna.Utils;
using luna.Utils.Formatters;
using Microsoft.Extensions.Options;
using luna.Utils.Models;

namespace luna.Controllers.Core
{
    [Route("core")]
    [ApiController]
    public class FacilityController(AsphyxiaContext context, HostConfig config) : ControllerBase
    {
        [HttpPost, XrpcCall("facility.get")]
        public ActionResult<EamuseXrpcData> Get([FromBody] EamuseXrpcData data)
        {
            var facilityReq = data.Document.Element("call").Element("facility");
            var pcbid = data.Document.Element("call").Attribute("srcid").Value;
            string requestedEncoding = facilityReq.Attribute("encoding").Value;
            string method = facilityReq.Attribute("method").Value;

            Facility? destFacility = context.Facilities.SingleOrDefault(x => x.PCBId == pcbid);

            if (destFacility is null && config.EnforcePCBId)
            {
                data.Document = new XDocument(new XElement("response", new XAttribute("status", "400")));
                return data;
            }

            if (destFacility is null)
            {
                destFacility = new Facility()
                {
                    FacilityId = "guest", Country = "JP", Region = "JP", Name = "GUEST", Type = 1, CompanyCode = "guest",
                    CountryJName = "guest", CountryName = "guest", CustomerCode = "guest", RegionJName = "guest",
                    RegionName = "guest"
                };
            }


            data.Document = new XDocument(new XElement("response", new XElement("facility",
                new XElement("location",
                    new KStr("id", destFacility.FacilityId),//"53BDC526"),
                    new KStr("country", destFacility.Country),
                    new KStr("region", destFacility.Region),
                    new KStr("name", destFacility.Name),
                    new KU8("type", byte.Parse(destFacility.Type.ToString())),
            new KStr("countryname", destFacility.CountryName),
            new KStr("countryjname", destFacility.CountryJName),
            new KStr("regionname", destFacility.RegionName),
            new KStr("regionjname", destFacility.RegionJName),
            new KStr("customercode", destFacility.CustomerCode),
            new KStr("companycode", destFacility.CompanyCode),
            new KS32("latitude", 1273),
            new KS32("longitude", 363),
            new KU8("accuracy", 0)
                ),
            new XElement("line",
                new KStr("id", "LIGHTSPEED"),
                new KU8("class", 0)
            ),
            new XElement("portfw",
                new KIP4("globalip", HttpContext.Connection.RemoteIpAddress),
                new KU16("globalport", 5700),
                new KU16("privateport", 5700)
            ),
            new XElement("public",
                new KU8("flag", 1),
                new KStr("name", destFacility.Name),
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
                    )
            )));

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
