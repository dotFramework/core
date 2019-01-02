using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DotFramework.Core
{
    public interface IBlockingQueue<T> : IDisposable
    {
        void Enqueue(T item);
        T Dequeue();
        bool TryDequeue(CancellationTokenSource tokenSource, out T item);
    }

    public sealed class BlockingQueue<T> : IBlockingQueue<T>
    {
        private object _lock;
        private SemaphoreSlim semaphore;
        private Queue<T> queue;

        public BlockingQueue()
        {
            this._lock = new object();
            this.semaphore = new SemaphoreSlim(0);
            this.queue = new Queue<T>();
        }

        public void Enqueue(T item)
        {
            lock (this._lock)
            {
                this.queue.Enqueue(item);
                this.semaphore.Release();
            }
        }

        public T Dequeue()
        {
            this.semaphore.Wait();
            lock (this._lock)
            {
                T item = this.queue.Dequeue();
                return item;
            }
        }

        public bool TryDequeue(CancellationTokenSource tokenSource, out T item)
        {
            try
            {
                this.semaphore.Wait(tokenSource.Token);
                lock (this._lock)
                {
                    item = this.queue.Dequeue();
                    return true;
                }
            }
            catch (OperationCanceledException)
            {
                item = default(T);
                return false;
            }
        }

        #region IDisposable

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                this.semaphore.Dispose();
                this.semaphore = null;

                this.queue.Clear();
                this.queue = null;

                this._lock = null;
            }

            // Free any unmanaged objects here.
            //
            this.disposed = true;
        }

        #endregion
    }
}
