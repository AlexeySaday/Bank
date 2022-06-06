using System;

namespace Bank
{
    public class LightException : Exception
    {
        public bool flag;
        public LightException(bool flag, string msg) : base(msg)
        {
            this.flag = flag;
        }
    }
}
