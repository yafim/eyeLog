using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Utils
{
    public class Utils
    {
        private static DateTime m_CurrenTime;
        private static TimeSpan m_TimeElasped;

        public static string SetJsonInformation(Form form, Stopwatch stopWatch, DateTime startTime)
        {
            // Get current time
            m_CurrenTime = DateTime.Now;

            // Evaluate running time
            m_TimeElasped = m_CurrenTime.Subtract(startTime);

            // Current cursor position
            Point relativePoint = form.PointToClient(Cursor.Position);

            // Set time stamp 
            //TODO: Use GetTimeStamp method.
            string timeStamp = m_TimeElasped.ToString();

            string strToReturn = SetJsonFormat(relativePoint.X, relativePoint.Y, timeStamp);
            return strToReturn;
        }

        /// <summary>
        /// TimeStamp format
        /// TODO: Never used
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GetTimestamp(DateTime value)
        {
            return value.ToString("mmssffff");
        }

        /// <summary>
        /// Set Json information in string
        /// </summary>
        /// <param name="xPosition">X Position</param>
        /// <param name="yPosition">Y Position</param>
        /// <param name="timeStamp">Current timeStamp</param>
        /// <returns></returns>
        private static string SetJsonFormat(int xPosition, int yPosition, string timeStamp)
        {
            string json = string.Format("{{\n \"x\": {0},\n\"y\": {1},\n \"timeStamp\" : \"{2}\"\n }},\n", xPosition, yPosition, timeStamp);
            return json;
        }


    }
}
