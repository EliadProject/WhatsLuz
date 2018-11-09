using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WhatsLuzMVCAPI.Models
{
    public class ValidationModel
    {
        public const int MIN_ATTENDIES = 5;
        public const int MAX_ATTENDIES = 50;

        public const int MIN_DURATION = 30;
        public const int MAX_DURATION = 300;

        public const int MIN_LENGTH = 3;
        public const int MAX_LENGTH = 50;

        public static bool EmailValidation(string input)
        {
            string regExpression = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex regex = new Regex(regExpression);
            Match match = regex.Match(input);

            if (match.Success)
                return true;
            return false;
        }
        public static bool LengthAndNotSpecialValidation(string input)
        {
            string regExpression = @"^[\w\d\s]{" + MIN_LENGTH + "," + MAX_LENGTH + "}$";
            Regex regex = new Regex(regExpression);
            Match match = regex.Match(input);

            if (match.Success)
                return true;
            return false;
        }
        public static bool LengthAndNotSpecialValidationMaxOnly(string input)
        {
            string regExpression = @"^[\w\d\s]{0," + MAX_LENGTH + "}$";
            Regex regex = new Regex(regExpression);
            Match match = regex.Match(input);

            if (match.Success)
                return true;
            return false;
        }

        public static bool isInt(string input)
        {
            int value;
            if (int.TryParse(input, out value))
            {
                if (value == 0 || value == 1)
                    return true;
            }
            return false;
        }

        public static bool isDouble(string input)
        {
            double value;
            if (double.TryParse(input, out value))
                    return true;
            return false;
        }

        public static bool isDateTime(string input)
        {
            DateTime value;
            if (DateTime.TryParse(input, out value))
                return true;
            return false;
        }

        public static bool ValidDuration(string input)
        {
            int value;
            if (int.TryParse(input, out value))
            {
                if (value >= MIN_DURATION && value <= MAX_DURATION)
                    return true;
            }
            return false;
        }
      
        public static bool ValidAttendies(string input)
        {
            int value;
            if (int.TryParse(input, out value))
            {
                if (value >= MIN_ATTENDIES && value <= MAX_ATTENDIES)
                    return true;
            }
            return false;
        }
    }
}