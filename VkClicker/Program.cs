using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace VkClicker
{
    class Program
    {
        static void Main(string[] args)
        {
            string login = args[0];
            string password = args[1];

            RunClicker(login, password);
        }

        private static void RunClicker(string login, string password)
        {
            var chrome = new ChromeDriver();
            chrome.Navigate().GoToUrl("http://vk.com");
            chrome.FindElement(By.Id("quick_email")).SendKeys(login);
            Thread.Sleep(1000);
            chrome.FindElement(By.Id("quick_pass")).SendKeys(password);
            Thread.Sleep(1000);
            chrome.FindElement(By.Id("quick_login_button")).Click();
            Thread.Sleep(5000);
            
            bool check = true;
            while(check)
            {
                try
                {
                    var e = chrome.FindElement(By.ClassName("captcha"));
                }
                catch
                {
                    check = false;
                    continue;
                }
                Thread.Sleep(2000);
            }

            chrome.Navigate().GoToUrl("http://vk.com/friends?section=out_requests");
            Thread.Sleep(2000);
            
            var list = chrome.FindElements(By.CssSelector(".flat_button.fl_l"));
            do
            {
                foreach (var el in list)
                {
                    try
                    {
                        el.Click();
                    }
                    catch { }
                }

                chrome.Navigate().Refresh();
                Thread.Sleep(2000);
                list = chrome.FindElements(By.CssSelector(".flat_button.fl_l"));
            } while (list != null);

            chrome.Close();
        }
    }
}
