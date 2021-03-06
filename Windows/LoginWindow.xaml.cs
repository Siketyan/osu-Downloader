﻿using osu_Downloader.Objects;
using osu_Downloader.Utilities;
using System;
using System.Windows;
using System.Windows.Input;

namespace osu_Downloader.Windows
{
    /// <summary>
    /// LoginWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginResponse Response { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            Loader.Visibility = Visibility.Visible;
            LoginButton.IsEnabled = false;
            KeyDown -= OnKeyDown;

            try
            {
                Response = await API.LoginAsync(Username.Text, Password.Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Couldn't log into osu!.\nCheck the network connection, username and password.\n"
                        + ex.Message
                );

                Loader.Visibility = Visibility.Hidden;
                KeyDown += OnKeyDown;
                return;
            }

            Close();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            Login(this, null);
        }
    }
}