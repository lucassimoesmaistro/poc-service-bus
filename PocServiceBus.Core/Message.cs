﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PocServiceBus.Core
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public int Id { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
