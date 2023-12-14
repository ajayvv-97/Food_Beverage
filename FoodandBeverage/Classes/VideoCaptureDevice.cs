using System;

namespace FoodandBeverage.Classes
{
    internal class VideoCaptureDevice
    {
        private object monikerString;

        public VideoCaptureDevice(object monikerString)
        {
            this.monikerString = monikerString;
        }

        public int NewFrame { get; internal set; }

        internal void Start()
        {
            throw new NotImplementedException();
        }
    }
}