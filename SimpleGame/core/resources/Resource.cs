using System;

namespace SimpleGame.Core.Resources
{
    public abstract class Resource : IDisposable
    {
        private bool _disposed = false;

        protected void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected virtual void FreeManaged() {}
        protected virtual void FreeUnmanaged() {}

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                FreeManaged();
            }

            FreeUnmanaged();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Resource()
        {
            Dispose(false);
        }
    }
}