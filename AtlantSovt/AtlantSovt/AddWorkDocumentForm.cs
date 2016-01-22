using AtlantSovt.Additions;
using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    public partial class AddWorkDocumentForm : Form
    {
        public AddWorkDocumentForm()
        {
            InitializeComponent();
        }

        public void AddWorkDocument()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addWorkDocumentTextBox.Text != "")
                {
                    var new_Status = addWorkDocumentTextBox.Text;

                    var New_WorkDocument = new WorkDocument
                    {
                        Status = new_Status,
                    };
                    try
                    {
                        db.WorkDocuments.Add(New_WorkDocument);
                        db.SaveChanges();
                        MessageBox.Show(AtlantSovt.Properties.Resources.Нове_значення_успішно_додане);
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void addWorkDocumentButton_Click(object sender, EventArgs e)
        {
            AddWorkDocument();
            this.Dispose();
        }
    }
}
