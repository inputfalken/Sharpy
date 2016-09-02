namespace DataGen.Types.String {
    public interface IStringFilter<out T> {
        T DoesNotStartWith(string arg);
        T DoesNotContain(string arg);
        T StartsWith(params string[] args);
        T Contains(params string[] args);
        T ByLength(int length);
    }
}