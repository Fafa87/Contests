using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Xml;

namespace Deadline
{
    public partial class TCPClient
    {
        private const string LOGIN = "team16";
        private const string PASSWORD = "10d6db0bd9";
        private string _login;
        private string _pass;
        private string _hostname;

        private TcpClient _client;
        private StreamReader _reader;
        private StreamWriter _writer;
        public int _port;

        #region Settings

        public bool _exceptions = true;
        public bool _showCommunitation = true;

        #endregion

        #region Connection

        public TCPClient(string hostname, int port)
            : this(hostname, port, LOGIN, PASSWORD)
        {
        }

        public TCPClient(string hostname, int port, string login, string pass)
        {
            Console.Out.Write(hostname + port.ToString());
            Console.Out.Flush();
            _hostname = hostname;
            _port = port;
            _login = login;
            _pass = pass;
        }

        public void Login()
        {
            int logged = 0;
            while (logged == 0)
            {
                try
                {

                    _client = new TcpClient(_hostname, _port);
                    logged = 1;
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.ToString());
                    logged = 0;
                }
            }
            _reader = new StreamReader(_client.GetStream());
            _writer = new StreamWriter(_client.GetStream());

            string line = _reader.ReadLine();
            if (line != "LOGIN")
                throw new InvalidOperationException("Błąd przy logowaniu.");
            WriteLineAndFlush(_login);

            line = _reader.ReadLine();
            if (line != "PASS")
                throw new InvalidOperationException("Błąd przy logowaniu.");
            WriteLineAndFlush(_pass);

            if (IsAccepted() == false)
            {
                throw new InvalidOperationException("Niepoprawny login lub hasło.");
            }
        }

        #endregion

        #region Commons

        protected virtual bool IsAccepted()
        {
            return IsAccepted(false);
        }

        protected virtual bool IsAccepted(bool output)
        {
            string line; bool wait = false;
            while ((line = _reader.ReadLine()).StartsWith("WAITING"))
                wait = true;
            if (wait)
                line = _reader.ReadLine();
            if (line != "OK")
            {
                if (line.StartsWith("FAILED 6"))
                {
                    Console.Out.WriteLine(line);
                    throw new InvalidOperationException(line);
                }
                if (output)
                    Console.Out.WriteLine(line);
                if (!_exceptions)
                    return false;
                else
                    throw new InvalidOperationException(line);
            }
            else
                return true;
        }

        protected string ReadLine()
        {
            var line = _reader.ReadLine().Trim();
            if (_showCommunitation)
                Console.Out.WriteLine(line);
            return line;
        }

        protected void WriteLineAndFlush(string text)
        {
            if (_showCommunitation)
                Console.Out.WriteLine(text);
            _writer.WriteLine(text);
            _writer.Flush();
        }

        public virtual void Wait()
        {
            string line = "";

            WriteLineAndFlush("WAIT");
            IsAccepted();
            do
            {
                line = _reader.ReadLine().Trim();
            } while (line != "OK");
        }

        #endregion
    }
}
