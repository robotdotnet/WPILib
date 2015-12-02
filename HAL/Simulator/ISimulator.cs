namespace HAL
{
    public interface ISimulator
    {
        string Name { get; }
        void Initialize();
        void Start();
    }
}
