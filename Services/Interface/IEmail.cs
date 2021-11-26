using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IEmail
    {
        public Task SendAsync(string email, string subject, string text);
    }
}
