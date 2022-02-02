using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SFML_Engine
{
    internal class GameClock
    {
        DateTime _start;
        DateTime _frameTime;

        /// <summary>
        /// Constructor
        /// </summary>
        public GameClock()
        {
            _start = DateTime.Now;
        }

        /// <summary>
        /// Get the elapsed time since the last frame
        /// </summary>
        /// <returns> Elapsed time since the last frame in seconds </returns>
        public float ElapsedFrame()
        {
            return (float)(DateTime.Now - _frameTime).TotalSeconds;
        }

        /// <summary>
        /// Reset the initiale frame time
        /// </summary>
        public void ResetFrame()
        {
            _frameTime = DateTime.Now;
        }

        /// <summary>
        /// Get elapsed time since the start of the application
        /// </summary>
        /// <returns> Elpsed time since the start </returns>
        public float ElapsedStart()
        {
            return (float)(DateTime.Now - _start).TotalSeconds;
        }

        /// <summary>
        /// Get the start time 
        /// </summary>
        public DateTime Start
        {
            get { return _start; }
        }

        /// <summary>
        /// Get the frame time
        /// </summary>
        public DateTime FrameTime
        {
            get { return _frameTime; }
        }
    }
}
