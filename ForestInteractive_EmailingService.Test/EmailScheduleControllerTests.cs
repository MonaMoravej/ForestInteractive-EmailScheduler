using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestInteractive_EmailingService.Test
{
    [TestClass]
   public class EmailScheduleControllerTests
    {
        //posetive test
        [TestMethod]
        public void IndexReturnsListOfEmailSchedule()
        {
            //1.check shavad ke view ba model as IEnumerable view model ast

        }

        //positive test
        [TestMethod]
        public void IndexShowsMessage()
        {
            //1.check shavad ba message khali ya ba massage por ke dar har hal bargardanad
        }

        [TestMethod]
        public void CreatePassesUserEmail()
        {

        }

       //[TestMethod] it's done on view,can we test it here???
       //public void IndexMustShowsCancelBtnToJust

        [TestMethod]
        public void CreateMustAddARecordOnDatabaseWithfilledJobId() { }

        //nrgative test , age error bokhore chi mishe
        [TestMethod]
        public void CreateNotAddIfBackgroundJobNotAdded() { }

        //if everything ok , redirect , if not return CreateView ,maybe must be 2 test, negative and posetive
        [TestMethod]
        public void CreateRedirectToIndexCorrectly() { }

        [TestMethod]
        public void CancelDeleteRecordFromDatabase() { }

        [TestMethod]
        public void CancelDeleteJobCorrectly() { }

        [TestMethod]
        public void CancelMustDeleteJobAndRecodAttheSameTime() { }


        //must have a field for current status of each schedule
        //once it's created must be 'Create'
        //when Send must be 'Send'
        //if get error during sending like Internet was disconnect or have an error on services must be 'Error'
        //if satus is !send must be able to cancelled otherwise not to be able to cancel the record
        [TestMethod]
        public void 
    }
}
