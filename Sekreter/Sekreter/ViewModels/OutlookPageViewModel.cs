using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Prism.Services;
using Sekreter.Interfaces;
using Sekreter.Models;

namespace Sekreter.ViewModels
{
    public class OutlookPageViewModel : BindableBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IDependencyService _dependencyService;
        private string _cc;

        public string Cc
        {
            get => _cc;
            set => SetProperty(ref _cc, value);
        }

        private string _subject;

        public string Subject
        {
            get => _subject;
            set => SetProperty(ref _subject, value);
        }

        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
        private Contact _selectedContact;

        public Contact SelectedContact
        {
            get => _selectedContact;
            set => SetProperty(ref _selectedContact, value);
        }
        public OutlookPageViewModel(IDependencyService dependencyService,INavigationService navigationService)
        {
            _navigationService = navigationService;
            _dependencyService = dependencyService;
        }

        public DelegateCommand MailCommand
        {
            get
            {
                return new DelegateCommand(SendMailAction);
            }
        }

        private  void SendMailAction()
        {
             _dependencyService.Get<IOutlookService>().Launch("com.microsoft.office.outlook",_selectedContact.Email,_cc,_subject,_message);
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
