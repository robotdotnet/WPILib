using NetworkTables;

namespace WPILib.ShuffleboardNS
{
    internal sealed class RecordingController
    {
        private static readonly string kRecordingTableName = "/Shuffleboard/.recording/";
        private static readonly string kRecordingControlKey = kRecordingTableName + "RecordData";
        private static readonly string kRecordingFileNameFormatKey = kRecordingTableName + "FileNameFormat";
        private static readonly string kEventMarkerTableName = kRecordingTableName + "events";

        private readonly NetworkTableEntry m_recordingControlEntry;
        private readonly NetworkTableEntry m_recordingFileNameFormatEntry;
        private readonly NetworkTable m_eventsTable;

        internal RecordingController(NetworkTableInstance ntInstance)
        {
            m_recordingControlEntry = ntInstance.GetEntry(kRecordingControlKey);
            m_recordingFileNameFormatEntry = ntInstance.GetEntry(kRecordingFileNameFormatKey);
            m_eventsTable = ntInstance.GetTable(kEventMarkerTableName);
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
