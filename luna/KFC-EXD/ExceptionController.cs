using luna.Utils;
using luna.Utils.Formatters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace KFC_EXD
{
    [Route("kfc/6")]
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        [HttpPost, XrpcCall("game.sv6_exception")]
        public async Task<ActionResult<EamuseXrpcData>> Exception([FromBody] EamuseXrpcData data)
        {
            XElement responseElement = new("response");
            var gameElement = new XElement("game", new XAttribute("status", 0));
            responseElement.Add(gameElement);
            data.Document = new(responseElement);

            return data;
        }
    }
}
