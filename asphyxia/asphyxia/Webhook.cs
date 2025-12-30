using Discord;
using Discord.Webhook;

namespace asphyxia
{
    public class Webhook
    {
        public static DiscordWebhookClient client = new DiscordWebhookClient(
            "https://discord.com/api/webhooks/1182202702307000341/dE7WK4whKKFwV5j0V0kMQC3VPwUc97J70NeXeVGamZS0bnnXz2PFHR4EgKihjbfanCBb");

        public static EmbedBuilder CreateEmbed(string title, string desc, string footer = "")
        {
            EmbedBuilder embed = new EmbedBuilder();

            embed.WithTitle(title);
            embed.WithDescription("```"+desc+"```");
            embed.WithCurrentTimestamp();
            embed.WithFooter(footer);

            return embed;
        }

        public static void SendEmbed(EmbedBuilder embed)
        {
            client.SendMessageAsync(embeds: new [] { embed.Build() });
            
        }
    }
}
