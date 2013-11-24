using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Demo.Content.Test
{
    [Export]
    public class TcpClientVM : NotificationObject
    {
        public TcpClientVM()
        {
            this.SendCommond = new DelegateCommand<object>(this.SendMessage);
        }

        private string _ip;
        public string Ip
        {
            get { return _ip; }
            set { _ip = value;
            //this.RaisePropertyChanged(() => this.Ip);
            }
        }

        private string _port;
        public string Port
        {
            get { return _port; }
            set { _port = value;
            //this.RaisePropertyChanged(() => this.Port);
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value;
            //this.RaisePropertyChanged(() => this.Message);
            }
        }

        DelegateCommand<object> SendCommond { get; set; }

        private void SendMessage(object obj) { 
        
        }

    }
}
