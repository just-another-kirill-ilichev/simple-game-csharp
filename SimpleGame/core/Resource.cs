using System;

namespace SimpleGame.Core
{
    public abstract class Resource : IDisposable
    {
        private bool _disposed = false;

        protected void CheckDisposed(string objectName)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(objectName);
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