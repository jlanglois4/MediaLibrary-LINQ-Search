using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace MediaLibrary
{
    public class MovieRepository : IRepository
    {
        private MovieDataContext _context = new MovieDataContext();

        public void Write()
        {
            int ID = 0;
            string title;
            List<string> genre = new List<string>();
            Console.WriteLine("Enter movie title");
            title = Console.ReadLine();
            if (TestTitle(title))
            {
                bool b = true;
                while (b)
                {
                    Console.WriteLine(string.Format("1: Enter genre\n" +
                                                    "2: Exit"));
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            Console.Write("Enter genre: ");
                            string genreInput = Console.ReadLine();

                            if (genreInput != "")
                            {
                                genre.Add(genreInput);
                            }
                            else
                            {
                                Console.Write("Please enter a genre.");
                            }

                            break;
                        case "2":
                            Console.WriteLine("Exit");
                            if (genre.Count == 0)
                            {
                                genre.Add("N/A");
                            }

                            b = false;
                            break;
                    }
                }

                _context.WriteNewMedia(new Movie(ID, title, genre));
            }
            else
            {
                Console.WriteLine("Movie title already exists\n");
            }
        }

        public void Read()
        {
            _context.ReadMedia();
            List<string> list = new List<string>();
            foreach (Movie m in _context.movieList)
            {
                list.Add(m.Display());
            }

            MediaReadService mediaReadService = new MediaReadService();
            mediaReadService.ListMedia(list);
        }

        private bool TestTitle(string newTitle)
        {
            List<string> titleList = _context.movieList.Select(title => title.title.Replace('"', ' ').Trim().ToLower())
                .ToList();

            if (titleList == null || titleList.Contains(newTitle))
            {
                return false;
            }

            return true;
        }
    }
}