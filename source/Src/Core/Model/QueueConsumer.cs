using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DotFramework.Core
{
    public interface IQueueConsumer<T> : IDisposable
    {
        int ConsumersCount { get; }
        IBlockingQueue<T> Queue { get; }
        void Start(IBlockingQueue<T> queue, Action<T> process);
        void Stop();
    }

    public sealed class QueueConsumer<T> : IQueueConsumer<T>
    {
        private object _lock;

        public int ConsumersCount { get; }

        private Action<T> process;
        public IBlockingQueue<T> Queue { get; private set; }

        private List<Thread> threads;
        private List<CancellationTokenSource> tokenSources;
        private bool stopRequested;

        public QueueConsumer(int consumersCount)
        {
            if (consumersCount < 1)
            {
                throw new ArgumentException($"{nameof(consumersCount)} must be greater than 0");
            }

            this.ConsumersCount = consumersCount;
            this._lock = new object();
        }

        public void Start(IBlockingQueue<T> queue, Action<T> process)
        {
            lock (_lock)
            {
                if (this.Queue == null)
                {
                    this.Queue = queue;
                    this.process = process;

                    this.stopRequested = false;

                    this.tokenSources = Enumerable.Range(0, this.ConsumersCount).Select(i => new CancellationTokenSource()).ToList();

                    this.threads = Enumerable.Range(0, this.ConsumersCount).Select(i => GetThread(i)).ToList();
                    this.threads.ForEach(thread => thread.Start());
                }
            }
        }

        public void Stop()
        {
            lock (this._lock)
            {
                if (this.Queue != null)
                {
                    this.stopRequested = true;

                    this.tokenSources.ForEach(tokenSource => tokenSource.Cancel());
                    this.tokenSources.Clear();
                    this.tokenSources = null;

                    this.threads.ForEach(thread => thread.Join());
                    this.threads.Clear();
                    this.threads = null;

                    this.Queue = null;
                    this.process = null;
                }
            }
        }

        private void ProcessLoop(int id)
        {
            while (true)
            {
                if (this.stopRequested)
                {
                    break;
                }
                if (this.Queue.TryDequeue(this.tokenSources[id], out T item))
                {
                    this.process(item);
                }
            }
        }

        private Thread GetThread(int id)
        {
#if NET40 || NET45 || NET46 || NET47 || NET471 || NET472
            return new Thread(() => ProcessLoop(id));
#elif NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
            return new Thread((_) => ProcessLoop(id));
#endif
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
                Stop();
                this._lock = null;
            }

            // Free any unmanaged objects here.
            //
            this.disposed = true;
        }

        ~QueueConsumer()
        {
            Dispose(false);
        }

        #endregion
    }
}
