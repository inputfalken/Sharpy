namespace Sharpy.Enums {
    /// <summary>
    ///     Is used as argument when filtering names
    /// </summary>
    public enum StringType {
        /// <summary>
        ///    Will give you back a random female first name.
        /// </summary>
        FemaleFirstName,

        /// <summary>
        ///    Will give you back a random male first name.
        /// </summary>
        MaleFirstName,

        /// <summary>
        ///    Will give you back a random male last name.
        /// </summary>
        LastName,

        /// <summary>
        ///    Will give you back a random first name.
        /// </summary>
        MixedFirstName,

        /// <summary>
        ///    Will give you back a random username.
        /// </summary>
        UserName,

        /// <summary>
        ///    Will give you either a first name, last name, username.
        /// </summary>
        Random,

        /// <summary>
        ///    Will give you either a first name, last name.
        /// </summary>
        AnyName,
        /// <summary>
        ///    Will give a random phone number.
        /// </summary>
        Phonenumber
           
    }
}