using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Utils
{
    public class Utils
    {
        public static string ControllerCoordinates(Form form, Stopwatch stopWatch)
        {
            Point relativePoint = form.PointToClient(Cursor.Position);
         //   string str = string.Format("{{\n \"x\": {0},\n\"y\": {1} \n}},\n", relativePoint.X, relativePoint.Y);
       //     string timeStamp = GetTimestamp(DateTime.Now);
        //    string timeStamp = dt.AddSeconds(seconds).ToString("HH:mm:ss");
            string timeStamp = stopWatch.Elapsed.ToString();
            
            string str = string.Format("{{\n \"x\": {0},\n\"y\": {1},\n \"timeStamp\" : \"{2}\"\n }},\n", relativePoint.X, relativePoint.Y, timeStamp);
 
            
            return str;
        }

        public static string GetTimestamp(DateTime value)
        {
        //    return value.ToString("yyyyMMddHHmmssffff");
            return value.ToString("mmssffff");
        }
    }
}
