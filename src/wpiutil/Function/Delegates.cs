namespace WPIUtil.Function;

public delegate void BiConsumer<T1, T2>(T1 t1, T2 t2);

public delegate TReturn BiFunction<T1, T2, TReturn>(T1 t1, T2 t2);

public delegate bool BiPredicate<T1, T2>(T1 t1, T2 t2);

public delegate T Supplier<T>();

public delegate void Consumer<T>(T t);

public delegate TReturn Function<T, TReturn>(T t);

public delegate bool Predicate<T>(T t);

public delegate void Runnable();
