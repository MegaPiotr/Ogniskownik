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
using System.Xml.XPath;
using System.IO;
using System.Reflection;

namespace Ogniskownik
{
    class XmlDataHelper : IDataHelper
    {
        private XPathNavigator mNavigator;

        public XmlDataHelper()
        {
            var assembly = typeof(XmlDataHelper).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Ogniskownik.songs.xml");
            XPathDocument xmlPathDoc = new XPathDocument(stream);
            mNavigator = xmlPathDoc.CreateNavigator();
        }
        public List<string> GetAuthors()
        {
            List<string> authors = new List<string>();
            XPathNodeIterator nodes = mNavigator.Select("/songs_file/author/@author");
            while (nodes.MoveNext())
                authors.Add(nodes.Current.Value);
            return authors;
        }

        public string GetSongInfo(string author, string title)
        {
            var nodes = mNavigator.SelectSingleNode("//author[@author=\"" + author + "\"]/song[@title=\""+title+"\"]/text");
            return nodes.Value;
        }

        public string GetSongInfo(Song song)
        {
            return GetSongInfo(song.Author, song.Title);
        }

        public List<Song> GetSongs(string author)
        {
            List<Song> songs = new List<Song>();
            XPathNodeIterator nodes = mNavigator.Select("//author[@author=\""+author+"\"]/song");
            while (nodes.MoveNext())
            {
                Song song = new Song() {
                    Author = author,
                    Title = nodes.Current.GetAttribute("title", "") };
                songs.Add(song);
            }
            return songs;
        }

        public List<Song> GetSongs()
        {
            List<Song> songs = new List<Song>();
            XPathNodeIterator nodes = mNavigator.Select("//author");
            while (nodes.MoveNext())
            {
                string author = nodes.Current.GetAttribute("author", "");
                XPathNodeIterator lvlsong=nodes.Current.Select("song");
                while (lvlsong.MoveNext())
                {
                    Song song = new Song()
                    {
                        Author = author,
                        Title = lvlsong.Current.GetAttribute("title", "")
                    };
                    songs.Add(song);
                }
            }
            return songs;
        }
    }
}