using System;
using System.Collections.Generic;

namespace FinalProject.Messaging
{
    internal class MessageCenter
    {
        private Dictionary<string, List<Delegate>> listeners;

        public MessageCenter()
        {
            listeners = new Dictionary<string, List<Delegate>>();
        }

        public void AddListener(string message, Callback callback)
        {
            if (!listeners.ContainsKey(message))
            {
                listeners[message] = new List<Delegate>();
            }
            listeners[message].Add(callback);
        }

        public void AddListener<A>(string message, Callback<A> callback)
        {
            if (!listeners.ContainsKey(message))
            {
                listeners[message] = new List<Delegate>();
            }
            listeners[message].Add(callback);
        }

        public void AddListener<A, B>(string message, Callback<A, B> callback)
        {
            if (!listeners.ContainsKey(message))
            {
                listeners[message] = new List<Delegate>();
            }
            listeners[message].Add(callback);
        }

        public void Broadcast(string message)
        {
            if (listeners.ContainsKey(message))
            {
                foreach (Delegate callbackDelegate in listeners[message])
                {
                    Callback callback = callbackDelegate as Callback;
                    if (callback != null)
                    {
                        callback();
                    }
                }
            }
        }

        public void Broadcast<A>(string message, A parameterA)
        {
            if (listeners.ContainsKey(message))
            {
                foreach (Delegate callbackDelegate in listeners[message])
                {
                    Callback<A> callback = callbackDelegate as Callback<A>;
                    if (callback != null)
                    {
                        callback(parameterA);
                    }
                }
            }
        }

        public void Broadcast<A, B>(string message, A parameterA, B parameterB)
        {
            if (listeners.ContainsKey(message))
            {
                foreach (Delegate callbackDelegate in listeners[message])
                {
                    Callback<A, B> callback = callbackDelegate as Callback<A, B>;
                    if (callback != null)
                    {
                        callback(parameterA, parameterB);
                    }
                }
            }
        }

        public void RemoveListener(string message, Delegate callback)
        {
            if (listeners.ContainsKey(message))
            {
                listeners[message].Remove(callback);
            }
        }
    }
}