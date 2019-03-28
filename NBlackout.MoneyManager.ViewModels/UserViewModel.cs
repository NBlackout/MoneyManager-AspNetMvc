using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using NBlackout.MoneyManager.Resources;

namespace NBlackout.MoneyManager.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "Labels_UserName", ResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Display(Name = "Labels_FirstName", ResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [Display(Name = "Labels_LastName", ResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [Display(Name = "Labels_Password", ResourceType = typeof(Resource))]
        public string Password { get; set; }

        [Display(Name = "Labels_Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Display(Name = "Labels_PhoneNumber", ResourceType = typeof(Resource))]
        public string PhoneNumber { get; set; }

        #region Read-only

        [Display(Name = "Labels_FullName", ResourceType = typeof(Resource))]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string Gravatar
        {
            get
            {
                if (String.IsNullOrEmpty(Email))
                    return String.Empty;

                var hash = HashWithMD5(Email.ToLower(CultureInfo.InvariantCulture));

                return $"https://secure.gravatar.com/avatar/{hash.ToLower(CultureInfo.InvariantCulture)}?s=32";
            }
        }

        #endregion

        private string HashWithMD5(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var builder = new StringBuilder();

            using (var provider = new MD5CryptoServiceProvider())
            {
                var buffer = Encoding.ASCII.GetBytes(value);
                var hash = provider.ComputeHash(buffer);

                foreach (var b in hash)
                    builder.Append(b.ToString("X2", CultureInfo.InvariantCulture));

                return builder.ToString();
            }
        }
    }
}