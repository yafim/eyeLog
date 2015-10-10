using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EyeLog
{
    public partial class EyeLogForm : Form
    {

        private List<string> m_Json;

        /// <summary>
        /// Holds the running time of current session.
        /// </summary>
        private readonly Stopwatch m_StopWatch;

        /// <summary>
        /// Coordinates to log.
        /// </summary>
        private readonly List<string> m_MouseCoords;

        /// <summary>
        /// Tray application
        /// </summary>
        private NotifyIcon m_TrayIcon;

        /// <summary>
        /// 
        /// </summary>
        private ContextMenu m_TrayMenu;

        /// <summary>
        /// Initializes a new instance of the EyeLogForm class.
        /// </summary>
        public EyeLogForm()
        {
            m_StopWatch = new Stopwatch();
            CreateTrayMenu();

            m_MouseCoords = new List<string>();
            m_Json = new List<string>();

            InitializeComponent();
        }

        /// <summary>
        /// Create a simple tray menu with some items
        /// </summary>
        private void CreateTrayMenu()
        {
            m_TrayMenu = new ContextMenu();
            m_TrayMenu.MenuItems.Add("Exit", OnExit);
            m_TrayMenu.MenuItems.Add("Start", StartRecordingCoordinates);
            m_TrayMenu.MenuItems.Add("Stop", StopRecordingCoordinates);

            //TODO: different method?
            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            m_TrayIcon = new NotifyIcon
            {
                Icon = new Icon(SystemIcons.Application, 40, 40),
                ContextMenu = m_TrayMenu,
                Visible = true
            };
        }

        /// <summary>
        /// 1. Hide form window.
        /// 2. Remove from taskbar.
        /// 3. Set the location to top left.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            Location = new Point(-8, -31);
            base.OnLoad(e);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Build json file
        /// </summary>
        /// <returns>json file</returns>
        private void BuildJson()
        {
            // m_Json start
            m_Json.Add("{\n \"data\":[\n");
            m_Json.AddRange(m_MouseCoords);

            // m_Json end
            m_Json.Add("{}\n]}");

        }

        /// <summary>
        /// Write logs into file
        /// </summary>
        private void WriteToFile()
        {
            m_StopWatch.Stop();

            BuildJson();

            //TODO: Let the user choose the destination
            // Set a variable to the My Documents path.
            string mydocpath = @"C:\Users\ifimv_000\Documents\alsProject\videoAnalysis\bower_components\videoLogs";

            //TODO: Let the user choose the name of the file 
            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\file.json"))
            {
                foreach (string line in m_Json)
                {
                    outputFile.WriteLine(line);
                }
            }

        }

        /// <summary>
        /// Clear coordinates logs
        /// </summary>
        private void ClearLogs()
        {
            m_MouseCoords.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string coords = Utils.Utils.ControllerCoordinates(this, m_StopWatch);
            m_MouseCoords.Add(coords);
        }

        private void StartRecordingCoordinates(object sender, EventArgs e)
        {
            timer1.Start();
            m_StopWatch.Start();
        }

        /// <summary>
        /// Stop recording and reset all stractures.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopRecordingCoordinates(object sender, EventArgs e)
        {
            timer1.Stop();
            WriteToFile();
            ClearLogs();
            m_StopWatch.Reset();
            m_Json.Clear();
        }


    }
}
