using AtlantSovt.Additions;
using AtlantSovt.AtlantSovtDb;
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
                string path = (System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\ConnectionString").Replace("\\Release", "").Replace("\\Debug", "").Replace("\\bin", "");
                StreamReader streamReader = new StreamReader(path);

                while (!streamReader.EndOfStream)
                {
                    str = streamReader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message, AtlantSovt.Properties.Resources.Помилка_1, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
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

        public Task<bool> CheckConnection()
        {
            var task = new Task<bool>(()=>{
            using (var db = new AtlantSovtContext())
            {
                try
                {
                    db.Database.Connection.Open();  // check the database connection
                    var query =
                        from testConnection in db.WorkDocuments
                        select testConnection;
                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка_з_єднання_з_сервером, AtlantSovt.Properties.Resources.Немає_з_єднання, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);

                    return false;
                }
            }
        });
            task.Start();
            return task;
        }
    }
}
