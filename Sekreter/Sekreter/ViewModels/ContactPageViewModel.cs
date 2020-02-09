using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Services;
using Sekreter.Interfaces;
using Sekreter.Models;
using Sekreter.Utilities;

namespace Sekreter.ViewModels
{
    public class ContactPageViewModel : BindableBase
    {
        private readonly IPageDialogService _pageDialogService;
        private Contact _selectedContact;

        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                SetProperty(ref _selectedContact, value);
                HandleSelectedContact();
            } 
        }

        private async void HandleSelectedContact()
        {
            var result = await _pageDialogService.DisplayActionSheetAsync("Yapmak İstediğiniz İşlemi Seçiniz", "Cancel", null, "Çağrı Yap", "Sms Gönder", "Whatsapp mesaj gönder","Email Gönder");
            switch (result)
            {
                case "Çağrı Yap":
                    GoToCallPage();
                    break;
                case "Sms Gönder":
                    GoToSmsPage();
                    break;
                case "Whatsapp mesaj gönder":
                    GoToWhatsAppPage();
                    break;
                case "Email Gönder":
                    GoToOutlookPage();
                    break;
            }

        }


        private async void GoToCallPage()
        {
            var param = new NavigationParameters();
            param.Add("selectedContact", SelectedContact);
            await _navigationService.NavigateAsync("CallPage", param);
        }

        private async void GoToSmsPage()
        {
            var param = new NavigationParameters();
            param.Add("selectedContact", SelectedContact);
            await _navigationService.NavigateAsync("SmsPage", param);
        }

        private async void GoToWhatsAppPage()
        {
            var param = new NavigationParameters();
            param.Add("selectedContact", SelectedContact);
            await _navigationService.NavigateAsync("WhatsAppPage", param);
        }

        private async void GoToOutlookPage()
        {
            var param = new NavigationParameters();
            param.Add("selectedContact", SelectedContact);
            await _navigationService.NavigateAsync("OutlookPage", param);
        }


        private readonly INavigationService _navigationService;
        private readonly IDependencyService _dependencyService;
        private IList<Contact> _contacts;
        private IEnumerable<Grouping<string, Contact>> _groupedContact;

        public IEnumerable<Grouping<string, Contact>> GroupedContact
        {
            get => _groupedContact;
            set => SetProperty(ref _groupedContact, value);
        }


        public IList<Contact> Contacts
        {
            get => _contacts;
            set => SetProperty(ref _contacts, value);
        }
        public ContactPageViewModel(IDependencyService dependencyService, INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            _dependencyService = dependencyService;
            Init();

        }

        private async Task Init()
        {
            Contacts = await _dependencyService.Get<IContactService>().GetContactListAsync();
            Thread.Sleep(500);
            GroupedContact = Contacts.OrderBy(p => p.Name).GroupBy(p => p.Name[0].ToString().ToUpper())
                .Select(p => new Grouping<string, Contact>(p.Key, p));

        }
    }
}
