using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Sekreter.Models;
using Xamarin.Forms.OpenWhatsApp;

namespace Sekreter.ViewModels
{
    public class WhatsAppPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private string _messageText;

        public string MessageText
        {
            get => _messageText;
            set => SetProperty(ref _messageText, value);
        }
        private Contact _selectedContact;

        public Contact SelectedContact
        {
            get => _selectedContact;
            set => SetProperty(ref _selectedContact, value);
        }
        public WhatsAppPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand WhatsAppCommand
        {
            get
            {
                return new DelegateCommand(SendWhatsAppMessage);
            }
        }

        private void SendWhatsAppMessage()
        {
            try
            {
                Chat.Open(_selectedContact.Number,_messageText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public DelegateCommand GoBackCommand
        {
            get
            {
                return new DelegateCommand(GoBackAction);
            }
        }

        private void GoBackAction()
        {
            _navigationService.NavigateAsync("ContactPage");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            SelectedContact = parameters.GetValue<Contact>("selectedContact");
        }
    }
}
