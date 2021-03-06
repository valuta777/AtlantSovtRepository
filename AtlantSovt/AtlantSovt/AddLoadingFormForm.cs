﻿using AtlantSovt.Additions;
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
    public partial class AddLoadingFormForm : Form
    {
        public AddLoadingFormForm()
        {
            InitializeComponent();
        }

        public void AddLoadingForm()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addLoadingFormTextBox.Text != "")
                {
                    var new_Type = addLoadingFormTextBox.Text;

                    var New_LoadingForm = new LoadingForm
                    {
                        Type = new_Type,
                    };
                    try
                    {
                        db.LoadingForms.Add(New_LoadingForm);
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

        private void addLoadingFormButton_Click(object sender, EventArgs e)
        {
            AddLoadingForm();
            this.Dispose();
        }
    }
}
