using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForestInteractive_EmailingService.Models.CustomValidations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MustPassDateOrNow: ValidationAttribute
    {
        private const string DefaultErrorMessage = "Choose sending now or fill send date and time.";

        public MustPassDateOrNow()
                : base(DefaultErrorMessage)
            {
            
        }

        public override string FormatErrorMessage(string name)
        {
            return DefaultErrorMessage;
        }


        public override bool IsValid(object value)
        {
           
            if (value != null)
            {

                EmailScheduleViewModel model = (EmailScheduleViewModel)value;
                if (!model.SendingNow && (!model.SendDate.HasValue || !model.SendTime.HasValue)) return false;

                //if (model.SendingNow && (model.SendDate.HasValue || model.SendTime.HasValue)) return false;

               
            }
            return true;
        }
    }
}