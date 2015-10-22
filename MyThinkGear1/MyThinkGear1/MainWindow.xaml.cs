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
using NeuroSky.ThinkGear;
using NeuroSky.ThinkGear.Algorithms;
using NeuroSky.ThinkGear.Algorithm.EEGTools;
using Jayrock.Collections;
using Jayrock.Configuration;
using Jayrock.Diagnostics;
using Jayrock.Json;
using Jayrock.JsonML;
using Jayrock.Reflection;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace MyThinkGear1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
        }

        
        public void Main()
        {
            Connector connector = new Connector();
            connector.DeviceConnected += new EventHandler(OnDeviceConnected);
            connector.DeviceConnectFail += new EventHandler(OnDeviceFail);
            connector.DeviceValidating += new EventHandler(OnDeviceValidating);
            connector.Connect("COM31");
            
            progressBar.ValueChanged += ProgressBar_ValueChanged;            
            
            
            Thread.Sleep(450000);
            
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            progressBar.Value = 0;
            Thread.Sleep(512);
            progressBar.Value = 20;
            Thread.Sleep(512);
            progressBar.Value = 40;
            Thread.Sleep(512);
            progressBar.Value = 60;
            Thread.Sleep(512);
            progressBar.Value = 80;
            Thread.Sleep(512);
            progressBar.Value = 100;
        }

        

        private void OnDeviceValidating(object sender, EventArgs e)
        {
            MessageBox.Show("Validating!:");
        }

        private void OnDeviceFail(object sender, EventArgs e)
        {
            Connector.DeviceEventArgs de = (Connector.DeviceEventArgs)e;
            MessageBox.Show("No Device Found" + de.Device.PortName);
        }

        private void OnDeviceConnected(object sender, EventArgs e)
        {
            Connector.DeviceEventArgs de = (Connector.DeviceEventArgs)e;

            MessageBox.Show("Device found on: " + de.Device.PortName);
            de.Device.DataReceived += new EventHandler(OnDataReceived);
        }
        
        private void OnDataReceived(object sender, EventArgs e)
        {
            // Device d = (Device)sender;

            Device.DataEventArgs de = (Device.DataEventArgs)e;
           // DataRow[] tempDataRowArray = de.DataRowArray;

            TGParser tgParser = new TGParser();
            tgParser.Read(de.DataRowArray);

            

            /* Loops through the newly parsed data of the connected headset*/
            // The comments below indicate and can be used to print out the different data outputs. 

            for (int i = 0; i < tgParser.ParsedData.Length; i++)
            {

                if (tgParser.ParsedData[i].ContainsKey("Raw"))
                {

                    //Console.WriteLine("Raw Value:" + tgParser.ParsedData[i]["Raw"]);

                }
                
                if (tgParser.ParsedData[i].ContainsKey(Properties.Resources.String3))
                {
                    //var poorSig = (byte)tgParser.ParsedData[i][Properties.Resources.String3];

                    //The following line prints the Time associated with the parsed data
                    //Console.WriteLine("Time:" + tgParser.ParsedData[i]["Time"]);

                    //A Poor Signal value of 0 indicates that your headset is fitting properly
                    //Console.WriteLine(Properties.Resources.String4 + tgParser.ParsedData[i][Properties.Resources.String3]);


                }


                if (tgParser.ParsedData[i].ContainsKey("Attention"))
                {

                    //Console.WriteLine("Att Value:" + tgParser.ParsedData[i]["Attention"]);

                }


                if (tgParser.ParsedData[i].ContainsKey("Meditation"))
                {

                    //Console.WriteLine("Med Value:" + tgParser.ParsedData[i]["Meditation"]);

                }


                if (tgParser.ParsedData[i].ContainsKey(Properties.Resources.String1))
                {

                    Console.WriteLine(Properties.Resources.String2 + tgParser.ParsedData[i][Properties.Resources.String1]);

                }

                if (tgParser.ParsedData[i].ContainsKey("BlinkStrength"))
                {

                    //Console.WriteLine("Eyeblink " + tgParser.ParsedData[i]["BlinkStrength"]);

                }

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
        
        private void button_connect_Click(object sender, RoutedEventArgs e)
        {
            Connector connector = new Connector();
            connector.DeviceConnected += new EventHandler(OnDeviceConnected);
            connector.DeviceConnectFail += new EventHandler(OnDeviceFail);
            connector.DeviceValidating += new EventHandler(OnDeviceValidating);
            connector.DeviceDisconnected += new EventHandler(OnDeviceDisconnected);
            connector.Connect("COM31");
            Thread.Sleep(450000);
        }

        private void OnDeviceDisconnected(object sender, EventArgs e)
        {
            MessageBox.Show("Disconnected! :");
        }
        
        private void button_disconnect_Click(object sender, RoutedEventArgs e)
        {
            Connector connector = new Connector();
            connector.Close();
            
        }

        
    }
}
