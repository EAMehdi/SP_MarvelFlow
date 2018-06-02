﻿using MarvelFlow.Classes.Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelFlow.Classes
{
    public class Film : Movie
    {
        public string Id { get; private set; }

        public Universe Universe { get; private set; }

        public string BA { get; private set; }

        public List<string> ListHeroString { get; private set; }

        public List<Hero> ListHeroes { get; set; }

        /// <summary>
        /// Default constructor Film
        /// Extends Movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="affiche"></param>
        /// <param name="desc"></param>
        /// <param name="productor"></param>
        /// <param name="real"></param>
        /// <param name="date"></param>
        public Film(string id, string title, string affiche, string desc, string real, string date, Universe universe, string BA, List<string> listHeros) 
            : base(title, affiche, desc, real, date)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id du film null", nameof(id));
            }

            this.Id = id;
            this.Universe = Universe;
            this.BA = ConfigurationManager.AppSettings["TeaserPath"] + BA ;
            this.ListHeroString = listHeros;
            this.ListHeroes = new List<Hero>();
        }

        public override string GetId()
        {
            return Id;
        }

        public override Universe GetUniverse()
        {
            return Universe;
        }

        public override List<Hero> GetListHeros()
        {
            return ListHeroes;
        }

        public override List<string> GetHeroString()
        {
            return ListHeroString;
        }

        public override void AddListHero(Hero h)
        {
           this.ListHeroes.Add(h);
        }

        // Custom //
    }
}
