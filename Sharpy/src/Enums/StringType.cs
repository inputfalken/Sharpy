namespace Sharpy.Enums {
    /// <summary>
    ///     Is used as argument when filtering names
    /// </summary>
    public enum StringType {

        /// <summary>
        ///     Will give you back a random female first name.
        /// </summary>
        FemaleFirstName,

        /// <summary>
        ///     Will give you back a random male first name.
        /// </summary>
        MaleFirstName,

        /// <summary>
        ///     Will give you back a random last name.
        /// </summary>
        LastName,

        /// <summary>
        ///     Will give you back a random first name.
        /// </summary>
        FirstName,

        /// <summary>
        ///     Will give you back a random username.
        /// </summary>
        UserName,

        /// <summary>
        ///     Will give you either a first name, last name, username.
        /// </summary>
        AnyName,

        /// <summary>
        ///     Will give a random string number. Based on the configuration of numbergenerator.
        ///     Could be used as a phone number generator for example.
        /// </summary>
        Number
    }
}