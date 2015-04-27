using AtlantSovt.AtlantSovtDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    partial class MainForm
    {

        void LoadClientFirstPersonDiapasonCombobox()
        {
            firstPersonDiapasonComboBox.Items.Clear();
            firstPersonNameComboBox.Items.Clear();
            firstPersonNameComboBox.Text = "";
            using (var db = new AtlantSovtContext())
            {
                int part = 1000;
                double clientPart = 0;
                if ((from c in db.Clients select c.Id).Count() != 0)
                {
                    long clientCount = (from c in db.Clients select c.Id).Max();
                    if (clientCount % part == 0)
                    {
                        clientPart = clientCount / part;
                    }
                    else
                    {
                        clientPart = (clientCount / part) + 1;
                    }

                    for (int i = 0; i < clientPart; i++)
                    {
                        firstPersonDiapasonComboBox.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    firstPersonDiapasonComboBox.DroppedDown = true;
                    firstPersonNameComboBox.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Немає жодних записів");
                }
            }
        }

        void LoadClientFirstPersonNameComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (firstPersonDiapasonComboBox.Text == "")
                {
                    MessageBox.Show("Ви не вибрали діапазон");
                }
                else
                {
                    string text = firstPersonDiapasonComboBox.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Clients
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        firstPersonNameComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }

        void LoadTransporterFirstPersonDiapasonCombobox()
        {
            firstPersonDiapasonComboBox.Items.Clear();
            firstPersonNameComboBox.Items.Clear();
            firstPersonNameComboBox.Text = "";
            using (var db = new AtlantSovtContext())
            {
                int part = 1000;
                double transporterPart = 0;
                if ((from c in db.Transporters select c.Id).Count() != 0)
                {
                    long transporterCount = (from c in db.Transporters select c.Id).Max();
                    if (transporterCount % part == 0)
                    {
                        transporterPart = transporterCount / part;
                    }
                    else
                    {
                        transporterPart = (transporterCount / part) + 1;
                    }

                    for (int i = 0; i < transporterPart; i++)
                    {
                        firstPersonDiapasonComboBox.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    firstPersonDiapasonComboBox.DroppedDown = true;
                    firstPersonNameComboBox.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Немає жодних записів");
                }
            }
        }

        void LoadTransporterFirstPersonNameComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (firstPersonDiapasonComboBox.Text == "")
                {
                    MessageBox.Show("Ви не вибрали діапазон");
                }
                else
                {
                    string text = firstPersonDiapasonComboBox.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Transporters
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        firstPersonNameComboBox.Items.Add(item.FullName + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }

        void LoadForwarderFirstPersonNameComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from f in db.Forwarders
                            orderby f.Id
                            select f;
                foreach (var item in query)
                {
                    firstPersonNameComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                }
            }
        }



        void LoadSecondPersonActivityComboBox()
        {
            switch(firstPersonActivityComboBox.SelectedItem.ToString())
            {
                case "Клієнт":
                    secondPersonActivityComboBox.Items.Clear();
                    secondPersonActivityComboBox.Items.Add("Експедитор");
                    break;
                case "Перевізник":
                    secondPersonActivityComboBox.Items.Clear();
                    secondPersonActivityComboBox.Items.Add("Експедитор");
                    break;
                case "Експедитор":
                    secondPersonActivityComboBox.Items.Clear();
                    secondPersonActivityComboBox.Items.Add("Експедитор");
                    break;
            }
        }

        void LoadClientSecondPersonDiapasonCombobox()
        {
            secondPersonDiapasonComboBox.Items.Clear();
            secondPersonNameComboBox.Items.Clear();
            secondPersonNameComboBox.Text = "";
            using (var db = new AtlantSovtContext())
            {
                int part = 1000;
                double clientPart = 0;
                if ((from c in db.Clients select c.Id).Count() != 0)
                {
                    long clientCount = (from c in db.Clients select c.Id).Max();
                    if (clientCount % part == 0)
                    {
                        clientPart = clientCount / part;
                    }
                    else
                    {
                        clientPart = (clientCount / part) + 1;
                    }

                    for (int i = 0; i < clientPart; i++)
                    {
                        secondPersonDiapasonComboBox.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    secondPersonDiapasonComboBox.DroppedDown = true;
                    secondPersonNameComboBox.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Немає жодних записів");
                }
            }
        }

        void LoadClientSecondPersonNameComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (secondPersonDiapasonComboBox.Text == "")
                {
                    MessageBox.Show("Ви не вибрали діапазон");
                }
                else
                {
                    string text = secondPersonDiapasonComboBox.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Clients
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        secondPersonNameComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }

        void LoadTransporterSecondPersonDiapasonCombobox()
        {
            secondPersonDiapasonComboBox.Items.Clear();
            secondPersonNameComboBox.Items.Clear();
            secondPersonNameComboBox.Text = "";
            using (var db = new AtlantSovtContext())
            {
                int part = 1000;
                double transporterPart = 0;
                if ((from c in db.Transporters select c.Id).Count() != 0)
                {
                    long transporterCount = (from c in db.Transporters select c.Id).Max();
                    if (transporterCount % part == 0)
                    {
                        transporterPart = transporterCount / part;
                    }
                    else
                    {
                        transporterPart = (transporterCount / part) + 1;
                    }

                    for (int i = 0; i < transporterPart; i++)
                    {
                        secondPersonDiapasonComboBox.Items.Add(((i * part) + 1) + " - " + ((i + 1) * part));
                    }
                    secondPersonDiapasonComboBox.DroppedDown = true;
                    secondPersonNameComboBox.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Немає жодних записів");
                }
            }
        }

        void LoadTransporterSecondPersonNameComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                if (secondPersonDiapasonComboBox.Text == "")
                {
                    MessageBox.Show("Ви не вибрали діапазон");
                }
                else
                {
                    string text = secondPersonDiapasonComboBox.SelectedItem.ToString();
                    string[] diapasone = text.Split(new char[] { ' ' });
                    int diapasoneFrom = Convert.ToInt32(diapasone[0]);
                    int diapasoneTo = Convert.ToInt32(diapasone[2]);
                    var query = from c in db.Transporters
                                orderby c.Id
                                where c.Id >= diapasoneFrom && c.Id <= diapasoneTo
                                select c;
                    foreach (var item in query)
                    {
                        secondPersonNameComboBox.Items.Add(item.FullName + " , " + item.Director + " [" + item.Id + "]");
                    }
                }
            }
        }

        void LoadForwarderSecondPersonNameComboBox()
        {
            using (var db = new AtlantSovtContext())
            {
                var query = from f in db.Forwarders
                            orderby f.Id
                            select f;
                foreach (var item in query)
                {
                    secondPersonNameComboBox.Items.Add(item.Name + " , " + item.Director + " [" + item.Id + "]");
                }
            }
        }


    }
}
