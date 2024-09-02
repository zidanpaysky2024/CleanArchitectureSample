﻿namespace CleanArchitecture.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        #region Consructors
        public NotFoundException()
           : base()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
        #endregion
    }
}
