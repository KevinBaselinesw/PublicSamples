/*
 * BSD 3-Clause License
 *
 * Copyright (c) 2024
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 *
 * 3. Neither the name of the copyright holder nor the names of its
 *    contributors may be used to endorse or promote products derived from
 *    this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using DatabaseAccessLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UtilityFunctions;
using WPFSampleApp.Dialogs;
using WPFSampleApp.UserControls;

namespace WPFSampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string NorthwindsDBBackupName = "NorthwindsDB.xml";

        IDataAccessAPI DataAccessAPI;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string AnimationType = ConfigurationManager.AppSettings["AnimationType"];

            foreach (var item in AnimationMenu.Items)
            {
                MenuItem mi = item as MenuItem;
                if (mi != null)
                {
                    if ((string)mi.Header == AnimationType)
                    {
                        mi.IsChecked = true;
                        break;
                    }
                }
            }


            string DatabaseSource = ConfigurationManager.AppSettings["DatabaseSource"];
            if (DatabaseSource == "XMLFile")
            {
                LoadDatabaseFromXMLFile();
            }
            else if (DatabaseSource == "SQLServer")
            {
                LoadDatabaseFromSQLServer();
            }
            else
            {
                string errorMessage = string.Format("The configuration item 'DatabaseSource' not equal to either 'XMLFile' or 'SQLServer'. " +
                                 "Would you like to load the database from XML?");

                var mbResult =  MessageBox.Show(errorMessage, "Invalid DatabaseSource configuration", MessageBoxButton.YesNo);
                if (mbResult == MessageBoxResult.Yes)
                {
                    LoadDatabaseFromXMLFile();
                }
                else
                {
                    DatabaseBackup databaseBackup = new DatabaseBackup();
                    DataAccessAPI = new DataAccessAPIInMemory(databaseBackup);
                }
            }

        }

        private void LoadDatabaseFromSQLServer()
        {
            bool bResult = false;
            try
            {
                DataAccessAPI = new DataAccessAPIDB();
                bResult = DataAccessAPI.DatabaseValidation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Exception thrown connection to SQL Server.  Check connection string! {0}", ex.Message));
            }

            if (bResult == false)
            {
                string errorMessage = string.Format("We failed to load the database from SQL Server. " +
                         "Would you like to load the database from XML?");

                var mbResult = MessageBox.Show(errorMessage, "Invalid Database Load", MessageBoxButton.YesNo);
                if (mbResult == MessageBoxResult.Yes)
                {
                    LoadDatabaseFromXMLFile();
                }
                else
                {
                    DatabaseBackup databaseBackup = new DatabaseBackup();
                    DataAccessAPI = new DataAccessAPIInMemory(databaseBackup);
                }
            }

        }

        private void LoadDatabaseFromXMLFile()
        {
            try
            {
                if (!System.IO.File.Exists(NorthwindsDBBackupName))
                {
                    MessageBox.Show(string.Format("The database XML file {0} is not found!", NorthwindsDBBackupName));
                }
                else
                {
                    string XmlDB = null;
                    DatabaseBackup databaseBackup = null;
                    try
                    {
                        XmlDB = System.IO.File.ReadAllText(NorthwindsDBBackupName);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(string.Format("Found, but unable to read the database XML file {0}!", NorthwindsDBBackupName));
                    }

                    if (XmlDB != null)
                    {
                        try
                        {
                            databaseBackup = Utility.DeserializeXml<DatabaseBackup>(XmlDB);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(string.Format("Found, but unable to DESERIALIZE the database XML file {0}!", NorthwindsDBBackupName));
                        }
                    }

                    if (databaseBackup != null)
                    {
                        DataAccessAPI = new DataAccessAPIInMemory(databaseBackup);
                    }
                }
            }
            finally
            {
                if (DataAccessAPI == null)
                {
                    DatabaseBackup databaseBackup = new DatabaseBackup();
                    DataAccessAPI = new DataAccessAPIInMemory(databaseBackup);
                }
            }
        }

        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            gridSplitter.UpdateLayout();
            PrimaryContent.Content = new Employees(DataAccessAPI, SecondaryContent);
        }

        private void ProductCategories_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Categories(DataAccessAPI, SecondaryContent);
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Products(DataAccessAPI, SecondaryContent);
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Customers(DataAccessAPI, SecondaryContent);
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Orders(DataAccessAPI, SecondaryContent);
        }

        private void Suppliers_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Suppliers(DataAccessAPI, SecondaryContent);
        }

        private void Shippers_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Shippers(DataAccessAPI, SecondaryContent);
        }

        private void AdjustDBDatesToday_Click(object sender, RoutedEventArgs e)
        {
            var bResult = MessageBox.Show(
                "This operation will adjust all dates in the NorthWind demo database to be relative to today.  Do you want to continue?",
                "Adjust Dates?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (bResult == MessageBoxResult.Yes)
            {
                // get the most recent date in all the database records.
                var latestDate = DataAccessAPI.GetLatestDateInDatabase();

                // generate a datetime with today's date and latestDate time (we only want to adjust days)
                DateTime Now = DateTime.Now;
                DateTime Current = new DateTime(Now.Year, Now.Month, Now.Day, latestDate.Hour, latestDate.Minute, latestDate.Second);

                // calculate the difference between current date and latestDate.
                var diff = Current - latestDate;

                // diff.Days contains the number of days to adjust each datetime value in the database.  The latestDate will be set to today.
                DataAccessAPI.AdjustAllDatesInDatabaseByDays(diff.Days);
            }
        }

        private void AdjustDBDates_Click(object sender, RoutedEventArgs e)
        {
            var Dlg = new DatabaseDaysToAdjust();
            var bResult = Dlg.ShowDialog();
            if (bResult == true)
            {
                int DaysToAdjust = Dlg.DaysToAdjust;
                DataAccessAPI.AdjustAllDatesInDatabaseByDays(DaysToAdjust);
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            var aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void AnimationMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            MenuItem SelectedItem = sender as MenuItem;
            if (SelectedItem == null)
                return;

            foreach (var item in AnimationMenu.Items)
            {
                MenuItem mi = item as MenuItem;
                if (mi != null)
                {
                    if (mi.Header != SelectedItem.Header)
                        mi.IsChecked = false;
                }
            }

            PageAnimationType pageAnimationType;
            bool bResult = Enum.TryParse((string)SelectedItem.Header, out pageAnimationType);
            if (bResult)
            {
                App.PageAnimationType = pageAnimationType;
            }

            return;
        }

        private void SaveDBToXML_Click(object sender, RoutedEventArgs e)
        {
            DataAccessAPIDB DataAccessAPI;
            DatabaseBackup DatabaseBackup;


            try
            {
                DataAccessAPI = new DataAccessAPIDB();
                DatabaseBackup = DataAccessAPI.GetDatabaseBackup();
            }
            catch (Exception)
            {
                MessageBox.Show("failure to OPEN Northwinds SQL database. Check connection string");
                return;
            }

            string XmlDB;
            try
            {
                XmlDB = Utility.SerializeXml(DatabaseBackup);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("failure to SERIALIZE Northwinds SQL database {0}", ex.Message));
                return;
            }

            try
            {
                System.IO.File.WriteAllText(NorthwindsDBBackupName, XmlDB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("failure to WRITE {0} XML file to disk {1}", NorthwindsDBBackupName, ex.Message));
                return;
            }

        }

        private void CreateNewOrder_Click(object sender, RoutedEventArgs e)
        {
            var CreateDlg = new CreateNewOrder();
            bool? bReturn = CreateDlg.ShowDialog();
        }
    }
}
