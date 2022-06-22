using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using KT.Elevator.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Handlers
{
    public class ElevatorNettyHandler : ChannelHandlerAdapter
    {
        //readonly IByteBuffer initialMessage;
        private ElevatorTypeEnum _elevatorType;

        public ElevatorNettyHandler(ElevatorTypeEnum elevatorType)
        {
            _elevatorType = elevatorType;

            //this.initialMessage = Unpooled.Buffer(2048);
            //byte[] messageBytes = Encoding.UTF8.GetBytes("Hello world");
            //this.initialMessage.WriteBytes(messageBytes);
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            Console.WriteLine("ChannelActive:.......................... ");
            //context.WriteAndFlushAsync(this.initialMessage);
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var byteBuffer = message as IByteBuffer;
            if (byteBuffer != null)
            {
                Console.WriteLine("Received from server: " + byteBuffer.ToString(Encoding.UTF8));
            }

            //context.WriteAsync(message);
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            Console.WriteLine("Flush:.............................. ");
            context.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            context.CloseAsync();
        }
    }
}