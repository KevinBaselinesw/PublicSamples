﻿/*
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
    /// Interaction logic for OrdersByShipper.xaml
    /// </summary>
    public partial class OrdersByShipper : UserControl
    {
        int ShipperID = 0;
        ContentControl contentControl;
        WCFType WCFType;

        public OrdersByShipper(int ShipperID, ContentControl contentControl, WCFType WCFType)
        {
            InitializeComponent();

            this.ShipperID = ShipperID;
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
                    var OrdersByShipper = Client.GetOrdersByShipVia(ShipperID);
                    var Shippers = Client.GetAllShippers();
                    var FirstShipper = Shippers.FirstOrDefault(t => t.ShipperID == ShipperID);  // all records likely have this
                    if (FirstShipper != null)
                    {
                        ReportTitle.Text = string.Format($"Orders sent by {FirstShipper.CompanyName}");
                    }
                    else
                    {
                        ReportTitle.Text = string.Format($"Shipping company not found in database!");
                    }

                    OrdersGrid.ItemsSource = OrdersByShipper;
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
                    { "id", ShipperID.ToString() }
                };

                var OrdersByShipper = await RestClient.Get<List<OrderDTO>>("GetOrdersByShipVia", parameters);
                var Shippers = await RestClient.Get<List<ShipperDTO>>("GetAllShippers", parameters);
                var FirstShipper = Shippers.FirstOrDefault(t => t.ShipperID == ShipperID);  // all records likely have this
                if (FirstShipper != null)
                {
                    ReportTitle.Text = string.Format($"Orders sent by {FirstShipper.CompanyName}");
                }
                else
                {
                    ReportTitle.Text = string.Format($"Shipping company not found in database!");
                }

                OrdersGrid.ItemsSource = OrdersByShipper;
            }





        }
    }
}
