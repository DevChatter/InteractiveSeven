using InteractiveSeven.Core.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using InteractiveSeven.Core.Events;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for TwitchAuthWindow.xaml
    /// </summary>
    public partial class TwitchAuthWindow : Window
    {
        public TwitchAuthWindow(TwitchAuthViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            WebBrowser.Source = new Uri(viewModel.AuthRequestUrl);
            WebBrowser.SourceUpdated += WebBrowser_SourceUpdated;
            DomainEvents.Register<AccessTokenReceived>(ReceivedAccessToken);
        }

        private void ReceivedAccessToken(AccessTokenReceived e)
        {
            Close();
        }

        private void WebBrowser_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            // do something here
        }

        public TwitchAuthViewModel ViewModel { get; }
    }
}
