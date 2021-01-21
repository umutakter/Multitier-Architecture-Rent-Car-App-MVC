using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentCarApp.Commons.Abstractions;

namespace RentCarApp.Commons.Concretes.Logger
{
    internal class EventLogger : LogBase
    {

        public override void Log(string message, bool isError)
        {
            lock (lockObj)
            {
                EventLog m_EventLog = new EventLog();
                m_EventLog.Source = "RentaCarEventLog";
                m_EventLog.WriteEntry(message);
            }
        }
    }
}
