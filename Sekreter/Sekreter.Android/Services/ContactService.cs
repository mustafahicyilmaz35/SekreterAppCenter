using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Sekreter.Droid.Services;
using Sekreter.Interfaces;
using Sekreter.Models;

[assembly:Xamarin.Forms.Dependency(typeof(ContactService))]
namespace Sekreter.Droid.Services
{
    public class ContactService : IContactService
    {
        public List<Contact> GetContactList()
        {
            return GetContactListA().ToList();
        }

        private IEnumerable<Contact> GetContactListA()
        {
            var uri = ContactsContract.Contacts.ContentUri;
            //var ctx = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity;
            var ctx = Application.Context;
            var cursor = ctx.ApplicationContext.ContentResolver.Query(uri, null, null, null, null);
            if (cursor.Count == 0)
            {
                yield break;
            }

            while (cursor.MoveToNext())
            {
                var contact = CreateContact(cursor, ctx);

                if (!string.IsNullOrWhiteSpace(contact.Name))
                    yield return contact;
            }
        }

        private static Contact CreateContact(ICursor cursor, Context ctx)
        {
            var contactId = GetString(cursor, ContactsContract.Contacts.InterfaceConsts.Id);

            //var hasNumbers = GetString(cursor, ContactsContract.Contacts.InterfaceConsts.HasPhoneNumber) == "1";

            var numbers = GetNumbers(ctx, contactId).ToList();
            var emails = GetEmails(ctx, contactId).ToList();

            var contact = new Contact
            {
                Name = GetString(cursor, ContactsContract.Contacts.InterfaceConsts.DisplayName),
                Emails = emails,
                Email = emails.LastOrDefault(),
                Numbers = numbers,
                Number = numbers.LastOrDefault()
            };

            return contact;
        }

        public Task<List<Contact>> GetContactListAsync()
        {
            return Task.Run(() => GetContactList());
        }

        private static string GetString(ICursor cursor, string key)
        {
            return cursor.GetString(cursor.GetColumnIndex(key));
        }

        private static IEnumerable<string> GetNumbers(Context ctx, string contactId)
        {
            var key = ContactsContract.CommonDataKinds.Phone.NormalizedNumber;

            var cursor = ctx.ApplicationContext.ContentResolver.Query(
                ContactsContract.CommonDataKinds.Phone.ContentUri,
                null,
                ContactsContract.CommonDataKinds.Phone.InterfaceConsts.ContactId + " = ?",
                new[] { contactId },
                null
            );

            return ReadCursorItems(cursor, key);
        }

        private static IEnumerable<string> GetEmails(Context ctx, string contactId)
        {
            var key = ContactsContract.CommonDataKinds.Email.InterfaceConsts.Data;

            var cursor = ctx.ApplicationContext.ContentResolver.Query(
                ContactsContract.CommonDataKinds.Email.ContentUri,
                null,
                ContactsContract.CommonDataKinds.Email.InterfaceConsts.ContactId + " = ?",
                new[] { contactId },
                null);

            return ReadCursorItems(cursor, key);
        }
        private static IEnumerable<string> ReadCursorItems(ICursor cursor, string key)
        {
            while (cursor.MoveToNext())
            {
                var value = GetString(cursor, key);
                yield return value;
            }
            cursor.Close();
        }
    }
}