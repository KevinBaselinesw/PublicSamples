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

using System;
using System.Collections.Generic;
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
using WCFSampleClient.WCFSampleService;

namespace WCFSampleClient.UserControls
{
    /// <summary>
    /// Interaction logic for SuppliersByProduct.xaml
    /// </summary>
    public partial class SuppliersByProduct : UserControl
    {
        int SupplierID;
        ContentControl contentControl;
        WCFType WCFType;

        public SuppliersByProduct(int SupplierID, ContentControl contentControl, WCFType WCFType)
        {
            InitializeComponent();
            this.SupplierID = SupplierID;
            this.contentControl = contentControl;
            this.WCFType = WCFType;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (WCFType == WCFType.SOAP)
            {
                WCFSampleServiceClient Client = null;

                try
                {
                    Client = new WCFSampleService.WCFSampleServiceClient();
                    var Suppliers = Client.GetSuppliersByID(SupplierID);
                    var Supplier = Suppliers.FirstOrDefault();
                    if (Supplier != null)
                    {
                        ReportTitle.Text = string.Format($"The supplier for this product is {Supplier.CompanyName}");
                    }
                    else
                    {
                        ReportTitle.Text = string.Format($"The supplier for this product is unknown");
                    }

                    SuppliersGrid.ItemsSource = Suppliers;
                }
                catch (Exception)
                {
                    if (Client != null)
                    {
                        Client.Abort();
                    }
                }
            }

            if (WCFType == WCFType.REST)
            {
                RESTClient RestClient = new RESTClient(App.NorthwindsServerBaseURL);

                Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    { "id", SupplierID.ToString() }
                };

                var Suppliers = await RestClient.Get<List<SupplierDTO>>("GetSuppliersByID", parameters);
                var Supplier = Suppliers.FirstOrDefault();
                if (Supplier != null)
                {
                    ReportTitle.Text = string.Format($"The supplier for this product is {Supplier.CompanyName}");
                }
                else
                {
                    ReportTitle.Text = string.Format($"The supplier for this product is unknown");
                }

                SuppliersGrid.ItemsSource = Suppliers;
            }


        }

  
    }
}
