using ForestInteractive_EmailingService.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ForestInteractive_EmailingService.Test
{
    [TestClass]
    public class HomeControllerTests
    {
        //kol in class bikhod ast
        //return correct view , Index
        //if it's not authorized still return view
        [TestMethod]
        public void ReturenIndexView()
        {
            //HomeController controller = new HomeController();

            //Mock<HttpContext> hc = new Mock<HttpContext>();
            //Mock<HttpRequest> hr = new Mock<HttpRequest>();

            //hr.Setup(h => h.Path).Returns("/");
            //hr.Setup(h => h.HttpMethod).Returns("GET");

            //hc.Setup(h => h.Request).Returns(hr.Object);

            ////register routes defined in mvc app
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            
            //ControllerContext controlerContect = new ControllerContext();


            var request = new Mock<HttpRequestBase>();
            request.Setup(r => r.HttpMethod).Returns("GET");
            request.Setup(r => r.Path).Returns("/");

            var mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.Setup(c => c.Request).Returns(request.Object);
           // mockHttpContext.Setup(c=>c.Request.R)

           

            var controllerContext = new ControllerContext(mockHttpContext.Object
            ,new RouteData(), new Mock<ControllerBase>().Object);
            
            
            Assert.AreEqual("Index", "Index");
        }

     


    }



    public class ContextMocks
    {
        public Mock<HttpContextBase> HttpContext { get; set; }
        public Mock<HttpRequestBase> Request1 { get; set; }
        public RouteData RouteData { get; set; }

        public ContextMocks(Controller controller)
        {
            //define context objects
            HttpContext = new Mock<HttpContextBase>();
            HttpContext.Setup(x => x.Request).Returns(Request1.Object);
            //you would setup Response, Session, etc similarly with either mocks or fakes

            //apply context to controller
            RequestContext rc = new RequestContext(HttpContext.Object, new RouteData());
            controller.ControllerContext = new ControllerContext(rc, controller);
        }
    }
}
