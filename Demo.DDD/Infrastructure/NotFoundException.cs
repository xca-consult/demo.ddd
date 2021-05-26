﻿using System;
using System.Runtime.Serialization;

namespace Demo.DDD
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}