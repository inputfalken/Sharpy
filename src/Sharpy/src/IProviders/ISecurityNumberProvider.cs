namespace Sharpy.IProviders {
    public interface ISecurityNumberProvider {
        long SecurityNumber(string dateNumber);
    }
}