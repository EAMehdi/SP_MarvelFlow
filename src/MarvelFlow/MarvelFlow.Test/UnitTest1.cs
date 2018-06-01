﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using MarvelFlow.Classes;
using MarvelFlow.Classes.Lib;
using MarvelFlow.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarvelFlow.Test
{
    [TestClass]
    public class MiscTest
    {
        [TestMethod]
        public void Test1()
        {
            ManagerJson Manager = new ManagerJson();

            List<Hero> ListHeros = Manager.GetHeroes();
            List<ISearchableMovie> ListMovies = Manager.GetMovies();

            Debug.WriteLine(ListHeros);
            Debug.WriteLine(ListMovies);
        }

        [TestMethod]
        public void DateTest()
        {
            DateTime tmpDate = DateTime.ParseExact("11/02/98", "dd/MM/yy", null);
            Debug.WriteLine(tmpDate);
            string Date = tmpDate.ToString("dd/MM/yy");
            Debug.WriteLine(Date);
            DateTime now = DateTime.Now;
            Debug.WriteLine(DateTime.Now);
            Debug.WriteLine(tmpDate < now);
        }
    }
}
