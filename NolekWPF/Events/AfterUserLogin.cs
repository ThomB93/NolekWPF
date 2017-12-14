using NolekWPF.Model;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.Events
{
    public class AfterUserLogin : PubSubEvent<Login>
    {
    }
}
