﻿using MarvelFlow.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelFlow.App.Lib
{
    public class CurrentUserHandler
    {
        private User CurrentUser { get; set; }


        public CurrentUserHandler()
        {
            //CurrentUser = null;
            CurrentUser = new User("louisonAdmin", "passwdSecret", "02/06/2016", "louison@mail.com", "Bellec", "Louison", true, "IM");
        }


        public User GetUser()
        {
            return CurrentUser;
        }

        public void EditUser(User u)
        {
            CurrentUser = u;
        }

        public void EditUserHero(string s)
        {
            CurrentUser.HeroFav = s;
        }
    }
}
