using Microsoft.Playwright;
//using Microsoft.Playwright.NUnit;
//using NUnit.Framework;

namespace DataComTest
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests : PageTest
    {
        [SetUp]
        public async Task Setup()
        {
            //Setup detail
            await Page.GotoAsync("https://datacom.com/nz/en/contact-us");
        }

        [TearDown]
        public async Task Teardown()
        {
            //Close down detail
            await Page.CloseAsync();
        }

        [Test]
        public async Task TestLoad_GetInTouchPage()
        {
            //Except all cookies
            var acceptCookies = Page.GetByRole(AriaRole.Button, new() { NameString = "Accept all" });
            if (await acceptCookies.IsVisibleAsync())
                await Page.GetByRole(AriaRole.Button, new() { NameString = "Accept all" }).ClickAsync();

            var datacomLogo = Page.GetByRole(AriaRole.Link, new() { NameString = "Datacom logo" });

            //await Expect(Page.GetByRole(AriaRole.Link, new() { NameString = "Datacom logo" })).ToBeVisibleAsync();
            await Expect(datacomLogo).ToBeVisibleAsync();
            await Expect(datacomLogo).ToHaveAttributeAsync("src", "https://assets.datacom.com/is/content/datacom/Datacom-Primary-Logo-RGB?$header-mega-logo$");
            await Expect(Page.GetByRole(AriaRole.Heading, new() { NameString = "Our locations" })).ToBeVisibleAsync();
            await Expect(Page.Locator("body")).ToContainTextAsync("Contact one of our global offices or one of our teams to find out more about how we can help you, or to answer any query you may have.");
        }

        [Test]
        public async Task TestNewZealandRegionalContactDetails()
        {
            await Page.GetByText("New Zealand", new() { Exact = true }).Nth(1).ClickAsync();


            //Auckland
            await Expect(Page.GetByText("Auckland", new() { Exact = true })).ToBeVisibleAsync();
            //var aucklandContact = Page.Locator("#section-0");  //Locator needs to be expanded
            var aucklandContact = Page.GetByText("58 Gaunt Street, Auckland CBD");
            await Expect(aucklandContact).ToContainTextAsync("58 Gaunt Street, Auckland CBD, Auckland 1010");

            //Christchurch
            await Expect(Page.GetByText("Christchurch", new() { Exact = true })).ToBeVisibleAsync();
            //var christchurchContact = Page.Locator("p").Filter(new() { HasTextString = "67 Gloucester Street," }).Locator("span");
            var christchurchContact = Page.Locator("#section-1");  //Locator needs to be expanded
            await Expect(christchurchContact).ToContainTextAsync("67 Gloucester Street, Christchurch 8013");

            //Wellington
            await Expect(Page.GetByText("Wellington", new() { Exact = true })).ToBeVisibleAsync();
            var wellingtonContact = Page.Locator("#section-9");    //Locator needs to be expanded
            await Expect(wellingtonContact).ToContainTextAsync("Wellington");
            await Expect(wellingtonContact).ToContainTextAsync("55 Featherston Street, Pipitea, Wellington 6011,");
        }

        [Test]
        public async Task TestAustraliaRegionalContactDetails()
        {
            await Page.GetByText("Australia", new() { Exact = true }).Nth(1).ClickAsync();
            await Expect(Page.GetByText("Adelaide", new() { Exact = true })).ToBeVisibleAsync();

            var adelaideContact = Page.Locator("#section-0");  //Locator needs to be expanded
            await Expect(adelaideContact).ToContainTextAsync("Adelaide");
            await Expect(adelaideContact).ToContainTextAsync("118 Franklin Street, Adelaide, South Australia 5000");
        }

        [Test]
        public async Task TestContactUs()
        {
            await Page.Locator("#cmp-mrkto-modal-thank-you").GetByText("Contact us").ClickAsync();
            await Page.GetByLabel("*First name").ClickAsync();
            await Page.GetByLabel("*First name").FillAsync("Anton");
            await Page.GetByLabel("*Last name").ClickAsync();
            await Page.GetByLabel("*Last name").FillAsync("Ohlson");
            await Page.GetByLabel("*Business email").ClickAsync();
            await Page.GetByLabel("*Business email").FillAsync("anton.Ohlson@yahoo.com");
            await Page.GetByLabel("*Phone number").ClickAsync();
            await Page.GetByLabel("*Phone number").FillAsync("0210687777");
            await Page.GetByLabel("*Job title").ClickAsync();
            await Page.GetByLabel("*Job title").FillAsync("QA Analyst");
            await Page.GetByLabel("*Company name").ClickAsync();
            await Page.GetByLabel("*Company name").FillAsync("CountDown");
            await Page.GetByLabel("*Country").SelectOptionAsync(new[] { "New Zealand" });
            await Page.GetByLabel("*State").SelectOptionAsync(new[] { "Christchurch" });
            await Page.GetByLabel("*What type of career are you looking for?").SelectOptionAsync(new[] { "Careers" });
            await Page.GetByLabel("*What type of career are you looking for?").SelectOptionAsync(new[] { "Internship" });
            await Page.GetByLabel("*What role/business area are you interested in?").ClickAsync();
            await Page.GetByLabel("*What role/business area are you interested in?").FillAsync("QA Analyst");
            await Page.GetByRole(AriaRole.Button, new() { NameString = "Submit" }).ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Heading, new() { NameString = "Thank you for getting in" })).ToBeVisibleAsync();
            await Page.GetByRole(AriaRole.Link, new() { NameString = "Return to homepage" }).ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Link, new() { NameString = "Datacom logo" })).ToBeVisibleAsync();
        }

        [Test]
        public async Task TestSignOn()
        {
            await Page.GetByRole(AriaRole.Link, new() { Name = "Sign in" }).ClickAsync();
            await Page.Locator("li").Filter(new() { HasText = "DataPay Employee self-service" }).GetByRole(AriaRole.Link).Nth(1).ClickAsync();
            await Page.GetByPlaceholder("Username").ClickAsync();
            await Page.GetByPlaceholder("Username").FillAsync("Anton");
            await Page.GetByPlaceholder("Password").ClickAsync();
            await Page.GetByPlaceholder("Password").FillAsync("Password2");
            await Page.GetByRole(AriaRole.Button, new() { Name = "Log In" }).ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "OK" }).ClickAsync();
        }
    }
}

