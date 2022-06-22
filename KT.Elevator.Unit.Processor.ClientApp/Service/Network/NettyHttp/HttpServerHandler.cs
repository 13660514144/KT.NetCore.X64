using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Common;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Common.WpfApp.Helpers;
using KT.Elevator.Unit.Processor.ClientApp.Service.Network.Controllers;
using KT.Elevator.Unit.Processor.ClientApp.Service.Network.NettyServer;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using KT.Quanta.Common.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Network.NettyServer
{
    public class HttpServerHandler : ChannelHandlerAdapter
    {
        private ILogger _logger;
        private AllController _all;
        public HttpServerHandler()
        {
            _logger = ContainerHelper.Resolve<ILogger>();
            _all = ContainerHelper.Resolve<AllController>();
        }

        volatile ICharSequence date = NettyHttpHelper.Cache.Value;

        public override void ChannelRead(IChannelHandlerContext ctx, object message)
        {
            ReadAsync(ctx, message);
        }

        private async void ReadAsync(IChannelHandlerContext ctx, object message)
        {
            if (message is IHttpRequest request)
            {
                try
                {
                    await ProcessAsync(ctx, request);
                }
                finally
                {
                    ReferenceCountUtil.Release(message);
                }
            }
            else
            {
                ctx.FireChannelRead(message);
            }

            await Task.CompletedTask;
        }

        async Task ProcessAsync(IChannelHandlerContext ctx, IHttpRequest request)
        {
            var actions = request.Uri.Split("/");
            if (actions.Length < 2)
            {
                await NoFoundAsync(ctx);
                return;
            }
            if (actions[0].ToLower() == "all")
            {
                //var allController = ContainerHelper.Resolve<AllController>();
                //if (actions[1].ToLower() == "addoreditpassrights")
                //{
                //    var data = JsonConvert.DeserializeObject<List<UnitPassRightEntity>>(ctx.Channel.Read());
                //    await allController.AddOrEditPassRightsAsync(data);
                //} 
                //if (actions[1].ToLower() == "addoreditpassright")
                //{
                //    var data = JsonConvert.DeserializeObject<UnitPassRightEntity>(request.Result);
                //    await allController.AddOrEditPassRightAsync(data);
                //}

                //if (actions[1].ToLower() == "deletepassright")
                //{
                //    var data = JsonConvert.DeserializeObject<DeleteNormalModel>(request.Result);
                //    await allController.DeletePassRightAsync(data);
                //}
 
                //if (actions[1].ToLower() == "addoreditcarddevices")
                //{
                //    var data = JsonConvert.DeserializeObject<List<UnitCardDeviceEntity>>(request.Result);
                //    await allController.AddOrEditCardDevicesAsync(data);
                //}

                //if (actions[1].ToLower() == "addoreditcarddevice")
                //{
                //    var data = JsonConvert.DeserializeObject<UnitCardDeviceEntity>(request.Result);
                //    await allController.AddOrEditCardDeviceAsync(data);
                //}
  
                //if (actions[1].ToLower() == "deletecarddevice")
                //{
                //    var data = JsonConvert.DeserializeObject<DeleteNormalModel>(request.Result);
                //    await allController.DeleteCardDeviceAsync(data);
                //}
  
                //if (actions[1].ToLower() == "handleelevatorsuccess")
                //{
                //    var data = JsonConvert.DeserializeObject<HandledElevatorSuccessModel>(request.Result);
                //    await allController.HandleElevatorSuccessAsync(data);
                //}
      
                //if (actions[1].ToLower() == "addoredithandleelevatordevice")
                //{
                //    var data = JsonConvert.DeserializeObject<UnitHandleElevatorDeviceModel>(request.Result);
                //    await allController.AddOrEditHandleElevatorDeviceAsync(data);
                //}
      
                //if (actions[1].ToLower() == "deletehandleelevatordevice")
                //{
                //    var data = JsonConvert.DeserializeObject<DeleteNormalModel>(request.Result);
                //    await allController.DeleteHandleElevatorDeviceAsync(data);
                //} 
     
                //if (actions[1].ToLower() == "righthandleelevator")
                //{
                //    var data = JsonConvert.DeserializeObject<RightHandleElevatorModel>(request.Result);
                //    await allController.RightHandleElevatorAsync(data);
                //} 
            }

            await NoFoundAsync(ctx);
        }

        private async Task NoFoundAsync(IChannelHandlerContext ctx)
        {
            var response = new DefaultFullHttpResponse(HttpVersion.Http11, HttpResponseStatus.NotFound, Unpooled.Empty, false);
            await ctx.WriteAndFlushAsync(response);
            await ctx.CloseAsync();
        }

        public async Task ResponseAsync(IChannelHandlerContext ctx, IResponse data)
        {
            var json = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);
            var bytes = Encoding.UTF8.GetBytes(json);
            var length = AsciiString.Cached(bytes.Length.ToString());

            await WriteResponseAsync(ctx, Unpooled.WrappedBuffer(bytes), NettyHttpHelper.TypeJson, length);
        }

        private async Task WriteResponseAsync(IChannelHandlerContext ctx, IByteBuffer buf, ICharSequence contentType, ICharSequence contentLength)
        {
            // Build the response object.
            var response = new DefaultFullHttpResponse(HttpVersion.Http11, HttpResponseStatus.OK, buf, false);
            HttpHeaders headers = response.Headers;
            headers.Set(NettyHttpHelper.ContentTypeEntity, contentType);
            headers.Set(NettyHttpHelper.ServerEntity, NettyHttpHelper.ServerName);
            headers.Set(NettyHttpHelper.DateEntity, this.date);
            headers.Set(NettyHttpHelper.ContentLengthEntity, contentLength);

            // Close the non-keep-alive connection after the write operation is done.
            await ctx.WriteAsync(response);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            context.CloseAsync();
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();
        }
    }
}