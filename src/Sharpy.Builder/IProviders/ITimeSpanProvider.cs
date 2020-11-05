using System;

namespace Sharpy.Builder.IProviders
{
    public interface ITimeSpanProvider
    {
        TimeSpan TimeSpan();
        TimeSpan TimeSpan(TimeSpan max);
        TimeSpan TimeSpan(TimeSpan min ,TimeSpan max);
    }
}