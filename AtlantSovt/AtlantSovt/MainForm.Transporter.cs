using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    partial class MainForm
    {        
        WorkDocument transporterWorkDocument;
        TaxPayerStatu transporterTaxPayerStatus;
        bool transporterAddWorkDocumentFlag, transporterAddTaxPayerStatusFlag;


        //Add
        #region Add
        void AddTransporter()
        {
            using (var db = new AtlantSovtContext())
            {
                if (nameTransporterAddTextBox.Text != "" && directorTransporterAddTextBox.Text != "" && physicalAddressTransporterAddTextBox.Text != "" && geographyAddressTransporterAddTextBox.Text != "" && transporterAddWorkDocumentFlag && transporterAddTaxPayerStatusFlag)
                {
                    var new_FullName = nameTransporterAddTextBox.Text;
                    var new_ShortName = shortNameTransporterAddTextBox.Text;
                    var new_Director = directorTransporterAddTextBox.Text;
                    var new_PhysicalAddress = physicalAddressTransporterAddTextBox.Text;
                    var new_GeographyAddress = geographyAddressTransporterAddTextBox.Text;
                    var new_WorkDocumentId = transporterWorkDocument.Id;
                    var new_TaxPayerStatusId = transporterTaxPayerStatus.Id;
                    var new_ContractType = originalTransporterAddCheckBox.Checked;
                    var new_Comment = commentTransporterAddTextBox.Text;

                    var new_IfForwarder = filtersTransporterAddCheckedListBox.GetItemChecked(0);
                    var new_TUR = filtersTransporterAddCheckedListBox.GetItemChecked(1);
                    var new_CMR = filtersTransporterAddCheckedListBox.GetItemChecked(2);
                    var new_EKMT = filtersTransporterAddCheckedListBox.GetItemChecked(3);
                    var new_Zborny = filtersTransporterAddCheckedListBox.GetItemChecked(4);
                    var new_AD = filtersTransporterAddCheckedListBox.GetItemChecked(5);


                    var New_Transporter = new Transporter
                    {
                        FullName = new_FullName,
                        ShortName = new_ShortName,
                        Director = new_Director,
                        PhysicalAddress = new_PhysicalAddress,
                        GeographyAddress = new_GeographyAddress,
                        WorkDocumentId = new_WorkDocumentId,
                        TaxPayerStatusId = new_TaxPayerStatusId,
                        ContractType = new_ContractType,                    
                        Comment = new_Comment,
                    };

                    var New_Filters = new Filter
                    {
                        IfForwarder = new_IfForwarder,
                        TUR = new_TUR,
                        CMR = new_CMR,
                        EKMT = new_EKMT,
                        Zborny = new_Zborny,
                        AD = new_AD                        
                    };

                    try
                    {
                        db.Transporters.Add(New_Transporter);
                        db.SaveChanges();
                        MessageBox.Show("Перевізник успішно доданий");

                        New_Filters.Id = New_Transporter.Id;
                        db.Filters.Add(New_Filters);
                        db.SaveChanges();

                        if (addTransporterBankDetailsAddForm != null)
                        {
                            addTransporterBankDetailsAddForm.AddTransporterBankDetail(New_Transporter.Id);
                            addTransporterBankDetailsAddForm = null;
                        }
                        if (addTransporterContactAddForm != null)
                        {
                            addTransporterContactAddForm.AddTransporterContact(New_Transporter.Id);
                            addTransporterContactAddForm = null;
                        }
                        if (transporterCountryAndVehicleSelectForm != null)
                        {
                            transporterCountryAndVehicleSelectForm.CoutriesAndVehiclesSelect(db.Transporters.Find(New_Transporter.Id));
                            transporterCountryAndVehicleSelectForm = null;
                        }
                    }
                    catch (Exception ec)
                    {
                        MessageBox.Show(ec.Message);         
                    }

                }
                else
                {
                    MessageBox.Show("Обов'язкові поля не заповнені");
                }
            }

        }

        void LoadTaxPayerStatusTransporterAddInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from t in db.TaxPayerStatus
                            orderby t.Id
                            select t;
                foreach (var item in query)
                {
                    taxPayerStatusTransporterAddComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }

        void LoadWorkDocumentTransporterAddInfoComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from w in db.WorkDocuments
                            orderby w.Id
                            select w;
                foreach (var item in query)
                {
                    workDocumentTransporterAddComboBox.Items.Add(item.Status + " [" + item.Id + "]");
                }
            }
        }

        void SplitLoadTaxPayerStatusTransporterAddInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = taxPayerStatusTransporterAddComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                transporterTaxPayerStatus = db.TaxPayerStatus.Find(id);
            }
        }

        void SplitLoadWorkDocumentTransporterAddInfo()
        {
            using (var db = new AtlantSovtContext())
            {
                string comboboxText = workDocumentTransporterAddComboBox.SelectedItem.ToString();
                string[] selectedStatus = comboboxText.Split(new char[] { '[', ']' });
                string comboBoxSelectedId = selectedStatus[1];
                long id = Convert.ToInt64(comboBoxSelectedId);
                transporterWorkDocument = db.WorkDocuments.Find(id);
            }
        }
        #endregion

       
    }
}
