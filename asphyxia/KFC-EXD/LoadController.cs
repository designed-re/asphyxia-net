using System.Xml.Linq;
using asphyxia.Models;
using asphyxia.Utils;
using asphyxia.Utils.Formatters;
using eAmuseCore.KBinXML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KFC_EXD
{
    [Route("kfc/6")]
    [ApiController]
    public class LoadController(AsphyxiaContext context) : ControllerBase
    {
        [HttpPost, XrpcCall("game.sv6_load")]
        public async Task<ActionResult<EamuseXrpcData>> DataLoad([FromBody] EamuseXrpcData data)
        {
            XElement gameElement = data.Document.Element("call").Element("game");
            string refid = gameElement.Element("refid").Value;

            Card? card = await context.Cards.Include(x=> x.SvProfile).SingleOrDefaultAsync(x =>
                x.RefId == refid);
            if (card.SvProfile?.Name is null)
            {
                Console.WriteLine($"no card data for RefId: {refid}");
                data.Document = new XDocument(new XElement("response", new XElement("game", new XAttribute("status", "0"), new KU8("result", 1))));
                return data;
            }

            data.Document = new XDocument(new XElement("response",
                new XElement("game", new XAttribute("status", 0),
                    new KU8("result", 0),
                    new KStr("name", card.SvProfile.Name),
                    new KStr("code", card.SvProfile.Code),
                    new KStr("sdvx_id", card.SvProfile.Code),
                    new KU32("gamecoin_packet", 0),
                    new KU32("gamecoin_block", 0),
                    new KU16("appeal_id", card.SvProfile.AppealId),
                    new KS32("last_music_id", card.SvProfile.LastMusicId),
                    new KU8("last_music_type", card.SvProfile.LastMusicType),
                    new KU8("sort_type", card.SvProfile.SortType),
                    new KU8("headphone", card.SvProfile.Headphone),
                    new KU32("blaster_energy", card.SvProfile.BlasterEnergy),
                    new KU32("blaster_count", card.SvProfile.BlasterCount),
                    new KU16("extrack_energy", card.SvProfile.ExtrackEnergy),
                    new KS32("hispeed", card.SvProfile.Hispeed),
                    new KU32("lanespeed", card.SvProfile.Lanespeed),
                    new KU8("gauge_option", card.SvProfile.GaugeOption),
                    new KU8("ars_option", card.SvProfile.ArsOption),
                    new KU8("notes_option", card.SvProfile.NotesOption),
                    new KU8("early_late_disp", card.SvProfile.EarlyLateDisp),
                    new KS32("draw_adjust", card.SvProfile.DrawAdjust),
                    new KU8("eff_c_left", card.SvProfile.EffCLeft),
                    new KU8("eff_c_right", card.SvProfile.EffCRight),
                    new KU8("narrow_down", 0), //todo find it out on db
                    new KStr("kac_id",  card.SvProfile.Name),
                    new KS16("skill_level", card.SvProfile.SkillLevel),
                    new KS16("skill_base_id", card.SvProfile.SkillBaseId),
                    new KS16("skill_name_id", card.SvProfile.SkillNameId),
                    new XElement("ea_shop", new KS32("packet_booster", 1), new KBool("blaster_pass_enable", Convert.ToBoolean(card.SvProfile.BlasterPassEnable)), new KU64("blaster_pass_limit_date", card.SvProfile.BlasterPassLimitDate)),
                    new XElement("eaappli", new KS8("relation", 1)),
                    new XElement("cloud", new KS8("relation", 1)),
                    new KS32("block_no", card.SvProfile.Pcb),
                    new XElement("skill"),
                    new XElement("item"), //idk what is this, but maybe in-game items.. like crew? //todo get in db
                    new XElement("param", new KS32("info", 2), new KS32("id", 2), new XElement("param", new XAttribute("__type", "s32"),new XAttribute("__count",7), "0 0 0 0 0 0 0")),
                        new XElement("info", new KS32("type", 6), new KS32("id", 0), new XElement("param", new XAttribute("__type", "s32"), new XAttribute("__count", 1), 0)),
                        new XElement("info", new KS32("type", 6), new KS32("id", 1), new XElement("param", new XAttribute("__type", "s32"), new XAttribute("__count", 1), 0)),
                        new XElement("info", new KS32("type", 6), new KS32("id", 2), new XElement("param", new XAttribute("__type", "s32"), new XAttribute("__count", 1), 0))), //same with up but i dont know what is this really
                    new KU32("play_count", card.SvProfile.PlayCount),
                    new KU32("day_count", card.SvProfile.DayCount),
                    new KU32("today_count", card.SvProfile.TodayCount),
                    new KU32("play_chain", card.SvProfile.PlayChain),
                    new KU32("max_play_chain", card.SvProfile.MaxPlayChain),
                    new KU32("week_count", card.SvProfile.WeekCount),
                    new KU32("week_play_count", card.SvProfile.WeekPlayCount),
                    new KU32("week_chain", card.SvProfile.WeekChain),
                    new KU32("max_week_chain", card.SvProfile.MaxWeekChain)
                ));
            return data;
        }

        [HttpPost, XrpcCall("game.sv6_load_m")]
        public async Task<ActionResult<EamuseXrpcData>> LoadM([FromBody] EamuseXrpcData data)
        {
            XElement gameElement = data.Document.Element("call").Element("game");
            string refid = gameElement.Element("refid").Value;

            Card? card = await context.Cards.Include(x => x.SvProfile).SingleOrDefaultAsync(x =>
                x.RefId == refid);
            if (card.SvProfile is null)
            {
                data.Document = new XDocument(new XElement("response",
                    new XElement("game", new XAttribute("status", 1), new XElement("music"))));
                return data;
            }

            XElement musicElement = new ("music");

            var scores = context.SvScores.Where(x => x.Profile == card.SvProfile.Id);
            foreach (var score in scores)
            {
                XElement infoElement = new("info",
                    new XElement("param", new XAttribute("__type", "u32"), new XAttribute("__count", 21),
                        $"{score.MusicId} {score.Type} {score.Score} {score.Exscore} {score.Clear} {score.Grade} 0 0 {score.ButtonRate} {score.LongRate} {score.VolRate} 0 0 0 0 0 0 0 0 0 0"));
                musicElement.Add(infoElement);
            }

            data.Document = new XDocument(new XElement("response",
                new XElement("game", new XAttribute("status", 0), musicElement)));
            return data;
        }

        [HttpPost, XrpcCall("game.sv6_load_r")]
        public async Task<ActionResult<EamuseXrpcData>> LoadR([FromBody] EamuseXrpcData data)
        {
            data.Document = new XDocument(new XElement("response",
                new XElement("game", new XAttribute("status", 0))));
            return data;
        }

    }
}
