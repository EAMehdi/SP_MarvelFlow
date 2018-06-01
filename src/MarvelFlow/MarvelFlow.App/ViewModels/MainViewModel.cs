using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MarvelFlow.App.Lib;
using MarvelFlow.App.Lib.Messages;
using MarvelFlow.Classes;
using MarvelFlow.Classes.Lib;
using MarvelFlow.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace MarvelFlow.App.ViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        public Stack<HistoryObject> History { get; set; } // Historique

        public RelayCommand NavigateHomeCommand { get; private set; }
        public RelayCommand NavigateUserWindowCommand { get; private set; }

        private ViewModelBase _CurrentVM;
        public ViewModelBase CurrentVM
        {
            get
            {
                return _CurrentVM;
            }
            set
            {
                if (_CurrentVM == value)
                    return;
                _CurrentVM = value;
                RaisePropertyChanged(() => CurrentVM);
            }
        }

        public List<Hero> ListHeros { get; set; }
        public List<ISearchableMovie> ListMovies { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            // MAIN
            this.History = new Stack<HistoryObject>();

            this._CurrentVM = ServiceLocator.Current.GetInstance<HomeViewModel>();

            // Init Messaging
            MessengerInstance.Register<HomeMessage>(this, (HomeMessage obj) => Navigator(obj, "HomeViewModel"));

            MessengerInstance.Register<HeroMessage>(this, (HeroMessage obj) => Navigator(obj, "HeroViewModel"));
            MessengerInstance.Register<MovieMessage>(this, LoadMoviePage);

            MessengerInstance.Register<ProfileMessage>(this, (ProfileMessage obj) => Navigator(obj, "ProfileViewModel"));
            MessengerInstance.Register<ListHeroMessage>(this, (ListHeroMessage obj) => Navigator(obj, "ListHeroViewModel"));
            MessengerInstance.Register<ListMovieMessage>(this, (ListMovieMessage obj) => Navigator(obj, "ListMovieViewModel"));
            
            MessengerInstance.Register<HistoryMessage>(this, (HistoryMessage obj) => Navigator(obj, string.Empty));

            // Commands
            this.NavigateUserWindowCommand = new RelayCommand(this.OpenUserWindow, CanDisplayMessage);
            this.NavigateHomeCommand = new RelayCommand(this.DisplayHome, CanDisplayMessage);

        }

        public void Navigator(MessageBase obj, string dest)
        {
            ViewModelBase Source = (ViewModelBase)obj.Sender;

            HistoryObject HistoryObject = null;
            string pageName;
            if (string.IsNullOrEmpty(dest)) // call the history or the ViewModel Name - (get the class name as string)
            {
                HistoryObject = this.History.Pop();
                pageName = HistoryObject.NomVM;
            }
            else
            {
                pageName = dest;
                this.History.Push(new HistoryObject(Source)); // push the source ViewModel in History (as his className string)
            }

            switch (pageName)
            {
                case "HomeViewModel":
                    this.CurrentVM = ServiceLocator.Current.GetInstance<HomeViewModel>();
                    this.History.Clear(); // Clear history
                    break;
                case "HeroViewModel":
                    if (HistoryObject == null && ((HeroMessage)obj).Hero == null)
                    {
                        this.CurrentVM = ServiceLocator.Current.GetInstance<HomeViewModel>();
                        this.History.Clear(); // Clear history
                    }
                    else
                    {
                        HeroViewModel tempHeroVM = ServiceLocator.Current.GetInstance<HeroViewModel>();
                        tempHeroVM.Hero = HistoryObject != null ? HistoryObject.Hero : ((HeroMessage)obj).Hero;
                        this.CurrentVM = tempHeroVM;
                    }
                    break;
                case "FilmViewModel":
                    if (HistoryObject == null && ((MovieMessage)obj).Movie == null)
                    {
                        this.CurrentVM = ServiceLocator.Current.GetInstance<HomeViewModel>();
                        this.History.Clear(); // Clear history
                    }
                    else
                    {
                        FilmViewModel tempMovieVM = ServiceLocator.Current.GetInstance<FilmViewModel>();
                        tempMovieVM.Movie = HistoryObject != null ? HistoryObject.Movie : ((MovieMessage)obj).Movie;
                        this.CurrentVM = tempMovieVM;
                    }
                    break;
                case "SerieViewModel":
                    if (HistoryObject == null && ((MovieMessage)obj).Movie == null)
                    {
                        this.CurrentVM = ServiceLocator.Current.GetInstance<HomeViewModel>();
                        this.History.Clear(); // Clear history
                    }
                    else
                    {
                        SerieViewModel tempMovieVM = ServiceLocator.Current.GetInstance<SerieViewModel>();
                        tempMovieVM.Movie = HistoryObject != null ? HistoryObject.Movie : ((MovieMessage)obj).Movie;
                        this.CurrentVM = tempMovieVM;
                    }
                    break;
                case "ProfileViewModel":
                    this.CurrentVM = ServiceLocator.Current.GetInstance<ProfileViewModel>();
                    break;
                case "ListHeroViewModel":
                    ListHeroViewModel tempListHVM = ServiceLocator.Current.GetInstance<ListHeroViewModel>();
                    if (tempListHVM.ListHerosView == null)
                    {
                        tempListHVM.ListHerosView = new ObservableCollection<Hero>(ServiceLocator.Current.GetInstance<ManagerJson>().GetHeroes());
                    }
                    this.CurrentVM = tempListHVM;
                    break;
                case "ListMovieViewModel":
                    ListMovieViewModel tempListMVM = ServiceLocator.Current.GetInstance<ListMovieViewModel>();
                    if (tempListMVM.ListMoviesView == null)
                    {
                        tempListMVM.ListMoviesView = new ObservableCollection<ISearchableMovie>(ServiceLocator.Current.GetInstance<ManagerJson>().GetMovies());
                    }
                    this.CurrentVM = tempListMVM;
                    break;
                default:
                    this.CurrentVM = ServiceLocator.Current.GetInstance<HomeViewModel>();
                    this.History.Clear(); // Clear history
                    break;
            }
        }

        
        public void LoadMoviePage(MovieMessage obj)
        {
            string target = obj.Movie.GetType().Name.Equals("Serie") ? "SerieViewModel" : "FilmViewModel";
            Navigator(obj, target);
        }
        


        public bool CanDisplayMessage()
        {
            return true;
        }

        public void DisplayHome()
        {
            Navigator(new HomeMessage(this, "Navigate Home"), "HomeViewModel");
        }

        public void OpenUserWindow()
        {
            ServiceLocator.Current.GetInstance<WindowUserViewModel>();
        }
    }
}