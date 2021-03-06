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
    public partial class AddPaymentTermsForm : Form
    {
        public AddPaymentTermsForm()
        {
            InitializeComponent();
        }

        public void AddPaymentTerms()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addPaymentTermsTextBox.Text != "")
                {
                    var new_Type = addPaymentTermsTextBox.Text;

                    var New_PaymentTerm = new Payment
                    {
                        Type = new_Type,
                    };
                    try
                    {
                        db.Payments.Add(New_PaymentTerm);
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

        private void addCargoButton_Click(object sender, EventArgs e)
        {
            AddPaymentTerms();
            this.Dispose();
        }
    }
}
