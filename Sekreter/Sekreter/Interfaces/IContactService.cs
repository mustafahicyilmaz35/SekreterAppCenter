using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sekreter.Models;

namespace Sekreter.Interfaces
{
    public interface IContactService
    {
        Task<List<Contact>> GetContactListAsync();
        List<Contact> GetContactList();
    }
}
