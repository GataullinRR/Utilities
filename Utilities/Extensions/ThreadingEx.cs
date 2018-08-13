using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace Utilities.Extensions
{
    public static class ThreadingEx
    {
        public static void Send(this SynchronizationContext synchronizationContext,  Action action)
        {
            synchronizationContext.Send(o => action(), null);
        }

        //public static Task NoThrowWhenCanceled(this Task continuation)
        //{
        //    return continuation.ContinueWith(
        //        ant => { var ex = ant.Exception; }, TaskContinuationOptions.OnlyOnCanceled);
        //}
        //public static Task<T> NoThrowWhenCanceled<T>(this Task<T> continuation)
        //{
        //    return continuation.ContinueWith(
        //        ant => 
        //        {
        //            var ex = ant.Exception;
        //            return default(T);
        //        }, TaskContinuationOptions.OnlyOnCanceled);
        //}

        /// <summary>
        /// Ловит <see cref="OperationCanceledException"/> и <see cref="AggregateException"/> при условии, что
        /// все его <see cref="AggregateException.InnerExceptions"/> наследуются от 
        /// <see cref="OperationCanceledException"/>
        /// </summary>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public static async Task CatchOperationCanceledExeption(this Task continuation)
        {
            try
            {
                await continuation;
            }
            catch (OperationCanceledException)
            {

            }
            catch (AggregateException ex)
            {
                if (!ex.InnerExceptions.All(e => typeof(OperationCanceledException).IsAssignableFrom(e.GetType())))
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Ловит <see cref="OperationCanceledException"/> и <see cref="AggregateException"/> при условии, что
        /// все его <see cref="AggregateException.InnerExceptions"/> наследуются от 
        /// <see cref="OperationCanceledException"/>
        /// </summary>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public static async Task<T> CatchOperationCanceledExeption<T>(this Task<T> continuation)
        {
            try
            {
                return await continuation;
            }
            catch (OperationCanceledException)
            {
                
            }
            catch (AggregateException ex)
            {
                if (!ex.InnerExceptions.All(e => typeof(OperationCanceledException).IsAssignableFrom(e.GetType())))
                {
                    throw;
                }
            }

            return await Task.FromResult<T>(default(T));
        }

        //public static async Task WhenCanceled(this Task continuation, Action action)
        //{
        //    await continuation.ContinueWith((ant) => { action(); }, TaskContinuationOptions.OnlyOnCanceled);
        //}

        //public static async Task<T> WhenCanceled<T>(this Task<T> continuation, Action action)
        //{
        //    return await continuation.ContinueWith((ant) => { action(); return default(T); }, 
        //        TaskContinuationOptions.OnlyOnCanceled);
        //}

        public static async Task<IDisposable> AcquireAsync<T>
            (this SemaphoreSlim semaphore, CancellationToken cancellation)
        {
            await semaphore.WaitAsync(cancellation);
            return new DisposingAction(() => semaphore.Release());
        }
    }
}
