using AtlantSovt.Additions;
using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    partial class MainForm
    {
        object deleteObjectId = null;        

        private void DeleteItems<T>(IQueryable<T> tableSelector)
        {
            using (var db = new AtlantSovtContext())
            {
                try
                {
                    if (deleteObjectId != null)
                    {
                        if (MessageBox.Show(AtlantSovt.Properties.Resources.Видалити_запис + "?" + "\n" + AtlantSovt.Properties.Resources.Видалення_цього_запису_приведе_до_його_втрати_у_звязаних_таблицях, AtlantSovt.Properties.Resources.Підтвердіть_видалення, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        { 
                            if (tableSelector.ElementType.Name == typeof(Country).Name)
                            {
                                db.Countries.Remove(db.Countries.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(Vehicle).Name)
                            {
                                db.Vehicles.Remove(db.Vehicles.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(TaxPayerStatu).Name)
                            {
                                db.TaxPayerStatus.Remove(db.TaxPayerStatus.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(WorkDocument).Name)
                            {
                                db.WorkDocuments.Remove(db.WorkDocuments.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(Staff).Name)
                            {
                                db.Staffs.Remove(db.Staffs.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(Trailer).Name)
                            {
                                db.Trailers.Remove(db.Trailers.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(Cube).Name)
                            {
                                db.Cubes.Remove(db.Cubes.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(TirCmr).Name)
                            {
                                db.TirCmrs.Remove(db.TirCmrs.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(Cargo).Name)
                            {
                                db.Cargoes.Remove(db.Cargoes.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(LoadingForm).Name)
                            {
                                db.LoadingForms.Remove(db.LoadingForms.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(Payment).Name)
                            {
                                db.Payments.Remove(db.Payments.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(AdditionalTerm).Name)
                            {
                                db.AdditionalTerms.Remove(db.AdditionalTerms.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(RegularyDelay).Name)
                            {
                                db.RegularyDelays.Remove(db.RegularyDelays.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(FineForDelay).Name)
                            {
                                db.FineForDelays.Remove(db.FineForDelays.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(OrderDeny).Name)
                            {
                                db.OrderDenies.Remove(db.OrderDenies.Find(deleteObjectId));
                            }
                            else
                            {
                                return;
                            }
                            try
                            {
                                db.SaveChanges();
                                MessageBox.Show(AtlantSovt.Properties.Resources.Запис_успішно_видалено);
                            }
                            catch (Exception ex)
                            {
                                Log.Write(ex);
                                MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(AtlantSovt.Properties.Resources.Спочатку_виберіть_запис);

                    }                    
                }
                catch(Exception exx)
                {
                    Log.Write(exx);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + exx.Message);
                }
            }
        }
        private void LoadItems<T>(IQueryable<T> tableSelector)
        {
            deleteItemSelectComboBox.SelectedIndex = -1;
            deleteItemSelectComboBox.Items.Clear();
            deleteDiaposoneSelectComboBox.Enabled = false;

            using (var db = new AtlantSovtContext())
            {
                try
                {
                    var query = from t in tableSelector
                                select t;

                    foreach (var item in query)
                    {

                        if (tableSelector.ElementType.Name == typeof(Country).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as Country).Name + " [" + (item as Country).Id + "]");
                        }
                        else if (tableSelector.ElementType.Name == typeof(Vehicle).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as Vehicle).Type + " [" + (item as Vehicle).Id + "]");
                        }
                        else if (tableSelector.ElementType.Name == typeof(TaxPayerStatu).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as TaxPayerStatu).Status + " [" + (item as TaxPayerStatu).Id + "]");
                        }
                        else if (tableSelector.ElementType.Name == typeof(WorkDocument).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as WorkDocument).Status + " [" + (item as WorkDocument).Id + "]");
                        }
                        else if (tableSelector.ElementType.Name == typeof(Staff).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as Staff).Type + " [" + (item as Staff).Id + "]");
                        }
                        else if (tableSelector.ElementType.Name == typeof(Trailer).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as Trailer).Type + " [" + (item as Trailer).Id + "]");
                        }
                        else if (tableSelector.ElementType.Name == typeof(Cube).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as Cube).Type + " [" + (item as Cube).Id + "]");
                        }
                        else if (tableSelector.ElementType.Name == typeof(TirCmr).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as TirCmr).Type + " [" + (item as TirCmr).Id + "]");
                        }
                        else if (tableSelector.ElementType.Name == typeof(Cargo).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as Cargo).Type + " [" + (item as Cargo).Id + "]");
                        }
                        else if (tableSelector.ElementType.Name == typeof(LoadingForm).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as LoadingForm).Type + " [" + (item as LoadingForm).Id + "]");
                        }
                        else if (tableSelector.ElementType.Name == typeof(Payment).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as Payment).Type + " [" + (item as Payment).Id + "]");
                        }
                        else if (tableSelector.ElementType.Name == typeof(AdditionalTerm).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as AdditionalTerm).Type + " [" + (item as AdditionalTerm).Id + "]");
                        }
                        else if (tableSelector.ElementType.Name == typeof(RegularyDelay).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as RegularyDelay).Type + " [" + (item as RegularyDelay).Id + "]");
                        }
                        else if (tableSelector.ElementType.Name == typeof(FineForDelay).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as FineForDelay).Type + " [" + (item as FineForDelay).Id + "]");
                        }
                        else if (tableSelector.ElementType.Name == typeof(OrderDeny).Name)
                        {
                            deleteItemSelectComboBox.Items.Add((item as OrderDeny).Type + " [" + (item as OrderDeny).Id + "]");
                        }
                        else
                        {
                            return;
                        }
                    }
                    deleteItemSelectComboBox.Enabled = true;
                    deleteItemSelectComboBox.Visible = true;
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                }
            }
        }

        private void SplitItems<T>(IQueryable<T> tableSelector)
        {
            using (var db = new AtlantSovtContext())
            {
                try {
                    if (deleteItemSelectComboBox.SelectedIndex != -1 && deleteItemSelectComboBox.Text == deleteItemSelectComboBox.SelectedItem.ToString())
                    {
                        string comboboxText = deleteItemSelectComboBox.SelectedItem.ToString();
                        string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                        string comboBoxSelectedId = selectedNameAndDirector[1];
                        long id = Convert.ToInt64(comboBoxSelectedId);

                        if (tableSelector.ElementType.Name == typeof(Country).Name)
                        {
                            deleteObjectId = db.Countries.Find(id).Id;
                        }
                        else if (tableSelector.ElementType.Name == typeof(Vehicle).Name)
                        {
                            deleteObjectId = db.Vehicles.Find(id).Id;
                        }
                        else if (tableSelector.ElementType.Name == typeof(TaxPayerStatu).Name)
                        {
                            deleteObjectId = db.TaxPayerStatus.Find(id).Id;
                        }
                        else if (tableSelector.ElementType.Name == typeof(WorkDocument).Name)
                        {
                            deleteObjectId = db.WorkDocuments.Find(id).Id;
                        }
                        else if (tableSelector.ElementType.Name == typeof(Staff).Name)
                        {
                            deleteObjectId = db.Staffs.Find(id).Id;
                        }
                        else if (tableSelector.ElementType.Name == typeof(Trailer).Name)
                        {
                            deleteObjectId = db.Trailers.Find(id).Id;
                        }
                        else if (tableSelector.ElementType.Name == typeof(Cube).Name)
                        {
                            deleteObjectId = db.Cubes.Find(id).Id;
                        }
                        else if (tableSelector.ElementType.Name == typeof(TirCmr).Name)
                        {
                            deleteObjectId = db.TirCmrs.Find(id).Id;
                        }
                        else if (tableSelector.ElementType.Name == typeof(Cargo).Name)
                        {
                            deleteObjectId = db.Cargoes.Find(id).Id;
                        }
                        else if (tableSelector.ElementType.Name == typeof(LoadingForm).Name)
                        {
                            deleteObjectId = db.LoadingForms.Find(id).Id;
                        }
                        else if (tableSelector.ElementType.Name == typeof(Payment).Name)
                        {
                            deleteObjectId = db.Payments.Find(id).Id;
                        }
                        else if (tableSelector.ElementType.Name == typeof(AdditionalTerm).Name)
                        {
                            deleteObjectId = db.AdditionalTerms.Find(id).Id;
                        }
                        else if (tableSelector.ElementType.Name == typeof(RegularyDelay).Name)
                        {
                            deleteObjectId = db.RegularyDelays.Find(id).Id;
                        }
                        else if (tableSelector.ElementType.Name == typeof(FineForDelay).Name)
                        {
                            deleteObjectId = db.FineForDelays.Find(id).Id;
                        }
                        else if (tableSelector.ElementType.Name == typeof(OrderDeny).Name)
                        {
                            deleteObjectId = db.OrderDenies.Find(id).Id;
                        }
                        else
                        {
                            deleteItemSelectComboBox.SelectedIndex = -1;
                            deleteItemSelectComboBox.Items.Clear();
                            deleteObjectId = null;
                            return;
                        }
                    }
                    else
                    {
                        deleteItemSelectComboBox.SelectedIndex = -1;
                        deleteItemSelectComboBox.Items.Clear();
                        deleteObjectId = null;
                        return;
                    }
                    deteteAdditionsButton.Enabled = true;
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                }
            }
        }

        private void DeleteItems<T>(IQueryable<T> tableSelector, ComboBox combo)
        {
            using (var db = new AtlantSovtContext())
            {
                try
                {
                    if (deleteObjectId != null)
                    {
                        if (MessageBox.Show(AtlantSovt.Properties.Resources.Видалити_запис + "?" + "\n" + AtlantSovt.Properties.Resources.Видалення_цього_запису_приведе_до_його_втрати_у_звязаних_таблицях, AtlantSovt.Properties.Resources.Підтвердіть_видалення, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {                         
                            
                            if (tableSelector.ElementType.Name == typeof(DownloadAddress).Name)
                            {
                                db.DownloadAddresses.Remove(db.DownloadAddresses.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(UploadAddress).Name)
                            {
                                db.UploadAddresses.Remove(db.UploadAddresses.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(UnCustomsAddress).Name)
                            {
                                db.UnCustomsAddresses.Remove(db.UnCustomsAddresses.Find(deleteObjectId));
                            }
                            else if (tableSelector.ElementType.Name == typeof(CustomsAddress).Name)
                            {
                                db.CustomsAddresses.Remove(db.CustomsAddresses.Find(deleteObjectId));
                            }
                            else
                            {
                                return;
                            }
                            try
                            {
                                db.SaveChanges();
                                MessageBox.Show(AtlantSovt.Properties.Resources.Запис_успішно_видалено);
                            }
                            catch (Exception ex)
                            {
                                Log.Write(ex);
                                MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(AtlantSovt.Properties.Resources.Спочатку_виберіть_запис);

                    }
                }
                catch (Exception exx)
                {
                    Log.Write(exx);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + exx.Message);
                }
            }
        }
        private void LoadItems<T>(IQueryable<T> tableSelector, ComboBox combo)
        {
            deleteItemSelectComboBox.SelectedIndex = -1;
            deleteItemSelectComboBox.Items.Clear();

            using (var db = new AtlantSovtContext())
            {
                try
                {
                    if (deleteDiaposoneSelectComboBox.Text == "")
                    {
                        MessageBox.Show(AtlantSovt.Properties.Resources.Ви_не_вибрали_діапазон);
                    }
                    else
                    {
                        if (tableSelector.ElementType.Name == typeof(UploadAddress).Name)
                        {
                            string text = deleteDiaposoneSelectComboBox.SelectedItem.ToString();
                            string[] diapasone = text.Split(new char[] { ' ' });
                            int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                            int diapasoneTo = Convert.ToInt32(diapasone[2]);

                            var query = from c in db.UploadAddresses
                                        where c.Id >= diapasoneFrom
                                        && c.Id <= diapasoneTo
                                        select c;
                            foreach (var item in query)
                            {
                                if (item.Country != null)
                                {
                                    deleteItemSelectComboBox.Items.Add(item.Country.Name + "," + item.CountryCode + "," + item.CityName + "," + item.StreetName + "," + item.HouseNumber + "[" + item.Id + "]");
                                }
                                else
                                {
                                    deleteItemSelectComboBox.Items.Add(item.CountryCode + "," + item.CityName + "," + item.StreetName + "," + item.HouseNumber + "[" + item.Id + "]");
                                }
                            }

                        }
                        else if (tableSelector.ElementType.Name == typeof(DownloadAddress).Name)
                        {
                            string text = deleteDiaposoneSelectComboBox.SelectedItem.ToString();
                            string[] diapasone = text.Split(new char[] { ' ' });
                            int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                            int diapasoneTo = Convert.ToInt32(diapasone[2]);
                            var query = from c in db.DownloadAddresses
                                        where c.Id >= diapasoneFrom
                                        && c.Id <= diapasoneTo
                                        select c;
                            foreach (var item in query)
                            {
                                if (item.Country != null)
                                {
                                    deleteItemSelectComboBox.Items.Add(item.Country.Name + "," + item.CountryCode + "," + item.CityName + "," + item.StreetName + "," + item.HouseNumber + "[" + item.Id + "]");
                                }
                                else
                                {
                                    deleteItemSelectComboBox.Items.Add(item.CountryCode + "," + item.CityName + "," + item.StreetName + "," + item.HouseNumber + "[" + item.Id + "]");
                                }
                            }
                        }
                        else if (tableSelector.ElementType.Name == typeof(CustomsAddress).Name)
                        {
                            string text = deleteDiaposoneSelectComboBox.SelectedItem.ToString();
                            string[] diapasone = text.Split(new char[] { ' ' });
                            int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                            int diapasoneTo = Convert.ToInt32(diapasone[2]);
                            var query = from c in db.CustomsAddresses
                                        where c.Id >= diapasoneFrom
                                        && c.Id <= diapasoneTo
                                        select c;
                            foreach (var item in query)
                            {
                                if (item.Country != null)
                                {
                                    deleteItemSelectComboBox.Items.Add(item.Country.Name + "," + item.CountryCode + "," + item.CityName + "," + item.StreetName + "," + item.HouseNumber + "[" + item.Id + "]");
                                }
                                else
                                {
                                    deleteItemSelectComboBox.Items.Add(item.CountryCode + "," + item.CityName + "," + item.StreetName + "," + item.HouseNumber + "[" + item.Id + "]");
                                }
                            }
                        }
                        else if (tableSelector.ElementType.Name == typeof(UnCustomsAddress).Name)
                        {
                            string text = deleteDiaposoneSelectComboBox.SelectedItem.ToString();
                            string[] diapasone = text.Split(new char[] { ' ' });
                            int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                            int diapasoneTo = Convert.ToInt32(diapasone[2]);
                            var query = from c in db.UnCustomsAddresses
                                        where c.Id >= diapasoneFrom
                                        && c.Id <= diapasoneTo
                                        select c;
                            foreach (var item in query)
                            {
                                if (item.Country != null)
                                {
                                    deleteItemSelectComboBox.Items.Add(item.Country.Name + "," + item.CountryCode + "," + item.CityName + "," + item.StreetName + "," + item.HouseNumber + "[" + item.Id + "]");
                                }
                                else
                                {
                                    deleteItemSelectComboBox.Items.Add(item.CountryCode + "," + item.CityName + "," + item.StreetName + "," + item.HouseNumber + "[" + item.Id + "]");
                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    deleteItemSelectComboBox.Enabled = true;
                    deleteItemSelectComboBox.Visible = true;
                }

                catch (Exception exx)
                {
                    Log.Write(exx);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + exx.Message);
                }
            }
        }        
        private void SplitItems<T>(IQueryable<T> tableSelector, ComboBox combo)
        {
            using (var db = new AtlantSovtContext())
            {
                try
                {                   
                    if (deleteItemSelectComboBox.SelectedIndex != -1 && deleteItemSelectComboBox.Text == deleteItemSelectComboBox.SelectedItem.ToString())
                    {
                        string comboboxText = deleteItemSelectComboBox.SelectedItem.ToString();
                        string[] selectedNameAndDirector = comboboxText.Split(new char[] { '[', ']' });
                        string comboBoxSelectedId = selectedNameAndDirector[1];
                        long id = Convert.ToInt64(comboBoxSelectedId);
                        if (tableSelector.ElementType.Name == typeof(UploadAddress).Name)
                            {
                                deleteObjectId = db.UploadAddresses.Find(id).Id;
                            }
                            else if (tableSelector.ElementType.Name == typeof(DownloadAddress).Name)
                            {
                                deleteObjectId = db.DownloadAddresses.Find(id).Id;
                            }
                            else if (tableSelector.ElementType.Name == typeof(CustomsAddress).Name)
                            {
                                deleteObjectId = db.CustomsAddresses.Find(id).Id;
                            }
                            else if (tableSelector.ElementType.Name == typeof(UnCustomsAddress).Name)
                            {
                                deleteObjectId = db.UnCustomsAddresses.Find(id).Id;
                            }
                            else
                            {
                                deleteItemSelectComboBox.SelectedIndex = -1;
                                deleteItemSelectComboBox.Items.Clear();
                                deleteObjectId = null;
                                return;
                            }
                        }
                    
                    else
                    {
                        deleteItemSelectComboBox.SelectedIndex = -1;
                        deleteItemSelectComboBox.Items.Clear();
                        deleteObjectId = null;
                        return;
                    } 
                    deteteAdditionsButton.Enabled = true;
                }
                catch (Exception exx)
                {
                    Log.Write(exx);
                    MessageBox.Show(AtlantSovt.Properties.Resources.Помилка + exx.Message);
                }
            }
        }
        private void LoadDiapasone<T>(IQueryable<T> tableSelector, ComboBox combo)
        {
            deleteDiaposoneSelectComboBox.Items.Clear();
            deleteDiaposoneSelectComboBox.SelectedIndex = -1;
            deleteItemSelectComboBox.Items.Clear();
            deleteItemSelectComboBox.SelectedIndex = -1;
            using (var db = new AtlantSovtContext())
            {
                int part = 1000;
                double addressPart = 0;
                if ((from c in tableSelector select c).Count() != 0)
                {
                    long addressCount = 0;
                    if (tableSelector.ElementType.Name == typeof(UploadAddress).Name)
                    {
                        addressCount = (from c in db.UploadAddresses select c.Id).Max();
                    }
                    else if (tableSelector.ElementType.Name == typeof(DownloadAddress).Name)
                    {
                        addressCount = (from c in db.DownloadAddresses select c.Id).Max();
                    }
                    else if (tableSelector.ElementType.Name == typeof(CustomsAddress).Name)
                    {
                        addressCount = (from c in db.CustomsAddresses  select c.Id).Max();
                    }
                    else if (tableSelector.ElementType.Name == typeof(UnCustomsAddress).Name)
                    {
                        addressCount = (from c in db.UnCustomsAddresses select c.Id).Max();
                    }
                    if (addressCount % part == 0)
                    {
                        addressPart = addressCount / part;
                    }
                    else
                    {
                        addressPart = (addressCount / part) + 1;
                    }

                    for (int i = 0; i<addressPart; i++)
                    {
                        combo.Items.Add(((i* part) + 1) + " - " + ((i + 1) * part));
                    }
                    deleteDiaposoneSelectComboBox.Enabled = true;
                    deleteDiaposoneSelectComboBox.Visible = true;

                }
                else
                {
                    //MessageBox.Show(AtlantSovt.Properties.Resources.Немає_жодного_запису);
                }
            }}

        private void LoadTabels()
        {
            List<string> Tabels = new List<string>
            {
                AtlantSovt.Properties.Resources.Адреса_завантаження,
                AtlantSovt.Properties.Resources.Адреса_розвантаження,
                AtlantSovt.Properties.Resources.Адреса_замитнення,
                AtlantSovt.Properties.Resources.Адреса_розмитнення,
                AtlantSovt.Properties.Resources.Країни,
                AtlantSovt.Properties.Resources.Типи_транспорту,
                AtlantSovt.Properties.Resources.Статус_платника_податку,
                AtlantSovt.Properties.Resources.На_основі_документу,
                AtlantSovt.Properties.Resources.Працівники,
                AtlantSovt.Properties.Resources.Причіп,
                AtlantSovt.Properties.Resources.Куб,
                AtlantSovt.Properties.Resources.TIR + "/" + AtlantSovt.Properties.Resources.CMR,
                AtlantSovt.Properties.Resources.Типи_вантажу,
                AtlantSovt.Properties.Resources.Форми_завантаження,
                AtlantSovt.Properties.Resources.Умови_оплати,
                AtlantSovt.Properties.Resources.Додаткові_умови,
                AtlantSovt.Properties.Resources.Нормативні_простої,
                AtlantSovt.Properties.Resources.Штрафи_за_простої,
                AtlantSovt.Properties.Resources.Штрафи_за_відмову_від_заявки,
            };
            foreach (string tabl in Tabels)
            {
                deleteTableSelectComboBox.Items.Add(tabl);
            }
        }
        private void DeleteItems()
        {
            using (var db = new AtlantSovtContext())
            {
                switch (deleteTableSelectComboBox.SelectedIndex)
                {
                    case -1: deleteObjectId = null; break;
                    case 0: DeleteItems<DownloadAddress>(db.DownloadAddresses, deleteDiaposoneSelectComboBox); break;//"Адреси завантаження",
                    case 1: DeleteItems<UploadAddress>(db.UploadAddresses, deleteDiaposoneSelectComboBox); break;//"Адреси розвантаження",
                    case 2: DeleteItems<CustomsAddress>(db.CustomsAddresses, deleteDiaposoneSelectComboBox); break; //"Адреси замитнення",
                    case 3: DeleteItems<UnCustomsAddress>(db.UnCustomsAddresses, deleteDiaposoneSelectComboBox); break;// "Адреси розмитнення",
                    case 4: DeleteItems<Country>(db.Countries); break;//"Країни",
                    case 5: DeleteItems<Vehicle>(db.Vehicles); break;//"Типи транспорту",
                    case 6: DeleteItems<TaxPayerStatu>(db.TaxPayerStatus); break;//"Статус платника податку",
                    case 7: DeleteItems<WorkDocument>(db.WorkDocuments); break;//"На основі документу",
                    case 8: DeleteItems<Staff>(db.Staffs); break; //"Працівники",
                    case 9: DeleteItems<Trailer>(db.Trailers); break;//"Причіп",
                    case 10: DeleteItems<Cube>(db.Cubes); break;//"Куб",
                    case 11: DeleteItems<TirCmr>(db.TirCmrs); break; //"Tir/Cmr",
                    case 12: DeleteItems<Cargo>(db.Cargoes); break; //"Типи вантажу",
                    case 13: DeleteItems<LoadingForm>(db.LoadingForms); break; //"Форми завантаження",
                    case 14: DeleteItems<Payment>(db.Payments); break; //"Умови оплати",
                    case 15: DeleteItems<AdditionalTerm>(db.AdditionalTerms); break; //"Додаткові умови",
                    case 16: DeleteItems<RegularyDelay>(db.RegularyDelays); break; //"Нормативні простої",
                    case 17: DeleteItems<FineForDelay>(db.FineForDelays); break;  //"Штрафи за простої",
                    case 18: DeleteItems<OrderDeny>(db.OrderDenies); break;  //"Штрафи за відмову від заявки",
                    default: deleteObjectId = null; break;
                }
            }
        }
        private void SplitItems()
        {
            using (var db = new AtlantSovtContext())
            {
                switch (deleteTableSelectComboBox.SelectedIndex)
                {
                    case -1: deleteObjectId = null; break;
                    case 0:  SplitItems<DownloadAddress>(db.DownloadAddresses, deleteDiaposoneSelectComboBox); break;//"Адреси завантаження",
                    case 1: SplitItems<UploadAddress>(db.UploadAddresses, deleteDiaposoneSelectComboBox); break;//"Адреси розвантаження",
                    case 2: SplitItems<CustomsAddress>(db.CustomsAddresses, deleteDiaposoneSelectComboBox); break; //"Адреси замитнення",
                    case 3:SplitItems<UnCustomsAddress>(db.UnCustomsAddresses, deleteDiaposoneSelectComboBox); break;// "Адреси розмитнення",
                    case 4: SplitItems<Country>(db.Countries); break;//"Країни",
                    case 5: SplitItems<Vehicle>(db.Vehicles); break;//"Типи транспорту",
                    case 6: SplitItems<TaxPayerStatu>(db.TaxPayerStatus); break;//"Статус платника податку",
                    case 7: SplitItems<WorkDocument>(db.WorkDocuments); break;//"На основі документу",
                    case 8: SplitItems<Staff>(db.Staffs); break; //"Працівники",
                    case 9: SplitItems<Trailer>(db.Trailers); break;//"Причіп",
                    case 10: SplitItems<Cube>(db.Cubes); break;//"Куб",
                    case 11: SplitItems<TirCmr>(db.TirCmrs); break; //"Tir/Cmr",
                    case 12: SplitItems<Cargo>(db.Cargoes); break; //"Типи вантажу",
                    case 13: SplitItems<LoadingForm>(db.LoadingForms); break; //"Форми завантаження",
                    case 14: SplitItems<Payment>(db.Payments); break; //"Умови оплати",
                    case 15: SplitItems<AdditionalTerm>(db.AdditionalTerms); break; //"Додаткові умови",
                    case 16: SplitItems<RegularyDelay>(db.RegularyDelays); break; //"Нормативні простої",
                    case 17: SplitItems<FineForDelay>(db.FineForDelays); break;  //"Штрафи за простої",
                    case 18: SplitItems<OrderDeny>(db.OrderDenies); break;  //"Штрафи за відмову від заявки",
                    default: deleteObjectId = null; break;
                }
            }
        }
        private void LoadItems()
        {
            using (var db = new AtlantSovtContext())
            {
                
                switch (deleteTableSelectComboBox.SelectedIndex)
                {
                    case -1: deleteObjectId = null; break;
                    case 0: LoadItems<DownloadAddress>(db.DownloadAddresses, deleteDiaposoneSelectComboBox); break;//"Адреси завантаження",
                                                                                                                       //LoadItems<CustomsAddress>(db.CustomsAddresses, deleteDiaposoneSelectComboBox); break;
                    case 1: LoadItems<UploadAddress>(db.UploadAddresses, deleteDiaposoneSelectComboBox); break;//"Адреси розвантаження",
                                                                                                                     //LoadItems<UploadAddress>(db.UploadAddresses, deleteDiaposoneSelectComboBox); break;
                    case 2: LoadItems<CustomsAddress>(db.CustomsAddresses, deleteDiaposoneSelectComboBox); break;
                    //LoadItems<CustomsAddress>(db.CustomsAddresses, deleteDiaposoneSelectComboBox); break; //"Адреси замитнення",
                    case 3: LoadItems<UnCustomsAddress>(db.UnCustomsAddresses, deleteDiaposoneSelectComboBox); break;
                    //LoadItems<UnCustomsAddress>(db.UnCustomsAddresses, deleteDiaposoneSelectComboBox); break;// "Адреси розмитнення",
                    case 4: LoadItems<Country>(db.Countries); break;//"Країни",
                    case 5: LoadItems<Vehicle>(db.Vehicles); break;//"Типи транспорту",
                    case 6: LoadItems<TaxPayerStatu>(db.TaxPayerStatus); break;//"Статус платника податку",
                    case 7: LoadItems<WorkDocument>(db.WorkDocuments); break;//"На основі документу",
                    case 8: LoadItems<Staff>(db.Staffs); break; //"Працівники",
                    case 9: LoadItems<Trailer>(db.Trailers); break;//"Причіп",
                    case 10: LoadItems<Cube>(db.Cubes); break;//"Куб",
                    case 11: LoadItems<TirCmr>(db.TirCmrs); break; //"Tir/Cmr",
                    case 12: LoadItems<Cargo>(db.Cargoes); break; //"Типи вантажу",
                    case 13: LoadItems<LoadingForm>(db.LoadingForms); break; //"Форми завантаження",
                    case 14: LoadItems<Payment>(db.Payments); break; //"Умови оплати",
                    case 15: LoadItems<AdditionalTerm>(db.AdditionalTerms); break; //"Додаткові умови",
                    case 16: LoadItems<RegularyDelay>(db.RegularyDelays); break; //"Нормативні простої",
                    case 17: LoadItems<FineForDelay>(db.FineForDelays); break;  //"Штрафи за простої",
                    case 18: LoadItems<OrderDeny>(db.OrderDenies); break;  //"Штрафи за відмову від заявки",
                    default: deleteObjectId = null; break;
                }
            }
        }
        private void LoadDiapasone()
        {
            using (var db = new AtlantSovtContext())
            {
                deleteItemSelectComboBox.SelectedIndex = -1;
                deleteItemSelectComboBox.Items.Clear();
                switch (deleteTableSelectComboBox.SelectedIndex)
                {
                    case -1: deleteObjectId = null; break;
                    case 0:
                        LoadDiapasone<DownloadAddress>(db.DownloadAddresses, deleteDiaposoneSelectComboBox);//"Адреси завантаження",
                        break;
                    case 1:
                        LoadDiapasone<UploadAddress>(db.UploadAddresses, deleteDiaposoneSelectComboBox);//"Адреси розвантаження",
                        break;
                    case 2:
                        LoadDiapasone<CustomsAddress>(db.CustomsAddresses, deleteDiaposoneSelectComboBox);
                        break; //"Адреси замитнення",
                    case 3:
                        LoadDiapasone<UnCustomsAddress>(db.UnCustomsAddresses, deleteDiaposoneSelectComboBox);
                        break;// "Адреси розмитнення",
                }
            }
            
            deleteDiaposoneSelectComboBox.DroppedDown = true;
        }

    }
}

