using System.Xml.Linq;
using asphyxia.Formatters;
using asphyxia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asphyxia.Controllers.KFC._6
{
    [Route("kfc/6")]
    [ApiController]
    public class LoadController : ControllerBase
    {

        private readonly AsphyxiaContext _context;

        public LoadController(AsphyxiaContext context)
        {
            _context = context;
        }

        [HttpPost, XrpcCall("game.sv6_load")]
        public async Task<ActionResult<EamuseXrpcData>> DataLoad([FromBody] EamuseXrpcData data)
        {
            XElement gameElement = data.Document.Element("call").Element("game");
            string refid = gameElement.Element("refid").Value;

            Card? card = await _context.Cards.Include(x=> x.SvProfile).SingleOrDefaultAsync(x =>
                x.RefId == refid);
            if (card.SvProfile?.Name is null)
            {
                Console.WriteLine($"no card data for RefId: {refid}");
                data.Document = new XDocument(new XElement("response", new XElement("game", new XAttribute("status", "0"), new XElement("result", new XAttribute("__type", "u8"), 1))));
                return data;
            }

            data.Document = new XDocument(new XElement("response",
                new XElement("game", new XAttribute("status", 0),
                    new XElement("result", new XAttribute("__type", "u8"), 0),
                    new XElement("name", new XAttribute("__type", "str"), card.SvProfile.Name),
                    new XElement("code", new XAttribute("__type", "str"), card.SvProfile.Code),
                    new XElement("sdvx_id", new XAttribute("__type", "str"), card.SvProfile.Code),
                    new XElement("gamecoin_packet", new XAttribute("__type", "u32"), 0),
                    new XElement("gamecoin_block", new XAttribute("__type", "u32"), 0),
                    new XElement("appeal_id", new XAttribute("__type", "u16"), card.SvProfile.AppealId),
                    new XElement("last_music_id", new XAttribute("__type", "s32"), card.SvProfile.LastMusicId),
                    new XElement("last_music_type", new XAttribute("__type", "u8"), card.SvProfile.LastMusicType),
                    new XElement("sort_type", new XAttribute("__type", "u8"), card.SvProfile.SortType),
                    new XElement("headphone", new XAttribute("__type", "u8"), card.SvProfile.Headphone),
                    new XElement("blaster_energy", new XAttribute("__type", "u32"), card.SvProfile.BlasterEnergy),
                    new XElement("blaster_count", new XAttribute("__type", "u32"), card.SvProfile.BlasterCount),
                    new XElement("extrack_energy", new XAttribute("__type", "u16"), card.SvProfile.ExtrackEnergy),
                    new XElement("hispeed", new XAttribute("__type", "s32"), card.SvProfile.Hispeed),
                    new XElement("lanespeed", new XAttribute("__type", "u32"), card.SvProfile.Lanespeed),
                    new XElement("gauge_option", new XAttribute("__type", "u8"), card.SvProfile.GaugeOption),
                    new XElement("ars_option", new XAttribute("__type", "u8"), card.SvProfile.ArsOption),
                    new XElement("notes_option", new XAttribute("__type", "u8"), card.SvProfile.NotesOption),
                    new XElement("early_late_disp", new XAttribute("__type", "u8"), card.SvProfile.EarlyLateDisp),
                    new XElement("draw_adjust", new XAttribute("__type", "s32"), card.SvProfile.DrawAdjust),
                    new XElement("eff_c_left", new XAttribute("__type", "u8"), card.SvProfile.EffCLeft),
                    new XElement("eff_c_right", new XAttribute("__type", "u8"), card.SvProfile.EffCRight),
                    new XElement("narrow_down", new XAttribute("__type", "u8"), 0), //todo find it out on db
                    new XElement("kac_id", new XAttribute("__type", "str"), card.SvProfile.Name),
                    new XElement("skill_level", new XAttribute("__type", "s16"), card.SvProfile.SkillLevel),
                    new XElement("skill_base_id", new XAttribute("__type", "s16"), card.SvProfile.SkillBaseId),
                    new XElement("skill_name_id", new XAttribute("__type", "s16"), card.SvProfile.SkillNameId),
                    new XElement("ea_shop", new XElement("packet_booster", new XAttribute("__type", "s32"), 1), new XElement("blaster_pass_enable", new XAttribute("__type", "bool"), card.SvProfile.BlasterPassEnable), new XElement("blaster_pass_limit_date", new XAttribute("__type", "u64"), card.SvProfile.BlasterPassLimitDate)),
                    new XElement("eaappli", new XElement("relation", new XAttribute("__type", "s8"), 1)),
                    new XElement("cloud", new XElement("relation", new XAttribute("__type", "s8"), 1)),
                    new XElement("block_no", new XAttribute("__type", "s32"), card.SvProfile.Pcb),
                    new XElement("skill"),
                    new XElement("item"), //idk what is this, but maybe in-game items.. like crew? //todo get in db
                    new XElement("param", new XElement("info", new XElement("type", new XAttribute("__type", "s32"), 2), new XElement("id", new XAttribute("__type", "s32"), 2), new XElement("param", new XAttribute("__type", "s32"),new XAttribute("__count",7), "0 0 0 0 0 0 0")),
                        new XElement("info", new XElement("type", new XAttribute("__type", "s32"), 6), new XElement("id", new XAttribute("__type", "s32"), 0), new XElement("param", new XAttribute("__type", "s32"), new XAttribute("__count", 1), 0)),
                        new XElement("info", new XElement("type", new XAttribute("__type", "s32"), 6), new XElement("id", new XAttribute("__type", "s32"), 1), new XElement("param", new XAttribute("__type", "s32"), new XAttribute("__count", 1), 0)),
                        new XElement("info", new XElement("type", new XAttribute("__type", "s32"), 6), new XElement("id", new XAttribute("__type", "s32"), 2), new XElement("param", new XAttribute("__type", "s32"), new XAttribute("__count", 1), 0))), //same with up but i dont know what is this really
                    new XElement("play_count", new XAttribute("__type", "u32"), card.SvProfile.PlayCount),
                    new XElement("day_count", new XAttribute("__type", "u32"), card.SvProfile.DayCount),
                    new XElement("today_count", new XAttribute("__type", "u32"), card.SvProfile.TodayCount),
                    new XElement("play_chain", new XAttribute("__type", "u32"), card.SvProfile.PlayChain),
                    new XElement("max_play_chain", new XAttribute("__type", "u32"), card.SvProfile.MaxPlayChain),
                    new XElement("week_count", new XAttribute("__type", "u32"), card.SvProfile.WeekCount),
                    new XElement("week_play_count", new XAttribute("__type", "u32"), card.SvProfile.WeekPlayCount),
                    new XElement("week_chain", new XAttribute("__type", "u32"), card.SvProfile.WeekChain),
                    new XElement("max_week_chain", new XAttribute("__type", "u32"), card.SvProfile.MaxWeekChain)
                )));
            return data;
        }

        [HttpPost, XrpcCall("game.sv6_load_m")]
        public async Task<ActionResult<EamuseXrpcData>> LoadM([FromBody] EamuseXrpcData data)
        {
            XElement gameElement = data.Document.Element("call").Element("game");
            string refid = gameElement.Element("refid").Value;

            Card? card = await _context.Cards.Include(x => x.SvProfile).SingleOrDefaultAsync(x =>
                x.RefId == refid);
            if (card.SvProfile is null)
            {
                data.Document = new XDocument(new XElement("response",
                    new XElement("game", new XAttribute("status", 1), new XElement("music"))));
                return data;
            }

            XElement musicElement = new ("music");

            var scores = _context.SvScores.Where(x => x.Profile == card.SvProfile.Id);
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
