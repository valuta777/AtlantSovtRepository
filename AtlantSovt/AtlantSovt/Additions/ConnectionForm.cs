using AtlantSovt.Additions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    public partial class ConnectionForm : Form
    {
        public ConnectionForm()
        {
            InitializeComponent();
        }

        static public string GetConnectionString()
        {
            string str = "";
            try
            {
                StreamReader streamReader = new StreamReader((System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\ConnectionString").Replace("\\bin\\Release", ""));

                while (!streamReader.EndOfStream)
                {
                    str = streamReader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show("Помилка: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
            }

            if (str == "")
            {
                str = "it`s not empty string";
                Application.Exit();
                return str;
            }
            else
            {
                return str;
            }
        }
    }
}
