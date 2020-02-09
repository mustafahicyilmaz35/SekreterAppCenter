using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sekreter.Interfaces
{
    public interface IOutlookService
    {
        Task<bool> Launch(string stringUri, string to, string cc, string subject, string text);
    }
}
