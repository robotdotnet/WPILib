namespace HAL.Simulator
{
    /// <summary>
    /// Interface for simulators
    /// </summary>
    /// <remarks>
    /// At program start, the simulator base looks for any DLL's in the same folder as the library
    /// for classes that inherit from this. It then provides a list for the end user to select their
    /// specific simulator. If only 1 simulator is found, it automatically starts it.
    /// </remarks>
    public interface ISimulator
    {
        /// <summary>
        /// The listing name for the simulator.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Called right before <see cref="Start"/>. Runs in the main thread, so must return.
        /// </summary>
        void Initialize();
        /// <summary>
        /// Main simulator entry method. Called in a seperate thread, so does not need to return.
        /// </summary>
        void Start();
    }
}
