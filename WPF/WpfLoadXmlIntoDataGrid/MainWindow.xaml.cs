using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            /* 
            // 1.
            this.l1.Items.Add("a");
            this.l1.Items.Add("b");
            this.l1.Items.Add("c");
            */

            /* 
            // 2.
            ArrayList al = new ArrayList();
            al.Add("a");
            al.Add("b");
            al.Add("c");
            this.l1.DataContext = al;
             * 
            <ListBox x:Name="l1" ItemsSource="{Binding}">
            </ListBox>
             * 
             */

            /*
            // 3.
            XmlDocument xd = new XmlDocument();
            xd.Load(@"file://C:\projects\NibrApplicationCatalog\Development\src\NibrApplicationCatalog\NibrApplicationCatalog.DatabaseDevelopment\V3.4\NAC-1775_new_sccm\WpfApplication1\glchbs-sp320153_getAllCatalogItems.xml");
            XmlDataProvider xdp = new XmlDataProvider();
            xdp.Document = xd;
            this.l1.DataContext = xdp;

            <ListBox x:Name="l1" ItemsSource="{Binding XPath=//CatalogItems/PackageID}">
            </ListBox>
            */

            XmlDocument xd = new XmlDocument();
            xd.Load(@"file://C:\projects\NibrApplicationCatalog\Development\src\NibrApplicationCatalog\NibrApplicationCatalog.DatabaseDevelopment\V3.4\NAC-1775_new_sccm\WpfApplication1\glchbs-sp320153_getAllCatalogItems.xml");
            XmlDataProvider xdp = new XmlDataProvider();
            xdp.Document = xd;
            xdp.XPath = "//CatalogItems";
            this.DataContext = xdp;
            /*
             * <Window x:Class="WpfApplication1.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="MainWindow" Height="350" Width="800">
    <Window.Resources>       
    </Window.Resources>
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*"></ColumnDefinition>
            <ColumnDefinition Width="300"></ColumnDefinition>
        </Grid.ColumnDefinitions>
    <DataGrid x:Name="dg1" AutoGenerateColumns="False" ItemsSource="{Binding}" Grid.Column="0">
        <DataGrid.Columns>
            <DataGridTextColumn Header="c1" IsReadOnly="True" Binding="{Binding XPath=PackageID}"/>
            <DataGridTextColumn Header="c2" IsReadOnly="True" Binding="{Binding XPath=PackageTitle}"/>
            <DataGridTextColumn Header="c3" IsReadOnly="True" Binding="{Binding XPath=PackageDesc}"/>            
        </DataGrid.Columns>
    </DataGrid>
        <Label x:Name="lb1" Content="{Binding XPath=PackageDesc}"  Grid.Column="1"/>
    </Grid>
</Window>
            */
        }
    }
}
