namespace HAL_Base
{
    public interface ISimulator
    {
        string Name { get; }
        void Initialize();
        void Start();
    }
}
