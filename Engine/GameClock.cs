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
        static GameClock _instance = null;

        /// <summary>
        /// Constructor
        /// </summary>
        GameClock()
        {
            _start = DateTime.Now;
        }

        /// <summary>
        /// Get the elapsed time of the last frame
        /// </summary>
        /// <returns> Last frame elapsed time </returns>
        public float ElapsedFrame()
        {
            float elapsedTime = (float)(DateTime.Now - _frameTime).TotalSeconds;
            _frameTime = DateTime.Now;

            return elapsedTime;
        }

        /// <summary>
        /// Get elapsed time since the start of the application
        /// </summary>
        /// <returns> Elpsed time since the start </returns>
        public float ElapsedStart()
        {
            return (float)(DateTime.Now - _start).TotalSeconds;
        }

        static public GameClock GetInstance()
        {
            if(_instance == null)
            {
                _instance = new GameClock();
            }

            return _instance;
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
