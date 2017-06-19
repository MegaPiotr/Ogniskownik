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
        List<string> getAuthors();
        List<Song> getSongs(string author);
        List<Song> getSongs();
        string getSongInfo(string author, string title);
    }
}
