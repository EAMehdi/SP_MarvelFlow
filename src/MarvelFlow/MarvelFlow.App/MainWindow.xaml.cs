﻿using MarvelFlow.App.controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarvelFlow.App
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public FrameworkElement CurrentControl { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            CurrentControl = new UcHome();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        /* private void Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentControl = new TextBlock { Text = "string" };
            PropertyChanged.Invoke(this,new PropertyChangedEventArgs("CurrentControl"));
        }
        */
    }
}
