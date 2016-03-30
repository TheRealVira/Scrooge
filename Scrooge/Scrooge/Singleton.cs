namespace Scrooge
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static T instance;

        public static T Instance => Singleton<T>.instance ?? (Singleton<T>.instance = new T());
    }
}