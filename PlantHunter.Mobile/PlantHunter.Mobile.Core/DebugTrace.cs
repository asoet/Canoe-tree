// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using MvvmCross.Logging;
using System;
using System.Diagnostics;

namespace PlantHunter.Mobile.Core
{
    public class DebugTrace : IMvxLog
    {
        public bool IsLogLevelEnabled(MvxLogLevel logLevel) => true;

        public bool Log(MvxLogLevel logLevel, Func<string> messageFunc, Exception exception = null, params object[] formatParameters)
        {
            Debug.WriteLine(logLevel + ":" + messageFunc());

            return true;
        }
    }
}