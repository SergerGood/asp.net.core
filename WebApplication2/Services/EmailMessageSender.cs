using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.Sample.Web.Services
{
    public class EmailMessageSender : IMessageSender
    {
        public string SendMessage()
        {
            return "сообщение отправлено на email";
        }
    }
}
