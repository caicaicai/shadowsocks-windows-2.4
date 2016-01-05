using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Shadowsocks.Controller;
using Shadowsocks.Properties;
using Shadowsocks.View;
using SimpleJson;

namespace Shadowsocks.View
{
    public partial class Login : Form
    {
        AuthController auth;
        
        public Login(AuthController au)
        {
            InitializeComponent();
            auth = au;
            
        }


        private void login_btn_Click(object sender, EventArgs e)
        {


            auth.doLogin();
            ShadowsocksController controller = new ShadowsocksController(auth);

            MenuViewController viewController = new MenuViewController(controller);

            controller.Start();
        }
    }
}
