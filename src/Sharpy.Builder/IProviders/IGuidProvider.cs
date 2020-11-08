using System;

namespace Sharpy.Builder.IProviders
{
    /// <summary>
    ///  Methods providing System.Guid and formatted System.String.
    /// </summary>
    public interface IGuidProvider
    {
        /// <summary>
        /// Provides a System.Guid.
        /// </summary>
        Guid Guid();

        /// <summary>
        /// Provides a System.Guid in the in the format specified from <paramref name="format"/>. 
        /// </summary>
        string Guid(GuidFormat format);
    }

    /// <summary>
    ///     Format options for <see cref="System.Guid" />.
    /// </summary>
    public enum GuidFormat
    {
        /// <summary>
        ///     32 digits:
        /// </summary>
        /// <para>
        ///     Example: 00000000000000000000000000000000
        /// </para>
        DigitsOnly = 'N',

        /// <summary>
        ///     32 digits separated by hyphens:
        /// </summary>
        /// <para>
        ///     Example: 00000000-0000-0000-0000-000000000000
        /// </para>
        DigitsWithHyphens = 'D',

        /// <summary>
        ///     32 digits separated by hyphens, enclosed in braces:
        /// </summary>
        /// <para>
        ///     Example: {00000000-0000-0000-0000-000000000000}
        /// </para>
        DigitsWithHyphensWrappedInBrackets = 'B',

        /// <summary>
        ///     32 digits separated by hyphens, enclosed in parentheses:
        /// </summary>
        /// <para>
        ///     Example: (00000000-0000-0000-0000-000000000000)
        /// </para>
        DigitsWithHyphensWrappedInParentheses = 'P',

        /// <summary>
        ///     Four hexadecimal values enclosed in braces, where the fourth value is a subset of eight hexadecimal values that is
        ///     also enclosed in braces:
        /// </summary>
        /// <para>
        ///     Example: {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}
        /// </para>
        FourHexadecimalWrappedInBrackets = 'X'
    }
}