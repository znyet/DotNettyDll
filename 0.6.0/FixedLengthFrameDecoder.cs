using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public class FixedLengthFrameDecoder : ByteToMessageDecoder
    {
        private int frameLength;

        public FixedLengthFrameDecoder(int frameLength)
        {
            this.frameLength = frameLength;
        }

        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            object decoded = decode(context, input);
            if (decoded != null)
            {
                output.Add(decoded);
            }
        }

        protected Object decode(IChannelHandlerContext ctx, IByteBuffer input)
        {
            return input.ReadableBytes < this.frameLength ? null : input.ReadRetainedSlice(this.frameLength);
        }
    }
}

