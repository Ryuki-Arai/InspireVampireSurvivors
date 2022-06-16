public interface IObserver<T>
{
    void NotifyUpdate(T obj);
}
