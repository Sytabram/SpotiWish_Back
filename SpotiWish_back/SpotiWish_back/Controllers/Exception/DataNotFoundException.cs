using System;

namespace SpotiWish_back.Controllers.Exception
{
    public class DataNotFoundException : System.Exception
    {
        public DataNotFoundException(string message) : base(message)
        {
        }
    }
}