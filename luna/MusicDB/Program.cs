using System.Text;
using System.Xml;
using System.Xml.Linq;
using MusicDB.Models;
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

if (!File.Exists("db.txt"))
{
    Console.WriteLine("Please enter connection string on db.txt");
    Console.WriteLine("Press enter to exit");
    Console.ReadLine();
    Environment.Exit(0);
}

Console.WriteLine("Paste your music_db.xml location");
string locate = Console.ReadLine();

AsphyxiaContext context = new ();

XDocument document = XDocument.Parse(File.ReadAllText(locate, Encoding.GetEncoding("Shift-JIS")));

var musicElements = document.Element("mdb").Elements();
foreach (var musicElement in musicElements)
{
    XElement infoElement = musicElement.Element("info");
    int id = int.Parse(musicElement.Attribute("id").Value);
    string title = infoElement.Element("title_name").Value;
    string title_yomi = infoElement.Element("title_yomigana").Value;
    string artist = infoElement.Element("artist_name").Value;
    string artist_yomi = infoElement.Element("artist_yomigana").Value;
    string date = infoElement.Element("distribution_date").Value;
    string data =
        $"#{id} {title}({title_yomi}) - {artist}({artist_yomi})";
    Console.WriteLine(data);

    File.AppendAllText("result.txt", data+Environment.NewLine, Encoding.UTF8);

    if (context.SvMusics.Any(x => x.Id == id)) continue;

    context.SvMusics.Add(new()
    {
        Id = id, Artist = artist, ArtistYomigana = artist_yomi, Date = DateOnly.ParseExact(date, "yyyyMMdd"), Title = title,
        TitleYomigana = title_yomi
    });
}

await context.SaveChangesAsync();