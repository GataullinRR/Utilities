using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace Utilities.Types
{
    // Debugger.Break(); бросает исключения на некоторых машинах!!!
    public static class Reporter
    {
        public static void ReportInfo(string msg)
        {
            MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ReportInfo(string eventName, string reason)
        {
            MessageBox.Show(reason, eventName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ReportErrorAndExit(Exception ex)
        {
            ReportErrorAndExit(null, ex);
        }
        public static void ReportErrorAndExit(string msg)
        {
            ReportErrorAndExit(msg, null);
        }
        public static void ReportErrorAndExit(string msg, Exception ex)
        {
            var text = generateMessageBody(msg, ex) + Global.NL + Global.NL +
                "Программа будет завершена.";
            MessageBox.Show(text, "Неустранимая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.FailFast(msg, ex);
        }

        //public static void ReportWarning(string msg)
        //{
        //    MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        public static void ReportError(string msg)
        {
            ReportError(msg, null);
        }
        public static void ReportError(Exception ex)
        {
            ReportError(null, ex);
        }
        public static void ReportError(string msg, Exception ex)
        {
            MessageBox.Show(generateMessageBody(msg, ex), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ReportUnknownError(string msg, Exception ex)
        {
            MessageBox.Show(generateMessageBody(msg, ex), "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static string generateMessageBody(string msg, Exception ex)
        {
            var exceptionText = ex == null
                    ? null
                    : $"Тип исключения: {ex.GetType().ToString()}{Global.NL}" + $"Сообщение: {ex.Message}";
            if (exceptionText == null)
            {
                return msg ?? "";
            }
            else
            {
                if (msg == null)
                {
                    return exceptionText;
                }
                else
                {
                    return msg + Global.NL + exceptionText;
                }
            }
        }
    }
}
