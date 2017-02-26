using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForestInteractive_EmailingService.Models.CustomValidations
{
    
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public sealed class CompareSendDateTimeWithNowValidationAttribute : ValidationAttribute , IClientValidatable
    {

        private const string DefaultErrorMessage = "The {0} must be at least {1} minutes {2} now.";

        public int CompareMinutes { get; private set; }

        public DateTimeCompareTo CompareState { get; private set; }
        public CompareSendDateTimeWithNowValidationAttribute(int compareMinutes, DateTimeCompareTo compareState)

        {
            CompareState = compareState;
            CompareMinutes = compareMinutes;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(DefaultErrorMessage, name, CompareMinutes.ToString(), CompareState.ToString());
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool flag = true;
            if (value != null && validationContext!=null && validationContext.ObjectInstance!=null)
            {
                DateTime? time = (DateTime?)value;
                var date = ((EmailScheduleViewModel)validationContext.ObjectInstance).SendDate;
                if (date != null && time != null)
                {
                    
                    DateTime sendDateTime = date.Value + time.Value.TimeOfDay;
                    switch (CompareState)
                    {
                        case DateTimeCompareTo.Before:
                            if (sendDateTime.AddMinutes(CompareMinutes) > DateTime.Now) flag = false;
                            break;

                        case DateTimeCompareTo.After:
                        default:
                            if (sendDateTime < DateTime.Now.AddMinutes(CompareMinutes)) flag = false;
                            break;

                    }

                }
                if (!flag)
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new CompareSendDateTimeWithNowValidationRule(CompareMinutes,CompareState, FormatErrorMessage(metadata.DisplayName));
            yield return rule;
        }
    }

    public sealed class CompareSendDateTimeWithNowValidationRule : ModelClientValidationRule
    {

        public CompareSendDateTimeWithNowValidationRule(int compareMinutes,DateTimeCompareTo compareState, string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationParameters.Add("compareminutes", compareMinutes);
            ValidationParameters.Add("comparestate",compareState.ToString());
            ValidationType = "comparesenddatetimewithnow";
        }
    }

    public enum DateTimeCompareTo
    {
        After,
        Before
    }
}