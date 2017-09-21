using System;

namespace Sharpy.IProviders {
    public interface ISecurityNumberProvider {
        string SecurityNumber(DateTime date);
        string SecurityNumber();
    }
}