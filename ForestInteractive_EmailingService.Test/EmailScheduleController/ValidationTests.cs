using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestInteractive_EmailingService.Test.EmailScheduleController
{
    [TestClass]
   public class ValidationTests
    {
        [TestMethod]
        public void CreateMustGetOneFile()
        {

        }

        public void CreateMustGetCSVFile()
        {

        }

        [TestMethod]
        public void CreateMustGetADateTimeOrChooseNow() { }

        [TestMethod]
        public void CreateMustGetDateTimeGreaterThanNow() { }

        [TestMethod]
        public void CreateMustGetASenderEmail() { }

        [TestMethod]
        public void CreateMustGetASubject() { }

        [TestMethod]
        public void CreateMustGetABody() { }

        [TestMethod]
        public void CreateMustGetSubjectWithoutSpecialChar() { }

        [TestMethod]
        public void CreateMustGetBodyWithoutSpecialChar() { }

        //negative test
        [TestMethod]
        public void IfItIsDuplicatedMustNotAdd() { }
    }
}
