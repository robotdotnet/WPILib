namespace Epilogue.Logging;

public interface DataLogger {
    public static DataLogger Multi(params DataLogger[] loggers) {
        return new MultiLogger(loggers);
    }

    public DataLogger Lazy => new LazyLogger(this);
}