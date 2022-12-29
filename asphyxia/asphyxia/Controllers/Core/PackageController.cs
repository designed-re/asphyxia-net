using asphyxia.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace asphyxia.Controllers.Core
{
    [Route("core")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        [HttpPost, XrpcCall("package.list")]
        public ActionResult<EamuseXrpcData> List([FromBody] EamuseXrpcData data)
        {
            data.Document = new XDocument(new XElement("response", new XElement("package", new XAttribute("expire","1200"), new XAttribute("status", "1"))));
            return data;
        }
    }
}
