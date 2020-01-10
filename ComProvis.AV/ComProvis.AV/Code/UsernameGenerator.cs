namespace ComProvis.AV.Code
{
    public class UsernameGenerator
    {
        /// <summary>
        /// Username se generira po sljedećoj logici
        /// ime korisnika - 8 (+ 2, index) znaka
        /// ime tvrtke - dobiva se kao parametar izvana.
        ///              O imenu tvrtke brine Donat i ne treba ga parsirati
        /// </summary>
        #region Enum
        public enum UserType
        {
            SsoAdmin = 1,
            User
        }
        #endregion

        #region Variables
        private int _UsernameMaxChars = 8;
        #endregion

        #region Fields
        private UserType Type { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string CustomerName { get; set; }

        public string Username { get; set; }

        public int UsernameMaxChars
        {
            get { return _UsernameMaxChars; }
            set { _UsernameMaxChars = value; }
        }
        #endregion

        #region Constructor
        public UsernameGenerator(string firstName, string lastName, string customerName, UserType type)
        {
            Type = type;
            FirstName = firstName;
            LastName = lastName;
            CustomerName = customerName;

            Username = Generate();
        }
        #endregion

        #region GeneratorMethods
        public string Generate()
        {
            var firstPartUsername = string.Empty;
            var username = string.Empty;
            switch (Type)
            {
                case UserType.SsoAdmin:
                    firstPartUsername = "admin";
                    username = string.Format("{0}_{1}", firstPartUsername, CustomerName);
                    break;
                case UserType.User:
                default:
                    firstPartUsername = GetFirstPartUsername();
                    if (string.IsNullOrEmpty(firstPartUsername)) return string.Empty;

                    firstPartUsername = firstPartUsername.Replace(" ", "");
                    var endPosition = firstPartUsername.Length <= UsernameMaxChars ? firstPartUsername.Length : UsernameMaxChars;
                    firstPartUsername = firstPartUsername.Substring(0, endPosition);

                    username = string.Format("{0}_{1}", firstPartUsername, StringHelper.CleanHRChars(CustomerName).ToLower().Trim());
                    break;
            }

            return username.ToLower();
        }

        private string GetFirstPartUsername()
        {
            var firstNameAscii = StringHelper.CleanHRChars(FirstName).ToLower().Trim();
            firstNameAscii = StringHelper.CleanString(firstNameAscii);
            var lastNameAscii = StringHelper.CleanHRChars(LastName).ToLower().Trim();
            lastNameAscii = StringHelper.CleanString(lastNameAscii);
            if (firstNameAscii.Length > 0 && lastNameAscii.Length > 0)
            {
                return firstNameAscii.Substring(0, 1) + lastNameAscii;
            }
            else return null;
        }
        #endregion
    }
}
