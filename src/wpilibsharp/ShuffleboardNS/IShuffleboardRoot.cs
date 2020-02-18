namespace WPILib.ShuffleboardNS
{
    internal interface IShuffleboardRoot
    {
        ShuffleboardTab GetTab(string title);

        void Update();

        void EnableActuatorWidgets();
        void DisableActuatorWidgets();

        void SelectTab(int index);

        void SelectTab(string title);
    }
}
