namespace Sharpy.IProviders {
    public interface IPhoneNumberProvider {
        string PhoneNumber();
        string PhoneNumber(int length);
    }
}