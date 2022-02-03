using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransDataLib;

namespace StatimatService
{
    public enum MessagePower
    {
        Error,
        Common,
        Warning,
        Critical
    }

    public partial class StatimatService : ServiceBase
    {

        public delegate void ServiceStringLogDelegate(string message, MessagePower power);

        public event ServiceStringLogDelegate Error;

        public event ServiceStringLogDelegate Start;

        public event ServiceStringLogDelegate Close;

        public event ServiceStringLogDelegate Pause;

        public event ServiceStringLogDelegate Resume;

        public event ServiceStringLogDelegate Message;

        public Thread ServiceThread;

        public StatimatService() => InitializeComponent();

        public void AddLog(string msg, MessagePower power)
        {
            try
            {
                if (!EventLog.SourceExists("StatimatServiceLog"))
                {
                    EventLog.CreateEventSource("StatimatServiceLog", "StatimatServiceLog");
                }
                EventLogger.Source = "StatimatServiceLog";
                switch(power)
                {
                    case MessagePower.Common:
                        {
                            EventLogger.WriteEntry($"{DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()} : {msg} ", EventLogEntryType.Information);
                            break;
                        }
                    case MessagePower.Error:
                        {
                            EventLogger.WriteEntry($"{DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()} : {msg} ", EventLogEntryType.Error);
                            break;
                        }
                    case MessagePower.Warning:
                        {
                            EventLogger.WriteEntry($"{DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()} : {msg} ", EventLogEntryType.Warning);
                            break;
                        }
                    case MessagePower.Critical:
                        {
                            EventLogger.WriteEntry($"{DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()} : {msg} ", EventLogEntryType.FailureAudit);
                            break;
                        }
                }

            }
            catch { }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Error += AddLog;
                Start += AddLog;
                Close += AddLog;
                Pause += AddLog;
                Resume += AddLog;
                Message += AddLog;

                ServiceThread = new Thread(new ThreadStart(StartTheThread));
                ServiceThread.Start();
                Start($" Служба была запущена", MessagePower.Common);
            }
            catch (Exception e)
            {
                Error("Ошибка работы службы - " + e.ToString(), MessagePower.Error);
            }
        }

        protected override void OnStop()
        {
            ServiceThread.Abort();
            Close($"{DateTime.Now.ToShortDateString()}:{DateTime.Now.ToShortTimeString()} : Служба была отклчюена <<<<<<<<<<<<<<<", MessagePower.Warning);
        }

        protected override void OnPause()
        {
            ServiceThread.Suspend();
            Pause($"{DateTime.Now.ToShortDateString()}:{DateTime.Now.ToShortTimeString()} : Служба была приостановлена <<<<<<<<<<<<<<<", MessagePower.Warning);
            base.OnPause();
        }

        protected override void OnContinue()
        {
            ServiceThread.Resume();
            Resume($"{DateTime.Now.ToShortDateString()}:{DateTime.Now.ToShortTimeString()} : Служба продолжила работу", MessagePower.Common);
            base.OnContinue();
        }

        private void StartTheThread()
        {
            while (Thread.CurrentThread.ThreadState != System.Threading.ThreadState.Aborted && Thread.CurrentThread.ThreadState != System.Threading.ThreadState.Suspended)
            {
                try
                {
                    Message(Charon.Transfer(Setuper.Instance.LastRowsCount), MessagePower.Common);
                    try { Thread.Sleep(Setuper.Instance.RefreshRate); } catch { }
                }
                catch (Exception e)
                {
                    Error("Во время работы службы произошла ошибка. " + e.ToString(), MessagePower.Error);
                }
            }
        }

        private void EventLogger_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}
