using System;
using System.Collections.Generic;

namespace FinalProject.Messaging
{
    public class MessageCenter
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

        public void AddListener<A, B, C>(string message, Callback<A, B, C> callback)
        {
            if (!listeners.ContainsKey(message))
            {
                listeners[message] = new List<Delegate>();
            }
            listeners[message].Add(callback);
        }

        public void AddListener<A, B, C, D>(string message, Callback<A, B, C, D> callback)
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

        public void Broadcast<A>(string message, A parameterOne)
        {
            if (listeners.ContainsKey(message))
            {
                foreach (Delegate callbackDelegate in listeners[message])
                {
                    Callback<A> callback = callbackDelegate as Callback<A>;
                    if (callback != null)
                    {
                        callback(parameterOne);
                    }
                }
            }
        }

        public void Broadcast<A, B>(string message, A parameterOne, B parameterTwo)
        {
            if (listeners.ContainsKey(message))
            {
                foreach (Delegate callbackDelegate in listeners[message])
                {
                    Callback<A, B> callback = callbackDelegate as Callback<A, B>;
                    if (callback != null)
                    {
                        callback(parameterOne, parameterTwo);
                    }
                }
            }
        }

        public void Broadcast<A, B, C>(string message, A parameterOne, B parameterTwo, C parameterThree)
        {
            if (listeners.ContainsKey(message))
            {
                foreach (Delegate callbackDelegate in listeners[message])
                {
                    Callback<A, B, C> callback = callbackDelegate as Callback<A, B, C>;
                    if (callback != null)
                    {
                        callback(parameterOne, parameterTwo, parameterThree);
                    }
                }
            }
        }

        public void Broadcast<A, B, C, D>(string message, A parameterOne, B parameterTwo, C parameterThree, D parameterFour)
        {
            if (listeners.ContainsKey(message))
            {
                foreach (Delegate callbackDelegate in listeners[message])
                {
                    Callback<A, B, C, D> callback = callbackDelegate as Callback<A, B, C, D>;
                    if (callback != null)
                    {
                        callback(parameterOne, parameterTwo, parameterThree, parameterFour);
                    }
                }
            }
        }

        public void RemoveListener(string message, Callback callback)
        {
            if (listeners.ContainsKey(message))
            {
                listeners[message].Remove(callback);
            }
        }

        public void RemoveListener<A>(string message, Callback<A> callback)
        {
            if (listeners.ContainsKey(message))
            {
                listeners[message].Remove(callback);
            }
        }

        public void RemoveListener<A, B>(string message, Callback<A, B> callback)
        {
            if (listeners.ContainsKey(message))
            {
                listeners[message].Remove(callback);
            }
        }

        public void RemoveListener<A, B, C>(string message, Callback<A, B, C> callback)
        {
            if (listeners.ContainsKey(message))
            {
                listeners[message].Remove(callback);
            }
        }

        public void RemoveListener<A, B, C, D>(string message, Callback<A, B, C, D> callback)
        {
            if (listeners.ContainsKey(message))
            {
                listeners[message].Remove(callback);
            }
        }
    }
}