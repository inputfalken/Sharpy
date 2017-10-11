namespace Sharpy.Builder.Implementation.DataObjects {
    /// <summary>
    ///     Is used as argument when filtering names
    /// </summary>
    internal enum NameType {
        /// <summary>
        ///     Will give you back a random female first name.
        /// </summary>
        FemaleFirst = 1,

        /// <summary>
        ///     Will give you back a random male first name.
        /// </summary>
        MaleFirst = 2,

        /// <summary>
        ///     Will give you back a random last name.
        /// </summary>
        Last = 3
    }
}