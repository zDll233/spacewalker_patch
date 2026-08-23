using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SpaceWalker.Helper;

public static class RxExtensions
{
	public static IObservable<TResult> SelectConcat<TSource, TResult>(this IObservable<TSource> source, Func<TSource, Task<TResult>> selector, IScheduler? scheduler = null)
	{
		Func<TSource, Task<TResult>> selector2 = selector;
		IScheduler scheduler2 = scheduler;
		return source.Select(delegate(TSource x)
		{
			IObservable<TResult> observable = Observable.FromAsync(() => selector2(x));
			return (scheduler2 != null) ? observable.SubscribeOn(scheduler2) : observable;
		}).Concat();
	}
}
