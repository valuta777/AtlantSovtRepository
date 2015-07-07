﻿using AtlantSovt.AtlantSovtDb;
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
    public partial class AddTransporterVehicleForm : Form
    {
        public AddTransporterVehicleForm()
        {
            InitializeComponent();
        }

        public void AddVehicle()
        {
            using (var db = new AtlantSovtContext())
            {
                if (addTransporterVehicleTextBox.Text != "")
                {
                    var new_Name = addTransporterVehicleTextBox.Text;

                    var New_Vehicle = new Vehicle
                    {
                        Type = new_Name,
                    };
                    try
                    {
                        db.Vehicles.Add(New_Vehicle);
                        db.SaveChanges();
                        MessageBox.Show("Нове значення успішно додане");
                    }
                    catch (Exception ec)
                    {
                        MessageBox.Show(ec.Message);
                    }
                }
            }
        }

        private void addVehicleButton_Click(object sender, EventArgs e)
        {
            AddVehicle();
            this.Dispose();
        }
    }
}
