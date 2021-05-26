using System;
using System.Runtime.Serialization;

namespace Demo.DDD.ApplicationServices.QueryServices
{
    [Serializable]
    public class ReadModelNotFoundException : NotFoundException
    {
        public ReadModelNotFoundException(string msg) : base(msg)
        {
            
        }

        protected ReadModelNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

    }
}