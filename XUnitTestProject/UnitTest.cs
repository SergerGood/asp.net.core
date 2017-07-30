using System.Threading.Tasks;
using Alba;
using Baseline;
using Xunit;
using ASP.NET.Core.Web.Api;
using ASP.NET.Core.Web.Api.Controllers;
using ASP.NET.Core.Web.Api.Models;

namespace XUnitTestProject
{
    public class UnitTest
    {
        [Fact]
        public async Task ShouldGetByController()
        {
            using (var system = SystemUnderTest.ForStartup<Startup>())
            {
                var response = await system.Scenario(scenario =>
                {
                    scenario.Get.Action<ValuesController>(x => x.Get());

                    scenario
                        .StatusCodeShouldBeOk()
                        .ContentShouldBe("Hello, World!");

                });
            }
        }

        [Fact]
        public async Task ShouldGetByUrl()
        {
            using (var system = SystemUnderTest.ForStartup<Startup>())
            {
                var response = await system.Scenario(scenario =>
                {
                    scenario.Get.Url("/api/values");

                    scenario
                        .StatusCodeShouldBeOk()
                        .ContentShouldBe("Hello, World!");

                });
            }
        }

        [Fact]
        public async Task ShouldGetByUrlWithParams()
        {
            using (var system = SystemUnderTest.ForStartup<Startup>())
            {
                var response = await system.Scenario(scenario =>
                {
                    scenario.Get.Url("/api/values")
                        .Accepts("text/plain")
                        .ContentType("text/json")
                        .Etag("12345");

                    scenario
                        .StatusCodeShouldBeOk()
                        .ContentShouldBe("Hello, World!");

                    scenario.Header("content-length").SingleValueShouldEqual("150");
                    scenario.Header("connection").ShouldNotBeWritten();
                    scenario.Header("set-cookie").ShouldHaveOneNonNullValue();
                    scenario.ContentTypeShouldBe("text/json");
                });
            }
        }

        [Fact]
        public async Task ShouldGetJson()
        {
            using (var system = SystemUnderTest.ForStartup<Startup>())
            {
                var responce = await system.Scenario(scenario =>
                {
                    scenario.Get.Url("/api/values/person");
                }
                );

                var person = responce.ResponseBody.ReadAsJson<Person>();

                Assert.Equal("Name", person.Name);
            }
        }
    }
}
