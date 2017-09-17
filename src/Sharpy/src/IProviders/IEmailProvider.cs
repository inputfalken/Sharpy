namespace Sharpy.IProviders {
    public interface IEmailProvider {
        string Mail(params string[] name);
    }
}