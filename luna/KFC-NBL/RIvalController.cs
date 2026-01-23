using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using eAmuseCore.KBinXML;
using luna.Utils;
using luna.Utils.Formatters;
using luna.Utils.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KFC_NBL
{
    [Route("kfc/7")]
    [ApiController]
    public class RivalController(AsphyxiaContext context) : ControllerBase
    {
        [HttpPost, XrpcCall("game.sv7_load_r")]
        public async Task<ActionResult<EamuseXrpcData>> LoadRival([FromBody] EamuseXrpcData data)
        {
            XElement entryElement = data.Document.Element("call").Element("game");
            XElement responseElement = new("response");

            try
            {
                string refId = entryElement.Element("refid")?.Value;
                if (string.IsNullOrEmpty(refId))
                {
                    responseElement.Add(new XElement("entry", new XAttribute("status", 1)));
                    data.Document = new(responseElement);
                    return data;
                }

                // Get version information
                int version = Math.Abs(int.Parse(entryElement.Element("version")?.Value ?? "0"));
                string model = data.Document.Element("call")?.Attribute("model")?.Value ?? "";
                string trimedVersion = string.Join("", model.Split(':')[4].Take(8));
                int dVersion = int.Parse(trimedVersion);

                // Get rivals from database (mutual: true, excluding self)
                var rivals = await context.SvRivals
                    .Where(r => r.RefId == refId && r.Mutual && r.Version == version)
                    .ToListAsync();

                // Exclude self from rivals
                rivals = rivals.Where(r => r.RivalRefId != refId).ToList();

                var rivalElements = new List<XElement>();

                for (int i = 0; i < rivals.Count; i++)
                {
                    var rival = rivals[i];
                    var rivalCard = await context.Cards
                        .Include(c => c.SvProfile)
                        .FirstOrDefaultAsync(c => c.RefId == rival.RivalRefId);

                    if (rivalCard?.SvProfile == null)
                        continue;

                    // Get rival's music records
                    var rivalScores = await context.SvScores
                        .Where(s => s.Profile == rivalCard.SvProfile.Id)
                        .ToListAsync();

                    var musicElements = new List<XElement>();

                    foreach (var score in rivalScores)
                    {
                        // Version 2023042500 added exscore to rival data.
                        uint[] param;
                        if (dVersion < 20230425)
                        {
                            param = new uint[] { (uint)score.MusicId, (uint)score.Type, (uint)score.Score, (uint)score.Clear, (uint)score.Grade };
                        }
                        else
                        {
                            param = new uint[] { (uint)score.MusicId, (uint)score.Type, (uint)score.Score, (uint)score.Exscore, (uint)score.Clear, (uint)score.Grade };
                        }

                        musicElements.Add(new XElement("music", new XElement("param", param, new XAttribute("__type", "u32"))));
                    }

                    string rivalCode = ConvertIdToCode(rival.SdvxId);

                    var rivalElement = new XElement("rival",
                        new KS16("no", (short)i),
                        new KStr("seq", rivalCode),
                        new KStr("name", rival.Name ?? rivalCard.SvProfile.Name),
                        new XElement("music", musicElements)
                    );

                    rivalElements.Add(rivalElement);
                }

                var entryResponse = new XElement("entry", new XAttribute("status", 0), rivalElements);
                responseElement.Add(entryResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadRival: {ex.Message}");
                responseElement.Add(new XElement("entry", new XAttribute("status", 1)));
            }

            data.Document = new(responseElement);
            return data;
        }

        private string ConvertIdToCode(int id)
        {
            // Converts a numeric ID to a 4-character code (e.g., 1 -> "0001")
            return id.ToString("D4");
        }
    }
}
