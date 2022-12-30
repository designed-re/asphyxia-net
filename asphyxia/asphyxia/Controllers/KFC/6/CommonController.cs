using System.ComponentModel;
using System.Reflection.Metadata;
using System.Xml.Linq;
using asphyxia.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asphyxia.Controllers.KFC._6
{
    [Route("kfc/6")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly string[] events = new[]
        {
            "DEMOGAME_PLAY",
            "MATCHING_MODE",
            "MATCHING_MODE_FREE_IP",
            "LEVEL_LIMIT_EASING",
            "ACHIEVEMENT_ENABLE",
            "APICAGACHADRAW	30",
            "VOLFORCE_ENABLE",
            "AKANAME_ENABLE",
            "PAUSE_ONLINEUPDATE",
            "CONTINUATION",
            "TENKAICHI_MODE",
            "QC_MODE",
            "KAC_MODE",
            "APPEAL_CARD_GEN_PRICE	100",
            "APPEAL_CARD_GEN_NEW_PRICE	200",
            "APPEAL_CARD_UNLOCK	0,20170914,0,20171014,0,20171116,0,20180201,0,20180607,0,20181206,0,20200326,0,20200611,4,10140732,6,10150431",
            "FAVORITE_APPEALCARD_MAX	200",
            "FAVORITE_MUSIC_MAX	200",
            "EVENTDATE_APRILFOOL",
            //"KONAMI_50TH_LOGO",
            "OMEGA_ARS_ENABLE",
            "DISABLE_MONITOR_ID_CHECK",
            "SKILL_ANALYZER_ABLE",
            "BLASTER_ABLE",
            "STANDARD_UNLOCK_ENABLE",
            "PLAYERJUDGEADJ_ENABLE",
            "MIXID_INPUT_ENABLE",
            "EVENTDATE_ONIGO",
            "EVENTDATE_GOTT",
            "GENERATOR_ABLE",
            "CREW_SELECT_ABLE",
            "PREMIUM_TIME_ENABLE",
            "OMEGA_ENABLE	1,2,3,4,5,6,7,8,9",
            "HEXA_ENABLE	1,2,3",
            "MEGAMIX_ENABLE",
            "VALGENE_ENABLE",
            "ARENA_ENABLE",
            "DISP_PASELI_BANNER"
        };

        [HttpPost, XrpcCall("game.sv6_common")]
        public async Task<ActionResult<EamuseXrpcData>> GetCommon([FromBody] EamuseXrpcData data)
        {
            Console.WriteLine(data.Document);
            string model = data.Document.Element("call").Attribute("model").Value.Split(':')[4];
            Console.WriteLine($"Exceed common request: {model}");

            XElement gameElement = new XElement("game", new XAttribute("status", "0"));
            XElement eventElement = new XElement("event");
            foreach (var @event in events)
            {
                XElement infoElement = new XElement("info",new XElement("event_id", new XAttribute("__type", "str"), @event));
                eventElement.Add(infoElement);
            }
            gameElement.Add(eventElement);
            gameElement.Add(new XElement("extend"));

            XElement musicLimitedElement = new XElement("music_limited");

            for (int i = 1; i <= 1999; i++) //for unlock all songs
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
