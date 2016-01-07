using System;
using System.Collections.Generic;
using System.Text;
using Shadowsocks.Model;

namespace Shadowsocks.Controller
{
    public class AuthController
    {
        Dictionary<string, string> loginPara;

        private string AuthServer = "xiaocaicai.com";
        private int AuthPort = 3721;

        public string EncryptType = "aes-256-cfb";
        public int localPort = 1080;
        public string localAddr = "0.0.0.0";

        public bool shareOverLan = false;

        public bool enableSystemProxy = false;

        public bool global = false;

        public bool useOnlinePac = false;

        public string pacUrl = "";

        private string dataServer;

        public string DataServer
        {
            get { return dataServer; }
            set { dataServer = value; }
        }

        private int dataPort;

        public int DataPort
        {
            get { return dataPort; }
            set { dataPort = value; }
        }


        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }


        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }


        public AuthController()
        {

            loginPara = new Dictionary<string, string>();
            loginPara.Add("username", "caicai");
            loginPara.Add("passwd", "520");
        }

        public void doLogin()
        {
            string loginJson = SimpleJson.SimpleJson.SerializeObject(loginPara);

            string result = GetSocket.SocketSendReceive(AuthServer, AuthPort, loginJson);


            Dictionary<string, string> loginInfo = SimpleJson.SimpleJson.DeserializeObject<Dictionary<string, string>>(result, new SimpleJson.PocoJsonSerializerStrategy());
            Console.WriteLine(result);
            Status = Int32.Parse(loginInfo["s"]);
            if (Status == 1)
            {
                Console.WriteLine("auth ok..");
                Username = loginInfo["username"];
                Code = loginInfo["code"];
                ID = loginInfo["id"];
                DataServer = loginInfo["addr"];
                DataPort = Int32.Parse(loginInfo["port"]);
            }
            else
            {
                Console.WriteLine("auth failed......");
            }

        }

        public Server GetCurrentServer()
        {
            Server server = new Server();
            server.password = Code;
            server.server = DataServer;
            server.method = EncryptType;
            server.server_port = DataPort;

            return server;
        }

        public byte[] GetId()
        {
            byte[] toBytes = Encoding.ASCII.GetBytes(ID);
            return toBytes;
        }

        public static byte[] Combine(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            System.Buffer.BlockCopy(a, 0, c, 0, a.Length);
            System.Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
            return c;
        }

    }
}
