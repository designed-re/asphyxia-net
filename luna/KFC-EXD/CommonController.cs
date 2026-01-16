using System.ComponentModel;
using System.Reflection.Metadata;
using System.Xml.Linq;
using asphyxia.Models;
using asphyxia.Utils;
using asphyxia.Utils.Formatters;
using eAmuseCore.KBinXML;
using KFC_EXD.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KFC_EXD
{
    [Route("kfc/6")]
    [ApiController]
    public class CommonController(AsphyxiaContext context) : ControllerBase
    {
        private readonly string[] events = new[]
        {
            // "DEMOGAME_PLAY",
            // "MATCHING_MODE",
            // "MATCHING_MODE_FREE_IP",
            // "LEVEL_LIMIT_EASING",
            // "ACHIEVEMENT_ENABLE",
            // "APICAGACHADRAW	30",
            // "VOLFORCE_ENABLE",
            // "AKANAME_ENABLE",
            // "PAUSE_ONLINEUPDATE",
            // "CONTINUATION",
            // "TENKAICHI_MODE",
            // "QC_MODE",
            // "KAC_MODE",
            // "APPEAL_CARD_GEN_PRICE	100",
            // "APPEAL_CARD_GEN_NEW_PRICE	200",
            // "APPEAL_CARD_UNLOCK	0,20170914,0,20171014,0,20171116,0,20180201,0,20180607,0,20181206,0,20200326,0,20200611,4,10140732,6,10150431",
            // "FAVORITE_APPEALCARD_MAX	200",
            // "FAVORITE_MUSIC_MAX	200",
            // "EVENTDATE_APRILFOOL",
            // "KONAMI_50TH_LOGO",
            // "OMEGA_ARS_ENABLE",
            // "DISABLE_MONITOR_ID_CHECK",
            // "SKILL_ANALYZER_ABLE", //DISABLED FOR WHAT
            // "BLASTER_ABLE",
            // "STANDARD_UNLOCK_ENABLE",
            // "PLAYERJUDGEADJ_ENABLE",
            // "MIXID_INPUT_ENABLE",
            // "EVENTDATE_ONIGO",
            // "EVENTDATE_GOTT",
            // "GENERATOR_ABLE",
            // "CREW_SELECT_ABLE",
            // "PREMIUM_TIME_ENABLE",
            // "OMEGA_ENABLE	1,2,3,4,5,6,7,8,9",
            // "HEXA_ENABLE	1,2,3,4,5",
            // "MEGAMIX_ENABLE",
            // "VALGENE_ENABLE",
            // "ARENA_ENABLE",
            // "DISP_PASELI_BANNER",



             "DEMOGAME_PLAY",
  "MATCHING_MODE",
  "MATCHING_MODE_FREE_IP",
  "LEVEL_LIMIT_EASING",
  "ACHIEVEMENT_ENABLE",
  "APICAGACHADRAW\t30",
  "VOLFORCE_ENABLE",
  "AKANAME_ENABLE",
  "PAUSE_ONLINEUPDATE",
  "CONTINUATION",
  "TENKAICHI_MODE",
  "QC_MODE",
  "KAC_MODE",
  "DISABLE_MONITOR_ID_CHECK",
  "FAVORITE_APPEALCARD_MAX\t200",
  "FAVORITE_MUSIC_MAX\t200",
  "STANDARD_UNLOCK_ENABLE",
  "PLAYERJUDGEADJ_ENABLE",
  "MIXID_INPUT_ENABLE",
  "DISP_PASELI_BANNER",
  "CHARACTER_IGNORE_DISABLE\t122,123,131,139,140,143,149,160,162,163,164,167,170,174,175",
  "STAMP_IGNORE_DISABLE\t273~312,773~820,993~1032,1245~1284,1469~1508,1585~1632,1633~1672,1737~1776,1777~1816,1897~1936",
  "SUBBG_IGNORE_DISABLE\t166~185,281~346,369~381,419~438,464~482,515~552,595~616,660~673,714~727",
  "BEGINNER_MUSIC_FOLDER\t56,78,80,86,87,91,111,128,134,275,278,180,697,770,769,779,842,948,940,1057,1056,1096,932,1136,1469,1480",
  "BEGINNER_MUSIC_FOLDER\t1471,1758,1753,1739,1867,1866,1860,1857,1903,1904,1859,1863,1856,1864,1865,1916,1917,1914,1915,1918,1960",
  "BEGINNER_MUSIC_FOLDER\t1961,1962,2029,2028,2030,2031,2035,2036,1905,1882,2058,2073,2070,2069,2074,2075,2067,2068,2066,2165,2166",
  "BEGINNER_MUSIC_FOLDER\t2174,2175,2193,2195,2196,2213,2216,2214,2215,2205,2206,2224,2229,2228,2230,2241,2244,2243,2242,2245,2240",
  "BEGINNER_MUSIC_FOLDER\t2251,2252,2220,2221,2289,2288,2291,2287,2290",
  "OMEGA_ENABLE\t1,2,3,4,5,6,7,8,9",
  "OMEGA_ARS_ENABLE",
  "HEXA_ENABLE\t1,2,3,4,5,6,7,8,9,10,11,12",
  "HEXA_OVERDRIVE_ENABLE\t8",
  "SKILL_ANALYZER_ABLE",
  "BLASTER_ABLE",
  "PREMIUM_TIME_ENABLE",
  "MEGAMIX_ENABLE",
  "ARENA_ENABLE",
  "ARENA_LOCAL_TO_ONLINE_ENABLE",
  "ARENA_ALTER_MODE_WINDOW_ENABLE",
  "ARENA_PASS_MATCH_WINDOW_ENABLE",
  "ARENA_VOTE_MODE_ENABLE",
  "ARENA_LOCAL_ULTIMATE_MATCH_ALWAYS",
  "MEGAMIX_BATTLE_MATCH_ENABLE",
  "DISABLED_MUSIC_IN_ARENA_ONLINE",
  "SINGLE_BATTLE_ENABLE",
  "GENERATOR_ABLE",
  "CREW_SELECT_ABLE",
  "VALGENE_ENABLE",
  "PLAYER_RADAR_ENABLE",
  "S_PUC_EFFECT_ENABLE",
  "FAVORITE_CREW_ENABLE",
  "TAMAADV_VALGENE_BONUS_ENABLE",
  "DEMOLOOP_INFORMATION\tdemo_info/250220_generator_pekora_demo.png",
  // "ULTIMATE_MATCH_PLAYABLE_ALWAYS", //todo this two flags leading to crash
  // "OVER_POWER_ENABLE" //todo this two flags leading to crash
        };

        [HttpPost, XrpcCall("game.sv6_common")]
        public async Task<ActionResult<EamuseXrpcData>> GetCommon([FromBody] EamuseXrpcData data)
        {
            Console.WriteLine(data.Document);
            string model = data.Document.Element("call").Attribute("model").Value.Split(':')[4];
            Console.WriteLine($"Exceed common request: {model}");



            // data preparation
            Valgene valgeneData = JsonConvert.DeserializeObject<Valgene>(
                System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "data", "exd_valgene.json")));
            Course[] courseData = JsonConvert.DeserializeObject<Course[]>(
                System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "data", "exd_course.json")));
            JObject megamixData = JObject.Parse(
                System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "data", "exd_megamix.json")));



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
            foreach (var @event in events)
            {
                XElement infoElement = new XElement("info", new XElement("event_id", new XAttribute("__type", "str"), @event));
                eventElement.Add(infoElement);
            }
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
                new KStr("param_str_1", $"[f:0] NOTIFICATION\nTHIS IS FREE SOFTWARE"),
                new KStr("param_str_2", ""),
                new KStr("param_str_3", ""),
                new KStr("param_str_4", ""),
                new KStr("param_str_5", ""));

            extendElement.Add(notiElement);
            

            gameElement.Add(extendElement);

            XElement musicLimitedElement = new XElement("music_limited");

            int lastSongId = context.SvMusics.OrderBy(x=> x.Id).Last().Id;

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
