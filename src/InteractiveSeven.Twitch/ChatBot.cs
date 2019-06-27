using System;
using InteractiveSeven.Core;
using InteractiveSeven.Core.IntervalMessages;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Commands;
using InteractiveSeven.Twitch.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TwitchLib.Api;
using TwitchLib.Api.Core.HttpCallHandlers;
using TwitchLib.Api.Core.Models.Root;
using TwitchLib.Api.Core.Models.Undocumented.ChatUser;
using TwitchLib.Api.V5.Models.Subscriptions;
using TwitchLib.Api.V5.Models.Users;
using TwitchLib.Client.Events;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace InteractiveSeven.Twitch
{
    public class ChatBot : INotifyPropertyChanged, IChatBot
    {
        private readonly ITwitchClient _client;
        private readonly IList<ITwitchCommand> _commands;
        private readonly IIntervalMessagingService _intervalMessaging;
        private readonly GilBank _gilBank;
        private bool _isConnected;
        private bool _proUnlock = false;

        private TwitchSettings Settings => ApplicationSettings.Instance.TwitchSettings;

        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }

        public ChatBot(ITwitchClient twitchClient, IList<ITwitchCommand> commands,
            IIntervalMessagingService intervalMessaging, GilBank gilBank)
        {
            _client = twitchClient;
            _commands = commands;
            _intervalMessaging = intervalMessaging;
            _gilBank = gilBank;

            _client.OnLog += Client_OnLog;
            _client.OnJoinedChannel += Client_OnJoinedChannel;
            _client.OnMessageReceived += Client_OnMessageReceived;
            _client.OnChatCommandReceived += Client_OnChatCommandReceived;
            _client.OnConnected += Client_OnConnected;
            _client.OnDisconnected += Client_OnDisconnected;

            var httpClient = new HttpClient();
            string userId = "57245338";
            string channelId = "188854137";
            var url = $"https://api.twitch.tv/kraken/users/{userId}/chat/channels/{channelId}?api_version=5";

            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("OAuth",
                    ApplicationSettings.Instance.TwitchSettings.AccessToken.Split(':').Last());

            //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.twitchtv.v5+json"));

            var result = httpClient.GetStringAsync(url).Result;

            var chatUserCheck = JsonConvert.DeserializeObject<ChatUserCheck>(result);

            if (chatUserCheck.badges.Any())
            {

            }

            var twitchApi = new TwitchAPI();
            twitchApi.Settings.AccessToken = ApplicationSettings.Instance.TwitchSettings.AccessToken.Split(':').Last();
            var response = twitchApi.Undocumented.GetChatUserAsync("57245338", "188854137").Result;
            if (response.Badges.Any())
            {
                
            }

            //Root root = twitchApi.V5.Root.GetRootAsync().Result;
            //twitchApi.Settings.ClientId = root.Token.ClientId;
            //Users users = twitchApi.V5.Users.GetUserByNameAsync(ApplicationSettings.Instance.TwitchSettings.Username).Result;
            //Subscription subscription = twitchApi.V5.Users.CheckUserSubscriptionByChannelAsync(users.Matches[0].Id, "188854137").Result;
            //Console.WriteLine(subscription.CreatedAt);
        }

        public void Connect()
        {
            ConnectionCredentials credentials = new ConnectionCredentials(Settings.Username, Settings.AccessToken);
            _client.Initialize(credentials, Settings.Channel);
            _client.Connect();
        }

        public void Disconnect()
        {
            _client.SendMessage(_client.JoinedChannels.FirstOrDefault(), "Disconnecting Interactive Seven!");
            _client.Disconnect();
        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            _commands.FirstOrDefault(x => x.ShouldExecute(e.Command.CommandText))
                ?.Execute(CommandData.FromChatCommand(e.Command));
            _intervalMessaging.MessageReceived();
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            IsConnected = true;
        }

        private void Client_OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            IsConnected = false;
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            _client.SendMessage(e.Channel, "Interactive Seven is live!");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            int bits = e.ChatMessage.Bits;
            if (bits > 0)
            {
                _gilBank.Deposit(ChatUser.FromChatMessage(e.ChatMessage), bits);
            }
            else
            {
                _gilBank.EnsureAccountExists(ChatUser.FromChatMessage(e.ChatMessage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}