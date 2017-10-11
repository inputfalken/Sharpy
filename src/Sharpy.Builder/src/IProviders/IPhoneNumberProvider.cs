namespace Sharpy.Builder.IProviders {
    public interface IPhoneNumberProvider {
        string PhoneNumber();
        string PhoneNumber(int length);
    }
}