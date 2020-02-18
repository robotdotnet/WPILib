using NetworkTables;

namespace WPILib.ShuffleboardNS
{
    public static class Shuffleboard
    {
        public static readonly string kBaseTableName = "/Shuffleboard";

        private static readonly IShuffleboardRoot root = new ShuffleboardInstance(NetworkTableInstance.Default);
        private static readonly RecordingController recordingController = new RecordingController(NetworkTableInstance.Default);

        public static void Update()
        {
            root.Update();
        }

        public static ShuffleboardTab GetTab(string title)
        {
            return root.GetTab(title);
        }

        public static void SelectTab(int index)
        {
            root.SelectTab(index);
        }

        public static void SelectTab(string title)
        {
            root.SelectTab(title);
        }

        public static void EnableActuatorWidgets()
        {
            root.EnableActuatorWidgets();
        }

        public static void DisableActuatorWidgets()
        {
            Update();
            root.DisableActuatorWidgets();
        }

        public static void StartRecording()
        {
            recordingController.StartRecording();
        }

        public static void StopRecording()
        {
            recordingController.StopRecording();
        }

        public static void SetRecordingFileNameFormat(string format)
        {
            recordingController.SetRecordingFileNameFormat(format);
        }

        public static void ClearRecordingFileNameFormat()
        {
            recordingController.ClearRecordingFileNameFormat();
        }

        public static void AddEventMarker(string name, string description, EventImportance importance)
        {
            recordingController.AddEventMarker(name, description, importance);
        }

        public static void AddEventMarker(string name, EventImportance importance)
        {
            AddEventMarker(name, "", importance);
        }
    }
}
