using System.ComponentModel;
using System.Reflection.Metadata;
using System.Xml.Linq;
using luna.Utils;
using eAmuseCore.KBinXML;
using KFC_EXD.Classes;
using luna.Utils.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using luna.Utils.Models;

namespace KFC_EXD
{
    [Route("kfc/6")]
    [ApiController]
    public class CommonController(AsphyxiaContext context, HostConfig config) : ControllerBase
    {
        [HttpPost, XrpcCall("game.sv6_common")]
        public async Task<ActionResult<EamuseXrpcData>> GetCommon([FromBody] EamuseXrpcData data)
        {
            string model = data.Document.Element("call").Attribute("model").Value.Split(':')[4];
            Console.WriteLine($"Exceed common request: {model}");

            // Load events from database
            var enabledEvents = await context.SvEvents
                .Where(e => e.Enabled)
                .Select(e => e.Event)
                .ToListAsync();

            // data preparation
            Valgene valgeneData = JsonConvert.DeserializeObject<Valgene>(
                System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Data", "exd_valgene.json")));
            Course[] courseData = JsonConvert.DeserializeObject<Course[]>(
                System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Data", "exd_course.json")));
            JObject megamixData = JObject.Parse(
                System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Data", "exd_megamix.json")));
            CurrentArena currentArenaData = JsonConvert.DeserializeObject<CurrentArena>(
                System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Data",
                    "current_arena.json")));
            JObject arenaData = JObject.Parse(
                System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Data",
                    "exd_arena.json")));



            XElement gameElement = new XElement("game", new XAttribute("status", "0"));



            XElement valgeneElement = new XElement("valgene");
            foreach (var info in valgeneData.Info)
            {
                var element = new XElement("info", new KStr("valgene_name", info.ValgeneName), new KStr(
                    "valgene_name_english", info.ValgeneNameEnglish), new KS32("valgene_id", Convert.ToInt32(info.ValgeneId)));
                valgeneElement.Add(element);
            }
            foreach (var catalog in valgeneData.Catalog)
            {
                
                foreach (var catalogItem in catalog.Items)
                {

                    foreach (var ids in catalogItem.ItemIds)
                    {
                        var element = new XElement("catalog", new KS32("valgene_id", Convert.ToInt32(catalog.Volume)));
                        var itemElement = new KS32("item_type", Convert.ToInt32(catalogItem.Type));
                        var itemIdElement = new KS32("item_id", Convert.ToInt32(ids));
                        var rarity = new KS32("rarity",
                            Convert.ToInt32(valgeneData.Rarity[catalogItem.Type.ToString()]));

                        element.Add(itemElement);
                        element.Add(itemIdElement);
                        element.Add(rarity);
                        valgeneElement.Add(element);
                    }

                }
            }

            gameElement.Add(valgeneElement);

            XElement courseElement = new XElement("skill_course");

            var trimedVersion = string.Join("",model.Take(8));
            var currentVersion = int.Parse(trimedVersion);
            string rawModel = data.Document.Element("call")?.Attribute("model")?.Value ?? "";
            string[] modelParts = rawModel.Split(':');
            string cabType = modelParts.Length > 2 ? modelParts[2] : "A"; // 기본값 A

            // thank you gemini!

            foreach (var s in courseData)
            {
                if (currentVersion >= s.Version)
                {
                    void AddCoursesToXml(CourseElement[] courses, short skillType)
                    {
                        foreach (var c in courses)
                        {
                            XElement info = new XElement("info");

                            info.Add(new KS32("season_id", s.Id));
                            info.Add(new KStr("season_name", s.Name));
                            info.Add(new KBool("season_new_flg",  s.IsNew == 1));
                            info.Add(new KS16("course_type", c.Type));
                            info.Add(new KS16("course_id", c.Id));
                            info.Add(new KStr("course_name", c.Name));
                            info.Add(new KS16("skill_level", c.Level));
                            info.Add(new KS16("skill_type", skillType));
                            info.Add(new KS16("skill_name_id",  c.NameId));
                            info.Add(new KBool("matching_assist", c.Assist == 1));
                            info.Add(new KS32("clear_rate",5000));
                            info.Add(new KU32("avg_score", 15000000));

                            foreach (var t in c.Tracks)
                            {
                                XElement track = new XElement("track");
                                track.Add(new KS16("track_no", (short)t.No));
                                track.Add(new KS32("music_id",  (int)t.Mid));
                                track.Add(new KS8("music_type", (sbyte)t.Mty));
                                info.Add(track);
                            }

                            courseElement.Add(info);
                        }
                    }

                    AddCoursesToXml(s.Courses, 0);

                    if ((cabType == "G" || cabType == "H") && s.HasGod == 1 && currentVersion >= 20230530)
                    {
                        AddCoursesToXml(s.Courses, s.HasGod);
                    }
                }
            }

            gameElement.Add(courseElement);


            XElement eventElement = new XElement("event");
            foreach (var @event in enabledEvents)
            {
                XElement infoElement = new XElement("info", new XElement("event_id", new XAttribute("__type", "str"), @event));
                eventElement.Add(infoElement);
            }


            XElement arenaElement = new XElement("arena");
            if (config.ArenaOpen && currentVersion >= 20220425 && currentArenaData.Season != 0)
            {
                Console.WriteLine("!!! ARENA OPEN !!!");
                arenaElement.Add(new KS32("season", currentArenaData.Season));
                arenaElement.Add(new KS32("rule", currentArenaData.Rule));
                arenaElement.Add(new KS32("rank_match_target", currentArenaData.RankMatchTarget));
                arenaElement.Add(new KU64("time_start", (ulong)currentArenaData.TimeStart.ToUnixTimeMilliseconds()));
                arenaElement.Add(new KU64("time_end", (ulong)currentArenaData.TimeEnd.ToUnixTimeMilliseconds()));
                arenaElement.Add(new KU64("shop_start", (ulong)currentArenaData.ShopStart.ToUnixTimeMilliseconds()));
                arenaElement.Add(new KU64("shop_end", (ulong)currentArenaData.ShopEnd.ToUnixTimeMilliseconds()));

                bool isOpen = config.ArenaOpen && 
                             DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() < (long)currentArenaData.TimeEnd.ToUnixTimeMilliseconds();
                
                bool shopOpen = config.ArenaOpen &&
                               DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() >= (long)currentArenaData.ShopStart.ToUnixTimeMilliseconds() &&
                               DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() < (long)currentArenaData.ShopEnd.ToUnixTimeMilliseconds();

                arenaElement.Add(new KBool("is_open", isOpen));
                arenaElement.Add(new KBool("is_shop", shopOpen));
                // Process catalog if shop is open
                if (shopOpen && config.ArenaSession != 0 && arenaData["Set "+config.ArenaSession] != null)
                {
                    var stationData = arenaData["Set " + config.ArenaSession];
                    int stationVersion = stationData["version"]?.Value<int>() ?? 0;

                    if (currentVersion >= stationVersion)
                    {
                        if (stationData["items"] is JArray items)
                        {
                            foreach (var itemArray in items)
                            {
                                if (itemArray is JArray { Count: >= 6 } itemElements)
                                {
                                    arenaElement.Add(new XElement("catalog",
                                        new KS32("catalog_id", itemElements[0].Value<int>()),
                                        new KS32("catalog_type", itemElements[1].Value<int>()),
                                        new KS32("price", itemElements[2].Value<int>()),
                                        new KS32("item_type", itemElements[3].Value<int>()),
                                        new KS32("item_id", itemElements[4].Value<int>()),
                                        new KS32("param", itemElements[5].Value<int>())
                                    ));
                                }
                            }
                        }
                    }
                }
            }

            gameElement.Add(arenaElement);


            gameElement.Add(eventElement);
            XElement extendElement = new XElement("extend");

            uint ii = 91;
            for (int i = 1; i <=4; i++)
            {
                var megamix = megamixData[$"megamix{i}"];

                XElement infoElement = new XElement("info",
                    new KU32("extend_id", ii++),
                    new KU32("extend_type", 17),
                    new KS32("param_num_1", 0),
                    new KS32("param_num_2", 0),
                    new KS32("param_num_3", 0),
                    new KS32("param_num_4", 0),
                    new KS32("param_num_5", 0),
                    new KStr("param_str_1", $"{string.Join(',',megamix.Values<int>())}"),
                    new KStr("param_str_2", ""),
                    new KStr("param_str_3", ""),
                    new KStr("param_str_4", ""),
                    new KStr("param_str_5", "")
                    );
                extendElement.Add(infoElement);
            }

            var time = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
            XElement notiElement = new XElement("info",
                new KU32("extend_id", 1),
                new KU32("extend_type", 1),
                new KS32("param_num_1", 1),
                new KS32("param_num_2", Convert.ToInt32(time/100000)),
                new KS32("param_num_3", 0),
                new KS32("param_num_4", 0),
                new KS32("param_num_5", 0),
                new KStr("param_str_1", $"[f:0] NOTIFICATION\nFREE SOFTWARE\n{DateTime.Now:s}"),
                new KStr("param_str_2", ""),
                new KStr("param_str_3", ""),
                new KStr("param_str_4", ""),
                new KStr("param_str_5", ""));

            extendElement.Add(notiElement);
            

            gameElement.Add(extendElement);

            XElement musicLimitedElement = new XElement("music_limited");

            int lastSongId = context.SvMusics.OrderBy(x=> x.Id).Last().Id;

            if (config.UnlockAllSongs)
            {
                for (int i = 1; i <= lastSongId; i++) //for unlock all songs
                {

                    XElement infoElement = new XElement("info",
                        new XElement("music_id", new XAttribute("__type", "s32"), i),
                        new XElement("music_type", new XAttribute("__type", "u8"), 0),
                        new XElement("limited", new XAttribute("__type", "u8"), 3));

                    musicLimitedElement.Add(infoElement);
                    infoElement = new XElement("info",
                        new XElement("music_id", new XAttribute("__type", "s32"), i),
                        new XElement("music_type", new XAttribute("__type", "u8"), 1),
                        new XElement("limited", new XAttribute("__type", "u8"), 3));

                    musicLimitedElement.Add(infoElement);
                    infoElement = new XElement("info",
                        new XElement("music_id", new XAttribute("__type", "s32"), i),
                        new XElement("music_type", new XAttribute("__type", "u8"), 2),
                        new XElement("limited", new XAttribute("__type", "u8"), 3));

                    musicLimitedElement.Add(infoElement);
                    infoElement = new XElement("info",
                        new XElement("music_id", new XAttribute("__type", "s32"), i),
                        new XElement("music_type", new XAttribute("__type", "u8"), 3),
                        new XElement("limited", new XAttribute("__type", "u8"), 3));

                    musicLimitedElement.Add(infoElement);
                    infoElement = new XElement("info",
                        new XElement("music_id", new XAttribute("__type", "s32"), i),
                        new XElement("music_type", new XAttribute("__type", "u8"), 4),
                        new XElement("limited", new XAttribute("__type", "u8"), 3));

                    musicLimitedElement.Add(infoElement);

                }
            }
            else
            {
                var diffName = new[] { "novice", "advanced", "exhaust", "infinite", "maximum", "ultimate" };
                var licensedSongs = new int[] {};
                var valkyrieSongs = new int[] {};
                
                Console.WriteLine(lastSongId);
                
                for (int i = 0; i <= lastSongId; i++)
                {
                    var songData = context.SvMusics.FirstOrDefault(s => s.Id == i);
                    
                    if (songData != null)
                    {
                        int limitedNo = 2;
                        
                        if (Math.Abs(currentVersion) == 6)
                        {
                            // int songVersion = int.Parse(songData["info"]?["version"]?.Value<string>() ?? "0");
                            // bool hasDistributionDate = songData["info"]?["distribution_date"] != null;
                            //
                            // if (songVersion <= 6 && hasDistributionDate)
                            // {
                            //     int distributionDate = int.Parse(songData["info"]["distribution_date"].Value<string>());
                            //     int currentYMDDate = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                            //     if (distributionDate > currentYMDDate)
                            //     {
                            //         Console.WriteLine($"Unreleased song: {songData["info"]?["title_name"]}");
                            //         continue;
                            //     }
                            // }
                            
                            limitedNo = 2;
                            
                            // if (songData["info"]?["version"]?.Value<string>() == "6" || true)
                            // {
                                if (licensedSongs.Contains(i))
                                    limitedNo += 1;
                                else if (valkyrieSongs.Contains(i) && !System.Text.RegularExpressions.Regex.IsMatch(cabType, @"^(G|H)$"))
                                    limitedNo -= 1;
                                
                                if (i == 2034)
                                    limitedNo = 2;
                                
                                for (int j = 0; j < 6; j++)
                                {
                                    // if (songData["difficulty"]?[diffName[j]]?.Value<string>() != "0")
                                    // {
                                        var infoElement = new XElement("info",
                                            new XElement("music_id", new XAttribute("__type", "s32"), i),
                                            new XElement("music_type", new XAttribute("__type", "u8"), j),
                                            new XElement("limited", new XAttribute("__type", "u8"), limitedNo));
                                        musicLimitedElement.Add(infoElement);
                                    // }
                                }
                            // }
                            // else if (songData["info"]?["inf_ver"]?.Value<string>() == "6")
                            // {
                            //     if (i == 469)
                            //         limitedNo = 2;
                            //     
                            //     var infoElement = new XElement("info",
                            //         new XElement("music_id", new XAttribute("__type", "s32"), i),
                            //         new XElement("music_type", new XAttribute("__type", "u8"), 3),
                            //         new XElement("limited", new XAttribute("__type", "u8"), limitedNo));
                            //     musicLimitedElement.Add(infoElement);
                            // }
                        }
                        
                        // Licensed songs released prior to current version
                        // if (int.Parse(songData["info"]?["version"]?.Value<string>() ?? "0") < Math.Abs(currentVersion) && licensedSongs.Contains(i))
                        // {
                        //     limitedNo += 1;
                        //     for (int j = 0; j < 6; j++)
                        //     {
                        //         if (songData["difficulty"]?[diffName[j]]?.Value<string>() != "0")
                        //         {
                        //             var infoElement = new XElement("info",
                        //                 new XElement("music_id", new XAttribute("__type", "s32"), i),
                        //                 new XElement("music_type", new XAttribute("__type", "u8"), j),
                        //                 new XElement("limited", new XAttribute("__type", "u8"), limitedNo));
                        //             musicLimitedElement.Add(infoElement);
                        //         }
                        //     }
                        // }
                    }
                }
            }



                gameElement.Add(musicLimitedElement);

            XElement skillElement = new XElement("skill_course");
            XElement skillInfoElement = new XElement("info",
                new XElement("season_id", new XAttribute("__type", "s32"), 1),
                new XElement("season_name", new XAttribute("__type", "str"), "SKILL ANALYZER DESIGNED A"),
                new XElement("season_new_flg", new XAttribute("__type", "bool"), 1),
                new XElement("course_type", new XAttribute("__type", "s16"), 0),
                new XElement("course_id", new XAttribute("__type", "s16"), 1),
                new XElement("course_name", new XAttribute("__type", "str"), "SKILL ANALYZER DESIGNED.01"),
                new XElement("skill_level", new XAttribute("__type", "s16"), 1),
                new XElement("skill_name_id", new XAttribute("__type", "s16"), 1),
                new XElement("matching_assist", new XAttribute("__type", "bool"), 0),
                new XElement("clear_rate", new XAttribute("__type", "s32"), 5000),
                new XElement("avg_score", new XAttribute("__type", "u32"), 15000000),
                new XElement("track", new XElement("track_no", new XAttribute("__type", "s16"), 0),
                    new XElement("music_id", new XAttribute("__type", "s32"), 1383),
                    new XElement("music_type", new XAttribute("__type", "s8"), 0)),
                new XElement("track", new XElement("track_no", new XAttribute("__type", "s16"), 1),
                    new XElement("music_id", new XAttribute("__type", "s32"), 334),
                    new XElement("music_type", new XAttribute("__type", "s8"), 1)),
                new XElement("track", new XElement("track_no", new XAttribute("__type", "s16"), 2),
                    new XElement("music_id", new XAttribute("__type", "s32"), 774),
                    new XElement("music_type", new XAttribute("__type", "s8"), 1))
                );

            skillElement.Add(skillInfoElement);

            gameElement.Add(skillElement);

            XDocument document = new XDocument(new XElement("response", gameElement));
            data.Document = document;
            GC.Collect();
            return data;
        }
    }
}
