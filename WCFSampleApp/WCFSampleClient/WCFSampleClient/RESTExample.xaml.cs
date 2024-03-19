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
using System.Windows.Shapes;
using WCFSampleClient.UserControls;

namespace WCFSampleClient
{
    /// <summary>
    /// Interaction logic for RESTExample.xaml
    /// </summary>
    public partial class RESTExample : Window
    {
        public RESTExample()
        {
            InitializeComponent();
        }

        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            gridSplitter.UpdateLayout();
            PrimaryContent.Content = new Employees(SecondaryContent, WCFType.REST);
        }

        private void ProductCategories_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Categories(SecondaryContent, WCFType.REST);
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Products(SecondaryContent, WCFType.REST);
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Customers(SecondaryContent, WCFType.REST);
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Orders(SecondaryContent, WCFType.REST);
        }

        private void Suppliers_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Suppliers(SecondaryContent, WCFType.REST);
        }

        private void Shippers_Click(object sender, RoutedEventArgs e)
        {
            SecondaryContent.Content = null;
            PrimaryContent.Content = new Shippers(SecondaryContent, WCFType.SOAP);
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            var aboutBoxSoap = new AboutBoxSOAP();
            aboutBoxSoap.ShowDialog();
        }
    }
}
