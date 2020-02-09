using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Prism.Navigation;
using Sekreter.Models;
using Xamarin.Essentials;

namespace Sekreter.ViewModels
{
    public class SmsPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private string _smsText;

        public string SmsText
        {
            get => _smsText;
            set => SetProperty(ref _smsText, value);
        }

        private Contact _selectedContact;

        public Contact SelectedContact
        {
            get => _selectedContact;
            set => SetProperty(ref _selectedContact, value);
        }
        public SmsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public DelegateCommand SmsCommand
        {
            get
            {
                return new DelegateCommand(SendSmsActionAsync);
            }
        }

        private async void SendSmsActionAsync()
        {
            try
            {
                var message = new SmsMessage(_smsText,new []{_selectedContact.Number});
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException e)
            {

                Debug.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
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
