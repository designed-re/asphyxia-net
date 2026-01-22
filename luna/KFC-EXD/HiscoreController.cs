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

namespace KFC_EXD
{
    [Route("kfc/6")]
    [ApiController]
    public class HiscoreController(AsphyxiaContext context) : ControllerBase
    {
        [HttpPost, XrpcCall("game.sv6_hiscore")]
        public async Task<ActionResult<EamuseXrpcData>> Hiscore([FromBody] EamuseXrpcData data)
        {
            XElement responseElement = new("response");
            
            // Get all scores from the database
            var allScores = await context.SvScores
                .Include(x => x.ProfileNavigation)
                .ToListAsync();

            // Get all profiles mapped by their ID
            var profiles = await context.SvProfiles.ToListAsync();
            var profileMap = profiles.ToDictionary(p => p.Id);

            // Group scores by (MusicId, Type) and get the maximum score for each group
            var hiscores = allScores
                .GroupBy(s => new { s.MusicId, s.Type })
                .Select(g => g.OrderByDescending(s => s.Score).FirstOrDefault())
                .Where(s => s != null)
                .ToList();

            // Build the hiscore data
            var hiscoreDataElements = new List<XElement>();

            foreach (var score in hiscores)
            {
                if (score?.ProfileNavigation is null || !profileMap.ContainsKey(score.Profile))
                    continue;

                var profile = score.ProfileNavigation;
                var code = ConvertIdToCode(profile.Id);

                hiscoreDataElements.Add(
                    new XElement("d",
                        new KU32("id", (uint)score.MusicId),
                        new KU32("ty", (uint)score.Type),
                        new KStr("a_sq", code),
                        new KStr("a_nm", profile.Name),
                        new KU32("a_sc", (uint)score.Score),
                        new KStr("l_sq", code),
                        new KStr("l_nm", profile.Name),
                        new KU32("l_sc", (uint)score.Score)
                    )
                );
            }

            var hiscoreElement = new XElement("hiscore", 
                new XAttribute("status", 0),
                new XElement("sc", hiscoreDataElements)
            );

            responseElement.Add(hiscoreElement);
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
