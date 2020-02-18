using NetworkTables;

namespace WPILib.ShuffleboardNS
{
    internal sealed class RecordingController
    {
        private const string RecordingTableName = "/Shuffleboard/.recording/";
        private const string RecordingControlKey = RecordingTableName + "RecordData";
        private const string RecordingFileNameFormatKey = RecordingTableName + "FileNameFormat";
        private const string EventMarkerTableName = RecordingTableName + "events";

        private readonly NetworkTableEntry m_recordingControlEntry;
        private readonly NetworkTableEntry m_recordingFileNameFormatEntry;
        private readonly NetworkTable m_eventsTable;

        internal RecordingController(NetworkTableInstance ntInstance)
        {
            m_recordingControlEntry = ntInstance.GetEntry(RecordingControlKey);
            m_recordingFileNameFormatEntry = ntInstance.GetEntry(RecordingFileNameFormatKey);
            m_eventsTable = ntInstance.GetTable(EventMarkerTableName);
        }

        public void StartRecording()
        {
            m_recordingControlEntry.SetBoolean(true);
        }

        public void StopRecording()
        {
            m_recordingControlEntry.SetBoolean(false);
        }

        public void SetRecordingFileNameFormat(string format)
        {
            m_recordingFileNameFormatEntry.SetString(format);
        }

        public void ClearRecordingFileNameFormat()
        {
            m_recordingFileNameFormatEntry.Delete();
        }

        public void AddEventMarker(string name, string description, EventImportance importance)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                DriverStation.ReportError("Shuffleboard event name was not specified", true);
                return;
            }

            m_eventsTable.GetSubTable(name).GetEntry("Info").SetStringArray(new string[] { description, importance.GetName() });
        }
    }
}
