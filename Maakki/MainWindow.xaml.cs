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
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Maakki
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Account> MaakkiList;
        private static int count = 1;
       
        

        public MainWindow()
        {
            InitializeComponent();
            MaakkiList = new List<Account>();
           
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(ShowPic);
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.AutoReset = true;

            
        }

      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            MaakkiList.Add(new Account(MyAccount.Text, MyPassword.Text, proxyText.Text));         
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Account myaccount;
            try {
                myaccount = MaakkiList.Find(x => x.username.Contains(MyAccount.Text));
                System.Diagnostics.Debug.WriteLine(myaccount.password);
            }catch(Exception AccountNull)
            {
                System.Diagnostics.Debug.WriteLine(AccountNull);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Account deleteAccount;
            deleteAccount = MaakkiList.Find(x => x.username.Contains(MyAccount.Text));
            deleteAccount.myThread.Abort();
            deleteAccount.CloseDriver();
            MaakkiList.Remove(deleteAccount);             
        }

        private void Get_Currency(object sender, RoutedEventArgs e)
        {
            getCurrency money = new getCurrency();
            ExchangeRate getUSD = money.currency;
            Currency_Label.Content = "USD="+getUSD.USD;
            
        }

        private void checkProxy_Click(object sender, RoutedEventArgs e)
        {
            Account myaccount;
            myaccount = MaakkiList.Last();
            myaccount.CheckProxy();
        }

        private void kill_chrome(object sender, RoutedEventArgs e)
        {
            Process[] chromeInstance = Process.GetProcessesByName("chrome");
            foreach(Process p in chromeInstance)
            {
                p.Kill();
            }
            chromeInstance = Process.GetProcessesByName("chromedriver");
            foreach (Process p in chromeInstance)
            {
                p.Kill();
            }


            Process[] PhantomjsInstance = Process.GetProcessesByName("phantomjs");
            foreach (Process p in PhantomjsInstance)
            {
                p.Kill();
            }

        }

        private void ShowPic(object sender, System.Timers.ElapsedEventArgs e)
        {
      
            try {

                Dispatcher.Invoke(() =>
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    FileStream myPicStream = File.OpenRead(Environment.CurrentDirectory + @"\mypic.png");
                    bitmap.StreamSource = (Stream)myPicStream;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    mypic_png.Source = bitmap;
                    myPicStream.Close();
                });       
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex);
            }

        }

       



        private void PicTemp(object sender, RoutedEventArgs e)
        {
            mypic_png.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\TEMP.png"));
        }
    }
}
