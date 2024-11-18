using System.Globalization;
using System.Text;
using NetworkTables;
using NetworkTables.Handles;
using UnitsNet.NumberExtensions.NumberToDuration;
using WPIUtil.Concurrent;
using WPIUtil.Logging;

namespace WPILib;

public static class DataLogManger
{
    private static DataLogBackgroundWriter? m_log;
    private static bool m_stopped;
    private static string? m_logDir;
    private static bool m_filenameOverride;
    private static Thread? m_thread;
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0052 // Remove unread private members
    private static TimeZoneInfo m_utc = TimeZoneInfo.Utc;
#pragma warning restore IDE0052 // Remove unread private members
#pragma warning restore IDE0044 // Add readonly modifier
    private static bool m_ntLoggerEnabled = true;
    private static NtDataLogger m_ntEntryLogger;
    private static NtConnectionDataLogger m_ntConnLogger;
    private static StringLogEntry? m_messageLog;
    private static readonly object m_lockObject = new();

    // if less than this much free space, delete log files until there is this much free space
    // OR there are this many files remaining.
    private const long FreeSpaceThreshold = 50000000L;
    private const int FileCountThreshold = 10;

    public static void Start(string dir = "", string filename = "", double period = 0.25)
    {
        lock (m_lockObject)
        {
            if (m_log is null)
            {
                m_logDir = MakeLogDir(dir);
                m_filenameOverride = !string.IsNullOrWhiteSpace(filename);

                // Delete all previously existing FRC_TBD_*.wpilog files. These only exist when the robot
                // never connects to the DS, so they are very unlikely to have useful data and just clutter
                // the filesystem.
                var files = Directory.GetFiles(m_logDir).Where(name => Path.GetFileName(name).StartsWith("FRC_TBD_", StringComparison.InvariantCulture) && name.EndsWith(".wpilog", StringComparison.InvariantCulture));
                foreach (var file in files)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (IOException)
                    {
                        Console.Error.WriteLine($"DataLogManager: could not delete {file}");
                    }
                }
                m_log = new(m_logDir, MakeLogFilename(filename), period);
                m_messageLog = new StringLogEntry(m_log, "messages");

                if (m_ntLoggerEnabled)
                {
                    StartNtLog();
                }
            }
            else if (m_stopped)
            {
                m_log.Filename = MakeLogFilename(filename);
                m_log.Resume();
                m_stopped = false;
            }

            if (m_thread is null)
            {
                m_thread = new Thread(LogMain)
                {
                    Name = "DataLogDS",
                    IsBackground = true
                };
                m_thread.Start();
            }
        }
    }

    public static void Stop()
    {
        lock (m_lockObject)
        {
            m_thread = null;
            if (m_log is not null)
            {
                m_log.Stop();
                m_stopped = true;
            }
        }
    }

    public static void Log(string message)
    {
        lock (m_lockObject)
        {
            if (m_messageLog is null)
            {
                Start();
            }
            m_messageLog!.Append(message);
            Console.WriteLine(message);
        }
    }

    public static DataLog DataLog
    {
        get
        {
            lock (m_lockObject)
            {
                if (m_log is null)
                {
                    Start();
                }
                return m_log!;
            }
        }
    }

    public static string LogDir
    {
        get
        {
            lock (m_lockObject)
            {
                return m_logDir ?? "";
            }
        }
    }

    public static void LogNetworkTables(bool enabled)
    {
        lock (m_lockObject)
        {
            bool wasEnabled = m_ntLoggerEnabled;
            m_ntLoggerEnabled = enabled;
            if (m_log is null)
            {
                Start();
                return;
            }
            if (enabled && !wasEnabled)
            {
                StartNtLog();
            }
            else if (!enabled && wasEnabled)
            {
                StopNtLog();
            }
        }
    }

    private static string MakeLogDir(string dir)
    {
        if (!string.IsNullOrWhiteSpace(dir))
        {
            return dir;
        }

        if (RobotBase.IsReal)
        {
            try
            {
                Directory.CreateDirectory("/u/logs");
                return "/u/logs";
            }
            catch (IOException)
            {

            }
            if (RobotBase.RuntimeType == RuntimeType.RoboRIO)
            {
                DriverStation.ReportWarning("DataLogManager: Logging to RoboRIO 1 internal storage is not recommended!, Plug in a FAT32 formatted flash drive!", false);
            }
            try
            {
                Directory.CreateDirectory("/home/lvuser/logs");
            }
            catch (IOException)
            {

            }
            return "/home/lvuser/logs";
        }
        string logDir = Filesystem.OperatingDirectory;
        try
        {
            Directory.CreateDirectory(logDir);
        }
        catch (IOException)
        {

        }
        return logDir;
    }

    private static string MakeLogFilename(string filenameOverride)
    {
        if (!string.IsNullOrWhiteSpace(filenameOverride))
        {
            return filenameOverride;
        }

        Random rnd = new Random();
        StringBuilder filename = new();
        filename.Append("FRC_TBD_");
        for (int i = 0; i < 4; i++)
        {
            filename.Append(rnd.Next(0x10000).ToString("x4", CultureInfo.InvariantCulture));
        }
        filename.Append(".wpilog");
        return filename.ToString();
    }

    private static void StartNtLog()
    {
        var inst = NetworkTableInstance.Default;
        m_ntEntryLogger = inst.StartEntryDataLog(m_log!, "", "NT:");
        m_ntConnLogger = inst.StartConnectionDataLog(m_log!, "NTConnection");
    }

    private static void StopNtLog()
    {
        NetworkTableInstance.StopEntryDataLog(m_ntEntryLogger);
        NetworkTableInstance.StopConnectionDataLog(m_ntConnLogger);
    }

    private static void LogMain()
    {
        // based on free disk space, scan for "old" FRC_*.wpilog files and remove
        {
            string logDir = m_logDir!;
            var fileInfo = new DirectoryInfo(logDir);
            var driveInfo = new DriveInfo(fileInfo.FullName);
            var freeSpace = driveInfo.AvailableFreeSpace;
            if (freeSpace < FreeSpaceThreshold)
            {
                var files = Directory.GetFiles(logDir).Where(name =>
                {
                    name = Path.GetFileName(name);
                    return name.StartsWith("FRC_", StringComparison.InvariantCultureIgnoreCase)
                        && name.EndsWith(".wpilog", StringComparison.InvariantCultureIgnoreCase)
                        && !name.StartsWith("FRC_TBD_", StringComparison.InvariantCultureIgnoreCase);
                }).OrderBy(File.GetLastWriteTimeUtc);
                int count = 0;
                foreach (var file in files)
                {
                    --count;
                    if (count < FileCountThreshold)
                    {
                        break;
                    }
                    try
                    {
                        long length = new FileInfo(file).Length;
                        DriverStation.ReportWarning($"DataLogManager: Deleted {Path.GetFileName(file)}", false);
                        freeSpace += length;
                        if (freeSpace >= FreeSpaceThreshold)
                        {
                            break;
                        }
                    }
                    catch (IOException)
                    {
                        Console.Error.WriteLine($"DataLogManager: could not delete {file}");
                    }
                }
            }
            else if (freeSpace < 2 * FreeSpaceThreshold)
            {
                DriverStation.ReportWarning(
                    "DataLogManager: Log storage device has "
                        + freeSpace / 1000000
                        + " MB of free space remaining! Logs will get deleted below "
                        + FreeSpaceThreshold / 1000000
                        + " MB of free space."
                        + "Consider deleting logs off the storage device.",
                    false);
            }
        }

        int timeoutCount = 0;
        bool paused = false;
        int dsAttachCount = 0;
        int fmsAttachCount = 0;
        bool dsRenamed = m_filenameOverride;
        bool fmsRenamed = m_filenameOverride;
        int sysTimeCount = 0;
        IntegerLogEntry sysSimteEntry = new IntegerLogEntry(m_log!, "systemTime", "{\"source\":\"DataLogManager\",\"format\":\"time_t_us\"}");
        using WpiEvent newDataEvent = new();
        DriverStation.ProvideRefreshedDataEventHandle(newDataEvent.Handle);
        while (Interlocked.CompareExchange(ref m_thread, null, null) != null)
        {
            var result = Synchronization.WaitForObject(newDataEvent.Handle.Handle, 0.25.Seconds());
            if (result == SynchronizationResult.Cancelled || Interlocked.CompareExchange(ref m_thread, null, null) != null)
            {
                break;
            }
            if (result == SynchronizationResult.TimedOut)
            {
                timeoutCount++;
                // pause logging after being disconnected for 10 seconds
                if (timeoutCount > 40 && !paused)
                {
                    timeoutCount = 0;
                    paused = true;
                    m_log!.Pause();
                }
                continue;
            }
            // when we connect to the DS, resume logging
            timeoutCount = 0;
            if (paused)
            {
                paused = false;
                m_log!.Resume();
            }

            if (!dsRenamed)
            {
                // track ds attach
                if (DriverStation.IsDSAttached)
                {
                    dsAttachCount++;

                }
                else
                {
                    dsAttachCount = 0;
                }
                if (dsAttachCount > 50) // 1 second
                {
                    if (RobotController.IsSystemTimeValid)
                    {
                        var now = DateTime.UtcNow;
                        // TODO make this time match
                        m_log!.Filename = $"FRC_{now}.wpilog";
                        dsRenamed = true;
                    }
                    else
                    {
                        dsAttachCount = 0; // wait a bit and try again
                    }
                }
            }

            if (!fmsRenamed)
            {
                // track FMS attach
                if (DriverStation.IsFMSAttached)
                {
                    fmsAttachCount++;
                }
                else
                {
                    fmsAttachCount = 0;
                }
                if (fmsAttachCount > 250) // 5 seconds
                {
                }
            }

            // Write system time every ~5 seconds
            sysTimeCount++;
            if (sysTimeCount > 250)
            {
                sysTimeCount = 0;
                if (RobotController.IsSystemTimeValid)
                {
                    // Write time
                }
            }
        }

    }
}
