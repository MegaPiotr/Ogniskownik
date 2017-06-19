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

namespace Ogniskownik
{
    class XmlDataHelper : IDataHelper
    {
        private XPathNavigator mNav;

        public XmlDataHelper(StreamReader reader)
        {
            XPathDocument xmlPathDoc = new XPathDocument(reader);
            mNav = xmlPathDoc.CreateNavigator();
        }
        public List<string> getAuthors()
        {
            List<string> authors = new List<string>();
            XPathNodeIterator nodes = mNav.Select("/songs_file/author/@author");
            while (nodes.MoveNext())
                authors.Add(nodes.Current.Value);
            return authors;
        }

        public string getSongInfo(string author, string title)
        {
            var nodes = mNav.SelectSingleNode("//author[@author=\"" + author + "\"]/song[@title=\""+title+"\"]/text");
            return nodes.Value;
        }

        public List<Song> getSongs(string author)
        {
            List<Song> songs = new List<Song>();
            XPathNodeIterator nodes = mNav.Select("//author[@author=\""+author+"\"]/song");
            while (nodes.MoveNext())
            {
                Song song = new Song() {
                    Author = author,
                    Title = nodes.Current.GetAttribute("title", "") };
                songs.Add(song);
            }
            return songs;
        }

        public List<Song> getSongs()
        {
            List<Song> songs = new List<Song>();
            XPathNodeIterator nodes = mNav.Select("//author");
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