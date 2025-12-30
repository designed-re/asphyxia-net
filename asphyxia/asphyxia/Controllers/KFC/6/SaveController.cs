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
    public class SaveController : ControllerBase
    {
        private readonly AsphyxiaContext _context;

        public SaveController(AsphyxiaContext context)
        {
            _context = context;
        }

        [HttpPost, XrpcCall("game.sv6_save_m")] //for score saving
        public async Task<ActionResult<EamuseXrpcData>> SaveM([FromBody] EamuseXrpcData data)
        {
            XElement gameElement = data.Document.Element("call").Element("game");
            XElement trackElement = gameElement.Element("track");
            XElement responseElement = new("response");

            string refId = gameElement.Element("refid").Value;
            Card? card = await _context.Cards.Include(x=> x.SvProfile).SingleOrDefaultAsync(x => x.RefId == refId);

            if (card is null)
            {
                gameElement = new("game", new XAttribute("status", 1));
                responseElement.Add(gameElement);
                data.Document = new(responseElement);
                return data;
            }

            int musicId = int.Parse(trackElement.Element("music_id").Value);
            int musicType = int.Parse(trackElement.Element("music_type").Value);
            int score = int.Parse(trackElement.Element("score").Value);
            int exscore = int.Parse(trackElement.Element("exscore").Value);
            int btn_rate = int.Parse(trackElement.Element("btn_rate").Value);
            int long_rate = int.Parse(trackElement.Element("long_rate").Value);
            int vol_rate = int.Parse(trackElement.Element("vol_rate").Value);
            int clear_type = int.Parse(trackElement.Element("clear_type").Value);
            int score_grade = int.Parse(trackElement.Element("score_grade").Value);

            SvScore record =
                await _context.SvScores.SingleOrDefaultAsync(x => x.Profile == card.SvProfile.Id && x.MusicId == musicId && x.Type == musicType) ?? new()
                {
                    MusicId = musicId, Type = musicType, Score = 0, Exscore = 0, Clear = 0, Grade = 0, ButtonRate = 0,
                    LongRate = 0, VolRate = 0, Profile = card.SvProfile.Id
                };
            if (score > record.Score)
            {
                record.Score = score;
                record.ButtonRate = btn_rate;
                record.LongRate = long_rate;
                record.VolRate = vol_rate;
            }

            if (exscore > record.Exscore) record.Exscore = exscore;

            record.Clear = Math.Max(clear_type, record.Clear);
            record.Grade = Math.Max(score_grade, record.Grade);
            _context.SvScores.Add(record);
            await _context.SaveChangesAsync();

            gameElement = new("game", new XAttribute("status", 0));
            responseElement.Add(gameElement);
            data.Document = new(responseElement);
            return data;
        }

        [HttpPost, XrpcCall("game.sv6_save")] //for saving settings
        public async Task<ActionResult<EamuseXrpcData>> Save([FromBody] EamuseXrpcData data)
        {
            XElement gameElement = data.Document.Element("call").Element("game");
            XElement responseElement = new("response");

            string refId = gameElement.Element("refid").Value;
            Card? card = await _context.Cards.Include(x=> x.SvProfile).SingleOrDefaultAsync(x => x.RefId == refId);

            if (card is null)
            {
                gameElement = new("game", new XAttribute("status", 1));
                responseElement.Add(gameElement);
                data.Document = new(responseElement);
                return data;
            }

            SvProfile profile = card.SvProfile;
            //todo do params

            profile.AppealId = ushort.Parse(gameElement.Element("appeal_id").Value);
            profile.SkillLevel = short.Parse(gameElement.Element("skill_level").Value);
            profile.SkillBaseId = short.Parse(gameElement.Element("skill_base_id").Value);
            profile.SkillNameId = short.Parse(gameElement.Element("skill_name_id").Value);
            profile.Hispeed = int.Parse(gameElement.Element("hispeed").Value);
            profile.Lanespeed = uint.Parse(gameElement.Element("lanespeed").Value);
            profile.GaugeOption = byte.Parse(gameElement.Element("gauge_option").Value);
            profile.ArsOption = byte.Parse(gameElement.Element("ars_option").Value);
            profile.NotesOption = byte.Parse(gameElement.Element("notes_option").Value);
            profile.EarlyLateDisp = byte.Parse(gameElement.Element("early_late_disp").Value);
            profile.DrawAdjust = int.Parse(gameElement.Element("draw_adjust").Value);
            profile.EffCLeft = byte.Parse(gameElement.Element("eff_c_left").Value);
            profile.EffCRight = byte.Parse(gameElement.Element("eff_c_right").Value);
            profile.LastMusicId = int.Parse(gameElement.Element("music_id").Value);
            profile.LastMusicType = byte.Parse(gameElement.Element("music_type").Value);
            profile.SortType = byte.Parse(gameElement.Element("sort_type").Value);
            profile.Headphone = byte.Parse(gameElement.Element("headphone").Value);

            profile.DayCount++;
            profile.PlayCount++;
            profile.TodayCount++;
            profile.WeekPlayCount++;

            profile.Pcb += int.Parse(gameElement.Element("earned_gamecoin_block").Value);


            await _context.SaveChangesAsync();

            gameElement = new("game", new XAttribute("status", 0));
            responseElement.Add(gameElement);
            data.Document = new(responseElement);
            return data;
        }
    }
}
