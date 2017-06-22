using System;
using System.Threading.Tasks;
using Alba;
using ASP.NET.Sample.Web;
using ASP.NET.Sample.Web.Controllers;
using Baseline;
using Xunit;

namespace XUnitTestProject
{
    public class UnitTest
    {
        [Fact]
        public async Task should_say_hello_world()
        {
            using (var system = SystemUnderTest.ForStartup<Startup>())
            {

                //return system.Scenario(scenario =>
                //{
                //    scenario.Get.Url("/GetHtml");
                //    scenario.ContentShouldBe("<h2>Привет ASP.NET Core</h2>");
                //    scenario.StatusCodeShouldBeOk();
                //});

                var response = await system.Scenario(_ =>
                {
                    _.Get.Url("/Home/Buy/1");
                });

                var asd = response.ResponseBody.ReadAsText();//.ShouldBe("Hello, World!");

                // or you can go straight at the HttpContext
                // The ReadAllText() extension method is from Baseline


                var body = response.Context.Response.Body;
                body.Position = 0; // need to rewind it because we read it above
                var ss = body.ReadAllText();//.ShouldBe("Hello, World!");
            }
        }
    }
}
