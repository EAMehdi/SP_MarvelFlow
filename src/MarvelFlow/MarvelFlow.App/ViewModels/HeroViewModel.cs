﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MarvelFlow.App.Lib.Messages;
using MarvelFlow.Classes;
using MarvelFlow.Classes.Lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelFlow.App.ViewModels
{
    public class HeroViewModel : ViewModelBase
    {
        public RelayCommand ReturnBackCommand { get; private set; }
        public RelayCommand<Movie> NavigateMovieCommand { get; private set; }


        private Hero _Hero;
        public Hero Hero
        {
            get
            {
                return _Hero;
            }
            set
            {
                if (_Hero == value)
                    return;
                _Hero = value;
                RaisePropertyChanged(() => Hero);
            }
        }

        public HeroViewModel()
        {
            this.ReturnBackCommand = new RelayCommand(this.SendReturnBack, CanDisplayMessage);
            this.NavigateMovieCommand = new RelayCommand<Movie>(this.SendNavigateMovie, CanDisplayMessage());
            
        }

        public bool CanDisplayMessage()
        {
            return true;
        }

        public void SendReturnBack()
        {
            MessengerInstance.Send<HistoryMessage>(new HistoryMessage(this, "Navigate Back History"));
        }

        public void SendNavigateMovie(ISearchableMovie movie)
        {
            MessengerInstance.Send<MovieMessage>(new MovieMessage(this, movie, "Navigate Movie Message"));
        }

    }
}
