using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Project2
{





    internal class MusicAlbum
    {
        public string Name_Album { get; set; }
        public string Name_Artist { get; set; }
        public DateTime Date_Release { get; set; }
        public UInt32 duration { get; set; }
        public string studio { get; set; }
        public List<Song> songs { get; set; }

        public MusicAlbum(string Name_Album, string Name_Artist, DateTime Date_Release, UInt32 duration, string studio)
        {
            this.Name_Album = Name_Album;
            this.Name_Artist = Name_Artist;
            this.Date_Release = Date_Release;
            this.duration = 0;
            this.studio = studio;
            songs = new List<Song>();
        }

        public void Add_Song(Song s)
        {
            songs.Add(s);
            duration += s.duration;
        }

        public void Remove_Song(string Name_song)
        {
            foreach (Song s in songs)
            {
                if (s.Name_Song == Name_song)
                {
                    songs.Remove(s);
                    duration -= s.duration;
                    break;
                }
            }
        }

        public void Edit_Song(string Name_song, Song New_Song)
        {
            foreach (Song s in songs)
            {
                if (s.Name_Song == Name_song)
                {
                    s.Name_Song = New_Song.Name_Song;
                    s.Name_Artist = New_Song.Name_Artist;

                    duration += New_Song.duration - s.duration;
                    s.duration = New_Song.duration;
                    s.studio = New_Song.studio;
                    break;
                }
            }
        }

        public void SeeInfo()
        {
            Console.WriteLine("Name Album: " + Name_Album);
            Console.WriteLine("Name Artist: " + Name_Artist);
            Console.WriteLine("Date Release: " + Date_Release);
            Console.WriteLine("Duration: " + duration);
            Console.WriteLine("Studio: " + studio);
            Console.WriteLine("Songs: ");
            foreach (Song s in songs)
            {
                Console.WriteLine("Name Song: " + s.Name_Song);
                Console.WriteLine("Name Artist: " + s.Name_Artist);
                Console.WriteLine("Duration: " + s.duration);
                Console.WriteLine("Studio: " + s.studio);
            }
        }

        public string SerializeToJson()
        {
            return JsonSerializer.Serialize<MusicAlbum>(this);
        }

        public static MusicAlbum DeserializeFromJson(string json)
        {
            return JsonSerializer.Deserialize<MusicAlbum>(json);
        }

        public void SaveToFile(string filePath)
        {
            string json = SerializeToJson();
            File.WriteAllText(filePath, json);
        }

        public static MusicAlbum LoadFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return DeserializeFromJson(json);
        }
    }
    [Serializable]
    internal class Song
    {
        public string Name_Song { get; set; }
        public string Name_Artist { get; set; }
        public UInt32 duration { get; set; }
        public string studio { get; set; }

        public Song(string Name_Song, string Name_Artist, uint duration, string studio)
        {
            this.Name_Song = Name_Song;
            this.Name_Artist = Name_Artist;
            this.duration = duration;
            this.studio = studio;
        }
    }
}
