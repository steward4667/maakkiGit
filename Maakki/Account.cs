using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System.Threading;

namespace Maakki
{

 

    public class Account
    {
        public string username { get; set; }
        public string password { get; set; }
        public string proxyIP  { get; set; }
        public double money    { get; set; }
        ChromeDriver driver;
        ChromeOptions options;
        public Thread myThread { get; set; }

        public Account(string id,string pwd,string proxy)
        {
            proxyIP = proxy;
            init();
            username = id;
            password = pwd;
            myThread = new Thread(MaakkiStart);
            myThread.Start();
         

        }

        private void init()
        {
            
            options = new ChromeOptions();
            if (proxyIP != "")
            {
                var proxy = new Proxy();
                proxy.HttpProxy = proxyIP;
                options.Proxy = proxy;
            }
            //no pic
            options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);

            money = 0;        
        }
        

        public void MaakkiStart()
        {
            try
            {
                driver = new ChromeDriver(options);

                driver.Url = "http://www.maakki.com/login.aspx";

                driver.FindElement(By.CssSelector("#Content > div:nth-child(2) > fieldset > div > div:nth-child(3) > label")).Click();
                driver.FindElementById("txtAccount").Clear();
                driver.FindElementById("txtPassword").Clear();
                driver.FindElementById("txtAccount").SendKeys(username);
                driver.FindElementById("txtPassword").SendKeys(password);
                driver.FindElementById("mainPageContent_Button1").Click();
                driver.Url = "http://www.maakki.com/dream/addSponsor.aspx";
            }
            catch (Exception)
            {

               
            }
               
             
           // money = Convert.ToDouble( driver.FindElementById("lblRemain").Text);

        }


        public string CheckProxy()
        {
            string Ip="";
           if (driver!= null)
            {
                PhantomJSDriverService service =  PhantomJSDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;
                service.Proxy = proxyIP;

                PhantomJSDriver ghostDriver = new PhantomJSDriver(service);
                ghostDriver.Navigate().GoToUrl("http://whatismyipaddress.com/zh-cn/index");
                Ip = ghostDriver.FindElementByCssSelector("#section_left > div:nth-child(2)").Text;

            }

            return Ip;

        }
        public void CloseDriver()
        {
            if(driver!=null)
            driver.Quit();
        }

       
    }
}
