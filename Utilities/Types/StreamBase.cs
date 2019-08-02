using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Extensions;

namespace Utilities.Types
{
    public class StreamBase : Stream
    {
        readonly Stream _baseStream;

        public override bool CanRead => _baseStream.CanRead;
        public override bool CanSeek => _baseStream.CanSeek;
        public override bool CanWrite => _baseStream.CanWrite;
        public override long Length => _baseStream.Length;
        public override long Position
        {
            get => _baseStream.Position;
            set => _baseStream.Position = value;
        }

        public override bool CanTimeout => _baseStream.CanTimeout;
        public override int ReadTimeout
        {
            get => _baseStream.ReadTimeout;
            set => _baseStream.ReadTimeout = value;
        }
        public override int WriteTimeout
        {
            get => _baseStream.WriteTimeout;
            set => _baseStream.WriteTimeout = value;
        }

        public StreamBase(Stream baseStream)
        {
            _baseStream = baseStream;
        }

        public override void Flush()
        {
            _baseStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _baseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _baseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _baseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _baseStream.Write(buffer, offset, count);
        }

        public override void Close()
        {
            _baseStream.Close();
        }
    }
}
