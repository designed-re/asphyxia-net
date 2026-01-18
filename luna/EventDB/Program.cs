using luna.Models;
using System.Text;
using System.Xml.Linq;

namespace EventDB
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (!File.Exists("db.txt"))
            {
                Console.WriteLine("Please enter connection string on db.txt");
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
                Environment.Exit(0);
            }

            Console.WriteLine("Paste your events.txt location");
            string locate = Console.ReadLine();

            AsphyxiaContext context = new();

            var data = File.ReadAllLines(locate);

            foreach (var @event in data)
            {
                Console.WriteLine(@event);

                if (context.SvEvents.Any(x => x.Event == @event)) continue;

                context.SvEvents.Add(new SvEvent
                {
                    Enabled = true,
                    Event = @event
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
