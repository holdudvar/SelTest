using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using SelTest;

public class Program
{
    private static void Main(string[] args)
    {
        const string testURL_DTITS_pos = "https://www.deutschetelekomitsolutions.hu/nyitott-poziciok/";
        const string askForInput = "Pleae select browser. Key in 'Chrome' or 'Firefox'.";
        const string expectedTitle = "Nyitott pozíciók | Deutsche Telekom IT Solutions";
        const string szakterulet = "Szakterület";
        const string cybotCookiebotDialogBodyButtonDecline = "CybotCookiebotDialogBodyButtonDecline";
        const string test = "Test";
        const string dropdownToggle = "dropdown-toggle";
        const string dropdownItem = "dropdown-item";
        const string tsystemsjobs_results = "tsystemsjobs_results";
        const string tagName_a = "a";
        const string job_title = "job-title";
        const string ref3674c = "REF3674C";
        const string quality_engineer_test_automation_load_test_REF3674C =
            "Quality Engineer Test Automation Load Test (REF3674C)";

        Console.WriteLine(askForInput);
        string browser = Console.ReadLine().Trim().ToLower();

        IWebDriver driver;
        switch (browser)
        {
            case "chrome":
                driver = new ChromeDriver();
                break;
            case "firefox":
                driver = new FirefoxDriver();
                break;
            default:
                throw new ArgumentException("Chosen browser is not supported!");
        }

        //Go to URL
        driver.Navigate().GoToUrl(testURL_DTITS_pos);

        //Reject all cookies
        var rejectAllButton = Utilities.FindWebElement(
            () => driver.FindElement(By.Id(cybotCookiebotDialogBodyButtonDecline)));

        Utilities.WaitUntil(() => rejectAllButton.Enabled);
        rejectAllButton.Click();

        //Maximize and verify window
        driver.Manage().Window.Maximize();
        Assert.AreEqual(expectedTitle, driver.Title);

        //Select 'Test' from 'Szakterület' dropdown list
        var dropdownToggles = driver.FindElements(By.ClassName(dropdownToggle));
        foreach (var element in dropdownToggles)
        {
            if (element.Text != null && element.Text.Equals(szakterulet))
            {
                element.Click();
                break;
            }
        }

        var dropdownItems = driver.FindElements(By.ClassName(dropdownItem));
        foreach (var element in dropdownItems)
        {
            if (element.Text != null && element.Text.Equals(test))
            {
                element.Click();
                break;
            }
        }

        //Refresh page  - to make sure the applied filter is active
        new Actions(driver).SendKeys(Keys.F5);

        //Search for 'REF3674C'
        var jobResultsListing = Utilities.FindWebElement(
            () => driver.FindElement(By.Id(tsystemsjobs_results)));
        var jobResults = jobResultsListing.FindElements(By.TagName(tagName_a));

        // Open the page and switch to it
        foreach (var element in jobResults)
        {
            if (element.Text != null && element.Text.Contains(ref3674c))
            {
                element.Click();
                break;
            }
        }

        Utilities.WaitUntil(() => driver.WindowHandles.Count == 2); //Wait for the new page to appear
        driver.SwitchTo().Window(driver.WindowHandles.Last());

        // Verify the title
        var pageH1 = Utilities.FindWebElement(
            () => driver.FindElement(By.ClassName(job_title)));

        Assert.AreEqual(quality_engineer_test_automation_load_test_REF3674C, pageH1.Text);

        //Minimize window
        driver.Manage().Window.Minimize();

        //Quit driver 
        //driver.Quit();
    }
}
