using System;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    /// <inheritdoc />
    public sealed class GuidProvider : IGuidProvider
    {
        /// <inheritdoc cref="IGuidProvider.Guid()"/>
        public Guid Guid()
        {
            return System.Guid.NewGuid();
        }

        /// <inheritdoc cref="IGuidProvider.Guid(GuidFormat)"/>
        public string Guid(GuidFormat format)
        {
            return Guid().ToString(char.ToString((char) format));
        }
    }
}