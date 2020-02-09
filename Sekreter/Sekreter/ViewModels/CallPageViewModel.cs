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
    public class CallPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private Contact _selectedContact;

        public Contact SelectedContact
        {
            get => _selectedContact;
            set => SetProperty(ref _selectedContact, value);
        }
        public CallPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
          
        }

        public DelegateCommand CallCommand
        {
            get
            {
                return new DelegateCommand(CallAction);
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

        private void CallAction()
        {
            try
            {
                PhoneDialer.Open(_selectedContact.Number);
            }
            catch (ArgumentNullException e)
            {

                Debug.WriteLine(e.Message);
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
