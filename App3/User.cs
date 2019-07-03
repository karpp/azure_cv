using System;
using System.Collections.Generic;

namespace App3
{
    public class User
    {
        private string login, password;
        public List<int> preferences = new List<int>{ 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public User(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
    }

    // to load images from csv:
    // https://stackoverflow.com/questions/5282999/reading-csv-file-and-storing-values-into-an-array
    //

    public class Painting
    {
        string name, author, info, receipt;
        string picture_url;
        int style, place;
        public Painting(string name, string author, string info="", string receipt="", string picture_url="", int style=-1, int place)
        {
            this.name = name;
            this.author = author;
            this.info = info;
            this.receipt = receipt;
            this.picture_url = picture_url;
            this.style = style;
            this.place = place;
        }
    }

    public class Route
    {
        private string name, description;
        string picture_url;
        List<Painting> paintings = new List<Painting>();
        int duration;
        public Route(string name, string description, string picture_url="", int duration)
        {
            this.name = name;
            this.description = description;
            this.picture_url = picture_url;
            this.duration = duration;
        }
    }
}
