﻿using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MarvelFlow.App.Lib.Messages;
using MarvelFlow.Classes;
using MarvelFlow.Classes.Lib;
using MarvelFlow.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelFlow.App.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public RelayCommand NavigateListHeroCommand { get; private set; } // Navigate to ListHeros
        public RelayCommand NavigateListMovieCommand { get; private set; } // Navigate to ListMovies
        public RelayCommand NavigateProfileCommand { get; private set; } // Navigate to Profile
        public RelayCommand<Movie> NavigateMovieCommand { get; private set; } //Navigate to HomeVideo


        private Film _HomeVideo;

        public Film HomeVideo
        {
            get
            {
                return _HomeVideo;
            }
            set
            {
                if (_HomeVideo == value)
                    return;
                _HomeVideo = value;
                RaisePropertyChanged(() => HomeVideo);
            }
        }



        public HomeViewModel()
        {
            FindLastVideo();
            this.NavigateListHeroCommand = new RelayCommand(this.SendNavigateListHero, CanDisplayMessage);
            this.NavigateListMovieCommand = new RelayCommand(this.SendNavigateListMovie, CanDisplayMessage);
            this.NavigateProfileCommand = new RelayCommand(this.SendNavigateProfile, CanDisplayMessage);

        }

        public void FindLastVideo()
        {
            HomeVideo = (Film) ServiceLocator.Current.GetInstance<ManagerJson>().GetMovies()
                .Where(m => m.GetType() == typeof(Film))
                .OrderBy(m => m.GetDate())
                .LastOrDefault();

            //this.NavigateMovieCommand = new RelayCommand<Film>(this.SendNavigateMovie(HomeVideo),CanDisplayMessage());
        }

        // Commands methods

        public bool CanDisplayMessage()
        {
            return true;
        }

        public void SendNavigateListHero()
        {
            MessengerInstance.Send<ListHeroMessage>(new ListHeroMessage(this, "Navigate List Hero Message"));
        }

        public void SendNavigateListMovie()
        {
            MessengerInstance.Send<ListMovieMessage>(new ListMovieMessage(this, "Navigate List Movie Message"));
        }

        public void SendNavigateProfile()
        {
            MessengerInstance.Send<ProfileMessage>(new ProfileMessage(this, "Navigate Profile Message"));
        }

        public void SendNavigateMovie(Film HomeVideo)
        {
            MessengerInstance.Send<MovieMessage>(new MovieMessage(this, HomeVideo, "Navigate Movie Message"));
        }
    }
}
