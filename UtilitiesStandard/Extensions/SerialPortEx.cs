using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Types;
using Utilities.Extensions;

namespace Utilities.Extensions
{
    //public static class SerialPortEx
    //{
    //    public static byte[] ClearReadBuffer(this SerialPort port)
    //    {
    //        var bytesToRead = port.BytesToRead;
    //        if (bytesToRead > 0)
    //        {
    //            var buffer = new byte[bytesToRead];
    //            port.Read(buffer, 0, bytesToRead);

    //            return buffer;
    //        }
    //        else
    //        {
    //            return new byte[0];
    //        }
    //    }

    //    public static void Write(this SerialPort port, IEnumerable<byte> data)
    //    {
    //        var buff = data.ToArray();
    //        port.Write(buff, 0, buff.Length);
    //    }

    //    /// <summary>
    //    /// Warning! Returns Lazy enumerable! So it should have been enumerated if full length read is required!
    //    /// Do not forget to set <see cref="SerialPort.ReadTimeout"/> or it can loop infinetely independently of
    //    /// <paramref name="timeout"/>. Ignores <see cref="TimeoutException"/> when thrown by <see cref="SerialPort.ReadByte"/>.
    //    /// </summary>
    //    /// <param name="port"></param>
    //    /// <param name="length"></param>
    //    /// <param name="timeout"></param>
    //    /// <returns></returns>
    //    /// <exception cref="TimeoutException"></exception>
    //    public static IEnumerable<byte> ReadExact(this SerialPort port, int length, int timeout)
    //    {
    //        var timeoutChecker = new Timeouter(timeout);
    //        for (int i = 0; i < length; i++)
    //        {
    //            var b = 0;
    //            do
    //            {
    //                timeoutChecker.ThrowIfTimeout();
    //                try
    //                {
    //                    b = port.ReadByte();
    //                }
    //                catch (TimeoutException)
    //                {
    //                    b = -1;
    //                }
    //            }
    //            while (b == -1);

    //            using (timeoutChecker.PauseHolder)
    //            {
    //                yield return (byte)b;
    //            }
    //        }
    //    }
    //}
}
