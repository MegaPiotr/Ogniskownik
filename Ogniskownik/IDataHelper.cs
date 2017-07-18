using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Ogniskownik
{
    public interface IDataHelper
    {
        List<string> GetAuthors();
        List<Song> GetSongs(string author);
        List<Song> GetSongs();
        string GetSongInfo(string author, string title);
        string GetSongInfo(Song song);
    }
}
