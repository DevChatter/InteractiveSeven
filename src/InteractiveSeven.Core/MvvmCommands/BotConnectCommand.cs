﻿using System;
using System.Windows.Input;

namespace InteractiveSeven.Core.MvvmCommands
{
    public class BotConnectCommand : ICommand
    {
        private IChatBot ChatBot;

        public BotConnectCommand(IChatBot chatBot)
        {
            ChatBot = chatBot;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true; // TODO: Replace with logic to determine if we're connected
        }

        public void Execute(object parameter)
        {
            ChatBot.Connect();
        }
    }
}