using System.Xml.Linq;
using eAmuseCore.KBinXML;
using luna.Utils;
using luna.Utils.Formatters;
using luna.Utils.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KFC_EXD
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

            var record1 = await _context.SvScores.SingleOrDefaultAsync(x =>
                x.Profile == card.SvProfile.Id && x.MusicId == musicId && x.Type == musicType);

            if (record1 is null)
                _context.SvScores.Add(record);
            else
                _context.SvScores.Update(record);

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
            Console.WriteLine(gameElement);
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

            // profile += byte.Parse(gameElement.Element("earned_gamecoin_packet").Value);
            profile.Pcb += int.Parse(gameElement.Element("earned_gamecoin_block").Value);
            profile.BlasterEnergy += uint.Parse(gameElement.Element("earned_blaster_energy").Value);
            profile.PlayCount++;
            profile.DayCount++;
            profile.TodayCount++;
            profile.PlayChain++;
            profile.MaxPlayChain++;
            profile.WeekChain++;
            profile.WeekCount++;
            profile.WeekPlayCount++;
            profile.MaxWeekChain++;
            //todo null debug 

            var courseElement = gameElement.Element("course");
            if (courseElement is not null)
            {
                if (short.TryParse(courseElement.Element("ssnid")?.Value, out short seriesId) &&
                    short.TryParse(courseElement.Element("crsid")?.Value, out short courseId))
                {
                    int courseScore = int.Parse(courseElement.Element("sc")?.Value ?? "0");
                    short courseClear = short.Parse(courseElement.Element("ct")?.Value ?? "0");
                    short courseGrade = short.Parse(courseElement.Element("gr")?.Value ?? "0");
                    short courseRate = short.Parse(courseElement.Element("ar")?.Value ?? "0");

                    var courseRecord = await _context.SvCourseRecords.SingleOrDefaultAsync(x =>
                        x.Profile == profile.Id && x.SeriesId == seriesId && x.CourseId == courseId);

                    if (courseRecord is null)
                    {
                        _context.SvCourseRecords.Add(new()
                        {
                            Profile = profile.Id,
                            SeriesId = seriesId,
                            CourseId = courseId,
                            Version = 0,
                            Score = courseScore,
                            Clear = courseClear,
                            Grade = courseGrade,
                            Rate = courseRate,
                            Count = 1
                        });
                    }
                    else
                    {
                        courseRecord.Score = Math.Max(courseScore, courseRecord.Score);
                        courseRecord.Clear = Math.Max(courseClear, courseRecord.Clear);
                        courseRecord.Grade = Math.Max(courseGrade, courseRecord.Grade);
                        courseRecord.Rate = Math.Max(courseRate, courseRecord.Rate);
                        courseRecord.Count++;
                        _context.SvCourseRecords.Update(courseRecord);
                    }
                }
            }

            var items = gameElement.Element("item").Elements("info");

            foreach (var item in items)
            {
                var id = uint.Parse(item.Element("id").Value);
                var itemType = byte.Parse(item.Element("type").Value);
                var param = uint.Parse(item.Element("param").Value);

                var record = await _context.SvItems.AsNoTracking().SingleOrDefaultAsync(x =>x.Profile == profile.Id && x.ItemId == id && x.Type == itemType);

                if (record is null)
                {
                    await _context.SvItems.AddAsync(new() { ItemId = id, Param = param, Type = itemType, Profile =profile.Id });
                }
                else
                {
                    _context.SvItems.Update(new() { Id = record.Id, ItemId = id, Param = param, Type = itemType, Profile = profile.Id });
                }

            }


            var svParams = gameElement.Element("param").Elements("info");

            foreach (var item in svParams)
            {
                var id = int.Parse(item.Element("id").Value);
                var itemType = int.Parse(item.Element("type").Value);
                var param1 = string.Join(' ',item.Element("param").Value.Split(' ').Select(int.Parse));
            
                var record = await _context.SvParams.AsNoTracking().SingleOrDefaultAsync(x => x.Profile == profile.Id && x.ParamId == id && x.Type == itemType);
            
                if (record is null)
                {
                    await _context.SvParams.AddAsync(new() { ParamId = id, Param = param1, Type = itemType, Profile = profile.Id });
                }
                else
                {
                    _context.SvParams.Update(new() { Id = record.Id, ParamId = id, Param = param1, Type = itemType, Profile = profile.Id });
                }
            
            }       
            //todo save skill, arena, variant power

            await _context.SaveChangesAsync();

            gameElement = new("game", new XAttribute("status", 0));
            responseElement.Add(gameElement);
            data.Document = new(responseElement);
            return data;
        }

        [HttpPost, XrpcCall("game.sv6_save_c")] //for course saving
        public async Task<ActionResult<EamuseXrpcData>> SaveC([FromBody] EamuseXrpcData data)
        {
            XElement gameElement = data.Document.Element("call").Element("game");
            XElement responseElement = new("response");

            string refId = gameElement.Element("refid").Value;
            Card? card = await _context.Cards.Include(x => x.SvProfile).SingleOrDefaultAsync(x => x.RefId == refId);

            if (card is null)
            {
                gameElement = new("game", new XAttribute("status", 1));
                responseElement.Add(gameElement);
                data.Document = new(responseElement);
                return data;
            }

            XElement? courseElement = gameElement.Element("course");
            if (courseElement is null)
            {
                gameElement = new("game", new XAttribute("status", 1));
                responseElement.Add(gameElement);
                data.Document = new(responseElement);
                return data;
            }

            if (!short.TryParse(courseElement.Element("ssnid")?.Value, out short seriesId) ||
                !short.TryParse(courseElement.Element("crsid")?.Value, out short courseId))
            {
                gameElement = new("game", new XAttribute("status", 1));
                responseElement.Add(gameElement);
                data.Document = new(responseElement);
                return data;
            }

            int score = int.Parse(courseElement.Element("sc")?.Value ?? "0");
            short clear = short.Parse(courseElement.Element("ct")?.Value ?? "0");
            short grade = short.Parse(courseElement.Element("gr")?.Value ?? "0");
            short rate = short.Parse(courseElement.Element("ar")?.Value ?? "0");

            var record = await _context.SvCourseRecords.SingleOrDefaultAsync(x =>
                x.Profile == card.SvProfile.Id && x.SeriesId == seriesId && x.CourseId == courseId);

            if (record is null)
            {
                _context.SvCourseRecords.Add(new()
                {
                    Profile = card.SvProfile.Id,
                    SeriesId = seriesId,
                    CourseId = courseId,
                    Version = 0,
                    Score = score,
                    Clear = clear,
                    Grade = grade,
                    Rate = rate,
                    Count = 1
                });
            }
            else
            {
                record.Score = Math.Max(score, record.Score);
                record.Clear = Math.Max(clear, record.Clear);
                record.Grade = Math.Max(grade, record.Grade);
                record.Rate = Math.Max(rate, record.Rate);
                record.Count++;
                _context.SvCourseRecords.Update(record);
            }

            await _context.SaveChangesAsync();

            gameElement = new("game", new XAttribute("status", 0));
            responseElement.Add(gameElement);
            data.Document = new(responseElement);
            return data;
        }

        [HttpPost, XrpcCall("game.sv6_save_valgene")] //for valgene item saving
        public async Task<ActionResult<EamuseXrpcData>> SaveValgene([FromBody] EamuseXrpcData data)
        {
            XElement gameElement = data.Document.Element("call").Element("game");
            XElement responseElement = new("response");

            string refId = gameElement.Element("refid").Value;
            Card? card = await _context.Cards.Include(x => x.SvProfile).SingleOrDefaultAsync(x => x.RefId == refId);

            if (card is null)
            {
                gameElement = new("game", new XAttribute("status", 1));
                responseElement.Add(gameElement);
                data.Document = new(responseElement);
                return data;
            }

            var itemElements = gameElement.Element("item")?.Elements("info");
            if (itemElements is null)
            {
                gameElement = new("game", new XAttribute("status", 1));
                responseElement.Add(gameElement);
                data.Document = new(responseElement);
                return data;
            }

            bool useTicket = gameElement.Element("use_ticket") is not null && 
                           gameElement.Element("use_ticket").Value == "1";

            var itemsToAdd = new List<(int type, uint id, uint param)>();

            foreach (var itemElement in itemElements)
            {
                int type = int.Parse(itemElement.Element("type")?.Value ?? "0");
                int id = int.Parse(itemElement.Element("id")?.Value ?? "0");
                uint param = uint.Parse(itemElement.Element("param")?.Value ?? "0");

                if (type == 17)
                {
                    // Special handling for stamp items: create 4 entries
                    for (int stampId = (id * 4) - 3; stampId <= (id * 4); stampId++)
                    {
                        itemsToAdd.Add((type, Convert.ToUInt32(stampId), param));
                    }
                }
                else
                {
                    itemsToAdd.Add((type, Convert.ToUInt32(id), param));
                }
            }

            foreach (var (type, id, param) in itemsToAdd)
            {
                var item = await _context.SvItems.SingleOrDefaultAsync(x =>
                    x.Profile == card.SvProfile.Id && x.ItemId == id && x.Type == type);

                if (item is null)
                {
                    _context.SvItems.Add(new()
                    {
                        ItemId = id,
                        Param = param,
                        Type = (byte)type,
                        Profile = card.SvProfile.Id
                    });
                }
                else
                {
                    item.Param = param;
                    _context.SvItems.Update(item);
                }
            }

            if (useTicket)
            {
                var ticket = await _context.SvValgeneTickets.SingleOrDefaultAsync(x =>
                    x.Profile == card.SvProfile.Id);

                if (ticket is not null)
                {
                    ticket.TicketNum--;
                    _context.SvValgeneTickets.Update(ticket);
                }
            }

            await _context.SaveChangesAsync();

            var resultTicket = await _context.SvValgeneTickets.SingleOrDefaultAsync(x =>
                x.Profile == card.SvProfile.Id);

            var resultElement = new XElement("game", new XAttribute("status", 0),
                new KS32("result", 1));

            if (resultTicket is not null)
            {
                resultElement.Add(new KS32("ticket_num", resultTicket.TicketNum));
                resultElement.Add(new KU64("limit_date", resultTicket.LimitDate));
            }

            responseElement.Add(resultElement);
            data.Document = new(responseElement);
            return data;
        }
    }
}
