using System;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
namespace MyThinkGear1
{

    public class Main
    {
        
        public Main()
        {
            ProgressBar progressBar = new ProgressBar();
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
    }
}
