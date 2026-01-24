using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using eAmuseCore.KBinXML;
using luna.Utils;
using luna.Utils.Formatters;
using luna.Utils.Models;
using luna.Utils.Models.sdvx;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KFC_EXD
{
    [Route("kfc/6")]
    [ApiController]
    public class EntryController(AsphyxiaContext context) : ControllerBase
    {
        [HttpPost, XrpcCall("game.sv6_entry_s")]
        public async Task<ActionResult<EamuseXrpcData>> EntryS([FromBody] EamuseXrpcData data)
        {
            XElement gameElement = data.Document.Element("call");
            XElement entryElement = data.Document.Element("call").Element("game");
            XElement responseElement = new("response");

            try
            {
                // Parse incoming data
                int version = Math.Abs(int.Parse(gameElement.Attribute("model")?.Value.Substring(10, 8) ?? "0"));
                int cVersion = int.Parse(entryElement.Element("c_ver")?.Value ?? "0");
                int playerNum = int.Parse(entryElement.Element("p_num")?.Value ?? "0");
                int playerRemaining = int.Parse(entryElement.Element("p_rest")?.Value ?? "0");
                int filter = int.Parse(entryElement.Element("filter")?.Value ?? "0");
                int musicId = int.Parse(entryElement.Element("mid")?.Value ?? "0");
                int seconds = int.Parse(entryElement.Element("sec")?.Value ?? "0");
                int port = int.Parse(entryElement.Element("port")?.Value ?? "0");
                int claim = int.Parse(entryElement.Element("claim")?.Value ?? "0");
                int entryId = int.Parse(entryElement.Element("entry_id")?.Value ?? "0");

                // Parse IP addresses (stored as space-separated strings)
                string globalIp = entryElement.Element("gip")?.Value ?? "0.0.0.0";
                string localIp = entryElement.Element("lip")?.Value ?? "0.0.0.0";

                Console.WriteLine($"[{localIp} | {globalIp}] matchmaking");

                // Remove expired matchmaker entries (older than 100 seconds)
                long expirationTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - 100000;
                var expiredRecords = await context.SvMatchmakers
                    .Where(m => m.Timestamp < expirationTime)
                    .ToListAsync();
                
                if (expiredRecords.Count > 0)
                {
                    context.SvMatchmakers.RemoveRange(expiredRecords);
                    await context.SaveChangesAsync();
                    Console.WriteLine($"[{localIp} | {globalIp}] Removed {expiredRecords.Count} expired match data.");
                }

                // Check if entry already exists
                var existingCount = await context.SvMatchmakers
                    .Where(m => m.Version == version && 
                                m.CVersion == cVersion &&
                                m.Filter == filter &&
                                m.Claim == claim &&
                                m.EntryId == entryId)
                    .CountAsync();

                if (existingCount == 0)
                {
                    // Add new matchmaker entry
                    Console.WriteLine($"[{localIp} | {globalIp}] Adding info");
                    var newEntry = new SvMatchmaker
                    {
                        Version = version,
                        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                        CVersion = cVersion,
                        PlayerNum = playerNum,
                        PlayerRemaining = playerRemaining,
                        Filter = filter,
                        MusicId = musicId,
                        Seconds = seconds,
                        Port = port,
                        GlobalIp = globalIp,
                        LocalIp = localIp,
                        Claim = claim,
                        EntryId = entryId
                    };
                    context.SvMatchmakers.Add(newEntry);
                    await context.SaveChangesAsync();
                }
                else
                {
                    // Update existing entry
                    var existingEntry = await context.SvMatchmakers
                        .Where(m => m.Version == version &&
                                    m.CVersion == cVersion &&
                                    m.Filter == filter &&
                                    m.Claim == claim &&
                                    m.EntryId == entryId &&
                                    m.LocalIp == localIp)
                        .FirstOrDefaultAsync();

                    if (existingEntry is not null)
                    {
                        Console.WriteLine($"[{localIp} | {globalIp}] Updating info");
                        existingEntry.PlayerNum = playerNum;
                        existingEntry.PlayerRemaining = playerRemaining;
                        existingEntry.MusicId = musicId;
                        existingEntry.Seconds = seconds;
                        existingEntry.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                        context.SvMatchmakers.Update(existingEntry);
                        await context.SaveChangesAsync();
                    }
                }

                // Check if room is full
                if (playerRemaining < 1)
                {
                    Console.WriteLine($"[{localIp} | {globalIp}] Room is full. Halting.");
                    var successElement = new XElement("entry", new XAttribute("status", 0));
                    responseElement.Add(successElement);
                    data.Document = new(responseElement);
                    return data;
                }

                Console.WriteLine($"[{localIp} | {globalIp}] Searching...");

                // Find opponents
                var opponents = await context.SvMatchmakers
                    .Where(m => m.Version == version &&
                                m.CVersion == cVersion &&
                                m.Filter == filter &&
                                m.Claim == claim &&
                                m.EntryId == entryId &&
                                m.LocalIp != localIp)
                    .ToListAsync(); //todo improve matching logic

                Console.WriteLine($"[{localIp} | {globalIp}] Opponents: {opponents.Count}");

                var entryResponse = new XElement("entry", new XAttribute("status", 0),
                    new KU32("entry_id", (uint)entryId));

                if (opponents.Count > 0)
                {
                    var opponentList = new List<XElement>();
                    foreach (var opponent in opponents)
                    {
                        var opIps = opponent.LocalIp.Split('.');
                        byte[] lipBytes = new byte[4];
                        for (int i = 0; i < 4 && i < opIps.Length; i++)
                        {
                            if (byte.TryParse(opIps[i], out byte ipByte))
                                lipBytes[i] = ipByte;
                        }

                        var opGlobalIps = opponent.GlobalIp.Split('.');
                        byte[] gipBytes = new byte[4];
                        for (int i = 0; i < 4 && i < opGlobalIps.Length; i++)
                        {
                            if (byte.TryParse(opGlobalIps[i], out byte ipByte))
                                gipBytes[i] = ipByte;
                        }

                        opponentList.Add(new XElement("e",
                            new KU16("port", (ushort)opponent.Port),
                            new XElement("gip", gipBytes, new XAttribute("__type", "4u8")),
                            new XElement("lip", lipBytes, new XAttribute("__type", "4u8"))
                        ));
                    }

                    entryResponse.Add(new XElement("entry", opponentList));
                }
                else
                {
                    entryResponse.Add(new XElement("entry"));
                }

                responseElement.Add(entryResponse);
                data.Document = new(responseElement);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EntryS: {ex.Message}");
                var errorElement = new XElement("entry", new XAttribute("status", 1));
                responseElement.Add(errorElement);
                data.Document = new(responseElement);
            }

            return data;
        }
    }
}
