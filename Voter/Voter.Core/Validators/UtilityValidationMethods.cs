using System;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using Voter.Dal.Models;

namespace Voter.Core.Validators
{
    public class UtilityValidationMethods
    {
        public static bool IsGuid(string str)
        {
            return str != null && Guid.TryParse(str, out Guid guid);
        }

        public static bool IsValidName(string str)
        {
            var r = new Regex("^[a-zA-Z0-9 .,/]*$", RegexOptions.Compiled);
            return !string.IsNullOrEmpty(str) && str.Length < 128 && r.IsMatch(str);
        }

        public static bool IsValidUserName(string str)
        {
            var r = new Regex("^[a-zA-Z\\- ]*$", RegexOptions.Compiled);
            return !string.IsNullOrEmpty(str) && str.Length < 64 && r.IsMatch(str);
        }

        public static bool IsValidCountry(string str)
        {
            var r = new Regex("^[a-zA-Z\\-]*$", RegexOptions.Compiled);
            return !string.IsNullOrEmpty(str) && str.Length < 32 && r.IsMatch(str);
        }

        public static bool IsValidSex(string str)
        {
            return str.Equals("male", StringComparison.InvariantCultureIgnoreCase)
                || str.Equals("female", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsValidMail(string str)
        {
            try
            {
                var mail = new MailAddress(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidDescription(string str)
        {
            var r = new Regex("^[a-zA-Z0-9 .,/]*$", RegexOptions.Compiled);
            return !string.IsNullOrEmpty(str) && str.Length < 512 && r.IsMatch(str);
        }

        public static bool IsValidVariantText(string str)
        {
            var r = new Regex("^[a-zA-Z0-9 .,/]*$", RegexOptions.Compiled);
            return !string.IsNullOrEmpty(str) && str.Length < 256 && r.IsMatch(str);
        }

        public static bool IsValidQuestionText(string str)
        {
            var r = new Regex("^[a-zA-Z0-9 .,/]*$", RegexOptions.Compiled);
            return !string.IsNullOrEmpty(str) && str.Length < 512 && r.IsMatch(str);
        }


        public static bool IsValidQuestionType(string str)
        {
            return str.Equals(QuestionType.Checkbox.ToString(), StringComparison.InvariantCultureIgnoreCase)
                || str.Equals(QuestionType.Radiobox.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }
        public static bool IsBasicAuthHeader(string str)
        {
            if (!str.StartsWith("Basic"))
            {
                return false;
            }

            var encodedUsernamePassword = str.Substring("Basic ".Length).Trim();
            var credentials = Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(encodedUsernamePassword));
            return credentials != null
                && credentials.Split(':').Length == 2;
        }
    }
}
