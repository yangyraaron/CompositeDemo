using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Demo.Content.Test
{
    [Export]
    public class TcpClientVM : NotificationObject
    {
        private byte[] _data = new byte[1024];
        private Socket _server;

        public TcpClientVM()
        {
            this.ConnectCommand = new DelegateCommand<object>(o=>this.Connect());
            this.CloseCommand = new DelegateCommand<object>(o=>this.Close());
            this.SendCommand = new DelegateCommand<object>(o=>this.SendMessage());
        }

        private string _ip = "127.0.0.1";
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

        private string _response = "No Response!";
        public string Response
        {
            get { return _response; }
            set {
                if (_response == value)
                    return;
                _response = value;
            this.RaisePropertyChanged(() => this.Response);
            }
        }

        private string _inputMessage;
        public string InputMessage
        {
            get { return _inputMessage; }
            set { _inputMessage = value; }
        }
        

        private string _log = "No log!";
        public string Log
        {
            get { return _log; }
            set {
                if (_log == value)
                    return;
                _log = value;
            this.RaisePropertyChanged(() => this.Log);
            }
        }

        public DelegateCommand<object> SendCommand { get; set; }
        public DelegateCommand<object> ConnectCommand { get; set; }
        public DelegateCommand<object> CloseCommand { get; set; }

        private void Connect() {
            IPEndPoint end = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                _server.Connect(end);

                int recv = this._server.Receive(_data);
                string message = Encoding.ASCII.GetString(_data, 0, recv);
                this.Response = message;
            }
            catch (SocketException e)
            {
                this.Log = "unable to connect to server \r\n" + e.ToString();
                return;
            }
            this.Log = "the connection has been established!\r\n";
        }

        private void Close() {
            this._server.Shutdown(SocketShutdown.Both);
            this._server.Close();

            this.Log = "the connection has been closed!\r\n";
        }
        private void SendMessage() { 
            this._server.Send(Encoding.ASCII.GetBytes(this.InputMessage));
            _data = new byte[1024];
            int recv = _server.Receive(_data);
            this.Response = Encoding.ASCII.GetString(_data, 0, recv);
        }

    }
}
