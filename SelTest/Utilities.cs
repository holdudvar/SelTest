using OpenQA.Selenium;
using System.Diagnostics;

namespace SelTest
{
    public class Utilities
    {
        public static bool WaitUntil(Func<bool> exitCondition, int timeout = 60000, int checkInterval = 500)
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                while (sw.ElapsedMilliseconds <= timeout)
                {
                    if (exitCondition())
                        return true;

                    Thread.Sleep(checkInterval);
                }
                throw new TimeoutException("'WaitUntil' timeout is reached!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occured during 'WaitUntil' execution: '{ex.Message}'");
                throw;
            }
        }

        public static IWebElement FindWebElement(Func<IWebElement> find, int timeout = 60000, int checkInterval = 500)
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                while (sw.ElapsedMilliseconds <= timeout)
                {
                    try
                    {
                        Thread.Sleep(checkInterval);

                        IWebElement element = find();   //Throws 'NoSuchElementException' if element is not loaded yet
                        if (element != null)
                        {
                            return element;
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("Element not found. Waiting for next check...");
                    }
                }
                Console.WriteLine("'FindWebElement' timeout is reached with no success! Returning null.");

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occured during 'FindWebElement' execution: '{ex.Message}'");
                throw;
            }
        }

    }
}
