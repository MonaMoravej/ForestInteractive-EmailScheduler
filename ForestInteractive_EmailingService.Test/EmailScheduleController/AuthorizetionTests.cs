using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestInteractive_EmailingService.Test.EmailScheduleController
{
    [TestClass]
   public  class AuthorizetionTests
    {
        [TestMethod]
        //negetive test
        public void IndexWhenUnAuthorized() { }

        [TestMethod]
        //posetiveTest
        public void IndexWhenAuthorized() { }


        [TestMethod]
        //negetive test
        public void CancelWhenUnAuthorized() { }

        [TestMethod]
        //posetiveTest
        public void CancelWhenAuthorized() { }


        [TestMethod]
        //negetive test
        public void CreateWhenUnAuthorized() { }

        [TestMethod]
        //posetiveTest
        public void CreateWhenAuthorized() { }


    }
}
