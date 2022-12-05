using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days.Messages
{
    public class DataServiceMessage : ValueChangedMessage<string>
    {
        public DataServiceMessage(string value) : base(value)
        {
        }
    }
}
