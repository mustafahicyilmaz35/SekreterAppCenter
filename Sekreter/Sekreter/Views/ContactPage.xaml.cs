using System.Linq;
using Sekreter.ViewModels;
using Xamarin.Forms;

namespace Sekreter.Views
{
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {

            {
                if (string.IsNullOrEmpty(e.NewTextValue))
                {
                    ListView.ItemsSource = null;

                    ListView.IsGroupingEnabled = true;
                    ListView.ItemsSource = (BindingContext as ContactPageViewModel).GroupedContact;
                }
                else
                {
                    ListView.IsGroupingEnabled = false;
                    ListView.ItemsSource =
                        (BindingContext as ContactPageViewModel).Contacts.Where(p =>
                            p.Name.ToLower().Contains(e.NewTextValue.ToLower()));
                }


            }
        }
    }
}
