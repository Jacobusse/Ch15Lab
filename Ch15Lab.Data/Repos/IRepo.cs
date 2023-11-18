namespace Ch15Lab.Data.Repos;

public interface IRepo<T> where T : class
{
    T? Set(T entity);
    T? Add(T entity);
    T? Get(int id);
    void Del(int id);
}
