namespace NetworkTables
{
    public delegate void RpcAnswerDelegate(in RpcAnswer answer);
    public delegate void EntryNotificationDelegate(in RefEntryNotification notification);
    public delegate void ConnectionNotificationDelegate(in ConnectionNotification notification);
    public delegate void LogMessageDelegate(in LogMessage log);
}
