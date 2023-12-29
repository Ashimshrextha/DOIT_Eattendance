using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemStores.GenericMapper
{
    public static   class  MapperExtensions
    {
        public static Task<TResult> MapAsync<TSource, TResult>(this IMapper mapper, Task<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var tcs = new TaskCompletionSource<TResult>();

            source
                .ContinueWith(t => tcs.TrySetCanceled(), TaskContinuationOptions.OnlyOnCanceled);

            source
                .ContinueWith
                (
                    t =>
                    {
                        tcs.TrySetResult(mapper.Map<TSource, TResult>(t.Result));
                    },
                    TaskContinuationOptions.OnlyOnRanToCompletion
                );

            source
                .ContinueWith
                (
                    t => tcs.TrySetException(t.Exception),
                    TaskContinuationOptions.OnlyOnFaulted
                );

            return tcs.Task;
        }
    }
}
