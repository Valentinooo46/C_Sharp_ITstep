using RecipeApp;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    internal class File1
    {
        static void Main(string[] args)
        {
            MusicAlbum musicAlbum = MusicAlbum.LoadFromFile(@"C:\Users\valea\source\repos\Project2\Project2\MusicAlbum.json");
            musicAlbum.SeeInfo();
            Console.WriteLine(musicAlbum.SerializeToJson());
        }

        
    }
}
