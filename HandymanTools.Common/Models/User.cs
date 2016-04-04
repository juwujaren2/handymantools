using HandymanTools.Common.Enums;

namespace HandymanTools.Common.Models
{
    public class User : UserIdentity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public string Password { get; set; }

        public string PasswordHash { get; set; }
    }
}