using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace PocServiceBus.BusMessaging
{
    public interface IHandlerData
    {
        IHandlerData SetNext(IHandlerData handler);
        Task<string> Handle(string message);
    }
    public abstract class AbstractHandlerData : IHandlerData
    {
        private IHandlerData _nextHandler;

        public IHandlerData SetNext(IHandlerData handler)
        {
            this._nextHandler = handler;

            return handler;
        }

        public virtual Task<string> Handle(string request)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }
}
