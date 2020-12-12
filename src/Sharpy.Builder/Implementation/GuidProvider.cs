using System;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    /// <inheritdoc />
    public sealed class GuidProvider : IGuidProvider
    {
        /// <inheritdoc/>
        public Guid Guid()
        {
            return System.Guid.NewGuid();
        }

        /// <inheritdoc/>
        public string Guid(in GuidFormat format)
        {
            return Guid().ToString(char.ToString((char) format));
        }
    }
}