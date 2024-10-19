using System.Text.Json;
using System.Xml.Serialization;

namespace ConsoleApp15
{
    public class Album
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }
        public TimeSpan Duration { get; set; }
        public string RecordLabel { get; set; }
        public List<Song> Songs { get; set; }

        public void Output()
        {
            Console.WriteLine($"Название альбома: {Title},Исполнитель: {Artist},Год выпуска: {Year},Продолжительность(мин): {Duration} \n\nПесни:");
            foreach (var a in Songs)
            {
                Console.WriteLine($"Название: {a.Title},Жанр: {a.Style},Продолжительность(мин): {a.Duration}");
                Console.WriteLine();
            }
        }
    }
    public class Song
    {
        public string Title { get; set; }
        public string Style { get; set; }
        public TimeSpan Duration { get; set; }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            Album album = Input();
            album.Output();

            string filename = "album.xml";
            SerializeAlbum(album, filename);

            Album loadedJournal = DeserializeAlbum(filename);
            loadedJournal.Output();
        }
        static Album Input()
        {
            Album album = new Album();
            Console.Write("Введите название альбома: ");
            album.Title = Console.ReadLine();
            Console.Write("Введите имя исполнителя: ");
            album.Artist = Console.ReadLine();
            Console.Write("Введите год выпуска: ");
            album.Year = int.Parse(Console.ReadLine());
            Console.Write("Введите продолжительность(мин): ");
            album.Duration = TimeSpan.Parse(Console.ReadLine());
            Console.Write("Студия звукозаписи:");
            album.RecordLabel = Console.ReadLine();
            album.Songs = new List<Song>();
            Console.Write("Сколько песен в журнале:");
            int songCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < songCount; i++)
            {
                Song song = new Song();
                Console.Write($"Введите название песни {i + 1}: ");
                song.Title = Console.ReadLine();
                Console.Write($"Введите жанр песни {i + 1}: ");
                song.Style = Console.ReadLine();
                Console.Write($"Введите продолжительность песни {i + 1}: ");
                song.Duration = TimeSpan.Parse(Console.ReadLine());
                album.Songs.Add(song);
            }
            return album;
        }
        static void SerializeAlbum(Album album, string filePath)
        {
            XmlSerializer srl = new XmlSerializer(typeof(Album));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                srl.Serialize(writer, album);
            }
            Console.WriteLine($"Альбом сериализован в файл: {filePath}");
        }
        static Album DeserializeAlbum(string filePath)
        {
            XmlSerializer dsrl = new XmlSerializer(typeof(Album));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (Album)dsrl.Deserialize(reader);
            }
        }
    }
}