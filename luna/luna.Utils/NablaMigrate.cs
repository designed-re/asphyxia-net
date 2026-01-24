using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using luna.Utils.Models;
using luna.Utils.Models.sdvx;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace luna.Utils
{
    public class NablaMigrate
    {
        private readonly AsphyxiaContext _context;

        public NablaMigrate(AsphyxiaContext context)
        {
            _context = context;
        }

        public async Task MigrateProfile(string refId)
        {
            try
            {
                // Get version 6 profile
                var card = await _context.Cards
                    .Include(c => c.SvProfile)
                    .FirstOrDefaultAsync(c => c.RefId == refId);

                if (card?.SvProfile == null)
                {
                    Console.WriteLine($"No profile found for RefId: {refId}");
                    return;
                }

                var profileData = card.SvProfile;

                if (profileData.Version == 6)
                {
                    profileData.Version = 7;
                    _context.SvProfiles.Update(card.SvProfile);
                }

                await _context.SaveChangesAsync();
                Console.WriteLine("Migrating profile from EG to กิ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error migrating profile: {ex.Message}");
            }
        }
        public async Task MigrateParams(string refId)
        {
            try
            {
                var card = await _context.Cards
                    .Include(c => c.SvProfile)
                    .FirstOrDefaultAsync(c => c.RefId == refId);

                if (card?.SvProfile == null)
                    return;

                // Get version 6 params
                var v6Params = await _context.SvParams
                    .Where(p => p.Profile == card.SvProfile.Id)
                    .ToListAsync();

                Console.WriteLine("Migrating param data");

                foreach (var param in v6Params)
                {
                    // Special handling for param type 2, id 1
                    if (param.Type == 2 && param.ParamId == 1)
                    {
                        var paramValues = param.Param.Split(' ').Select(int.Parse).ToList();
                        if (paramValues.Count > 24)
                        {
                            paramValues[24] = 0;
                            param.Param = string.Join(' ', paramValues);
                        }
                    }

                    // Update or create version 7 param
                    var existingV7Param = await _context.SvParams
                        .FirstOrDefaultAsync(p => 
                            p.Profile == card.SvProfile.Id && 
                            p.ParamId == param.ParamId && 
                            p.Type == param.Type);

                    if (existingV7Param != null)
                    {
                        existingV7Param.Param = param.Param;
                        _context.SvParams.Update(existingV7Param);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error migrating params: {ex.Message}");
            }
        }

        public async Task MigrateMusic(string refId, string musicDbPath)
        {
            try
            {
                var card = await _context.Cards
                    .Include(c => c.SvProfile)
                    .FirstOrDefaultAsync(c => c.RefId == refId);

                if (card?.SvProfile == null)
                    return;

                // Get version 6 scores
                var v6Scores = await _context.SvScores
                    .Where(s => s.Profile == card.SvProfile.Id)
                    .ToListAsync();

                // Load music database
                var musicDbJson = System.IO.File.ReadAllText(musicDbPath);
                var mdb = JObject.Parse(musicDbJson);
                var musicList = mdb["mdb"]?["music"] as JArray ?? new JArray();

                var diffName = new[] { "novice", "advanced", "exhaust", "infinite", "maximum", "ultimate" };
                var nblClearLamp = new[] { 0, 1, 2, 3, 5, 6, 4 };

                var exScoreResetList = new[]
                {
                    new { id = 360, type = 3 }, new { id = 580, type = 2 }, new { id = 1121, type = 4 }, new { id = 1185, type = 2 },
                    new { id = 1199, type = 4 }, new { id = 1738, type = 4 }, new { id = 2242, type = 0 }
                };

                var levelDifOverride = new[]
                {
                    new { mid = 1, type = 1, lvl = 10 }, new { mid = 18, type = 1, lvl = 8 }, new { mid = 18, type = 2, lvl = 10 },
                    new { mid = 73, type = 2, lvl = 17 }, new { mid = 48, type = 1, lvl = 8 }, new { mid = 75, type = 2, lvl = 12 },
                    new { mid = 124, type = 2, lvl = 16 }, new { mid = 65, type = 1, lvl = 7 }, new { mid = 66, type = 1, lvl = 8 },
                    new { mid = 27, type = 1, lvl = 7 }, new { mid = 27, type = 2, lvl = 12 }, new { mid = 68, type = 1, lvl = 9 },
                    new { mid = 6, type = 1, lvl = 7 }, new { mid = 6, type = 2, lvl = 12 }, new { mid = 16, type = 1, lvl = 7 },
                    new { mid = 2, type = 1, lvl = 10 }, new { mid = 60, type = 3, lvl = 17 }, new { mid = 5, type = 2, lvl = 13 },
                    new { mid = 128, type = 2, lvl = 13 }, new { mid = 9, type = 2, lvl = 1 }, new { mid = 340, type = 2, lvl = 13 },
                    new { mid = 247, type = 3, lvl = 18 }, new { mid = 282, type = 2, lvl = 17 }, new { mid = 288, type = 2, lvl = 13 },
                    new { mid = 699, type = 3, lvl = 18 }, new { mid = 595, type = 2, lvl = 17 }, new { mid = 507, type = 2, lvl = 17 }, 
                    new { mid = 1044, type = 2, lvl = 16 }, new { mid = 948, type = 4, lvl = 16 }, new { mid = 1115, type = 4, lvl = 16 },
                    new { mid = 1215, type = 2, lvl = 15 }, new { mid = 1152, type = 2, lvl = 15 }, new { mid = 1282, type = 3, lvl = 17 },
                    new { mid = 1343, type = 2, lvl = 16 }, new { mid = 1300, type = 3, lvl = 17 }, new { mid = 1938, type = 2, lvl = 18 }
                };

                Console.WriteLine("Migrating music records");

                foreach (var record in v6Scores)
                {
                    // Find song in music database
                    var songData = musicList.FirstOrDefault(s => 
                        s["id"]?.Value<string>() == record.MusicId.ToString());

                    int diffLevel = 0;
                    int exscore = record.Exscore;

                    if (songData != null && record.Type < diffName.Length)
                    {
                        var diffStr = songData["difficulty"]?[diffName[record.Type]]?.Value<int>();
                        if (diffStr.HasValue)
                            diffLevel = diffStr.Value;

                        // Apply level override if exists
                        var lvOverride = levelDifOverride.FirstOrDefault(d => 
                            d.mid == record.MusicId && d.type == record.Type);
                        if (lvOverride != null)
                            diffLevel = lvOverride.lvl;

                        // Check for exscore reset
                        var exscoreOverride = exScoreResetList.FirstOrDefault(d => 
                            d.id == record.MusicId && d.type == record.Type);
                        if (exscoreOverride != null)
                            exscore = 0;
                    }

                    // Update or create version 7 score
                    var existingV7Score = await _context.SvScores
                        .FirstOrDefaultAsync(s => 
                            s.Profile == card.SvProfile.Id && 
                            s.MusicId == record.MusicId && 
                            s.Type == record.Type);

                    if (existingV7Score != null)
                    {
                        existingV7Score.Score = record.Score;
                        existingV7Score.Exscore = exscore;
                        existingV7Score.Clear = record.Clear;
                        existingV7Score.Grade = record.Grade;
                        existingV7Score.ButtonRate = record.ButtonRate;
                        existingV7Score.LongRate = record.LongRate;
                        existingV7Score.VolRate = record.VolRate;
                        _context.SvScores.Update(existingV7Score);
                    }
                    else
                    {
                        var newScore = new SvScore
                        {
                            Profile = card.SvProfile.Id,
                            MusicId = record.MusicId,
                            Type = record.Type,
                            Score = record.Score,
                            Exscore = exscore,
                            Clear = nblClearLamp.Length > record.Clear ? nblClearLamp[record.Clear] : record.Clear,
                            Grade = record.Grade,
                            ButtonRate = record.ButtonRate,
                            LongRate = record.LongRate,
                            VolRate = record.VolRate
                        };
                        _context.SvScores.Add(newScore);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error migrating music: {ex.Message}");
            }
        }

        public async Task MigrateAll(string refId, string musicDbPath)
        {
            try
            {
                Console.WriteLine($"Starting Nabla migration for RefId: {refId}");
                
                await MigrateProfile(refId);
                await MigrateParams(refId);
                await MigrateMusic(refId, musicDbPath);

                Console.WriteLine("Migration completed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during migration: {ex.Message}");
            }
        }

        private double ComputeForce(int diffLevel, int score, int clearType, int grade)
        {
            // Placeholder implementation - adjust based on actual Nabla force calculation
            // This would need to be based on the actual game's force calculation algorithm
            double baseForce = diffLevel * 0.5;
            
            // Bonus for clear type
            if (clearType >= 3)
                baseForce += 1.0;
            
            // Bonus for grade
            if (grade >= 5)
                baseForce += 0.5;

            return baseForce;
        }
    }
}
