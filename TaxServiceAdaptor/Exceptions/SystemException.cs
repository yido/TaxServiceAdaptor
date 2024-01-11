using System;

namespace TaxServiceAdaptor.DTO.Exceptions
{
    public class SystemException : Exception
    {
        public SystemException()
            : base()
        {
        }

        public SystemException(string message)
            : base(message)
        {
        }

        public SystemException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public SystemException(ReturnCodes returnCodes)
            : base($"Code Description: \"{(Enum.GetName (typeof (ReturnCodes), returnCodes)).Replace ("_", " ")}\", Return code: ({(int) returnCodes})")
        {
            this.ReturnCode = returnCodes;
        }
        public ReturnCodes ReturnCode {get; private set;}
    }
}