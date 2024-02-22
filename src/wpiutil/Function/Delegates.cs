namespace WPIUtil.Function;

public delegate void BiConsumer<T, U>(T t, U u);

public delegate R BiFunction<T, U, R>(T t, U u);

public delegate bool BiPredicate<T, U>(T t, U u);

public delegate T Supplier<T>();

public delegate void Consumer<T>(T t);

public delegate R Function<T, R>(T t);

public delegate bool Predicate<T>(T t);

public delegate void Runnable();
