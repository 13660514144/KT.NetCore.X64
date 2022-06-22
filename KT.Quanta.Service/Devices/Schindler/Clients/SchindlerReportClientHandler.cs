using DotNetty.Buffers;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Schindler.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KT.Quanta.Service.Devices.Schindler.Clients
{
    public class SchindlerReportClientHandler : SchindlerClientHandlerBase, ISchindlerReportClientHandler
    {
        private ISchindlerReponseHandler _schindlerReponseHandler;
        private ILogger<SchindlerReportClientHandler> _logger;

        public SchindlerReportClientHandler(ILogger<SchindlerReportClientHandler> logger,
            ISchindlerReponseHandler schindlerReponseHandler)
            : base(logger)
        {
            _schindlerReponseHandler = schindlerReponseHandler;
            _logger = logger;
        }

        public override async Task HandleResult(IByteBuffer byteBuffer)
        {
            var value = byteBuffer.ToString(Encoding.UTF8);
            _logger.LogInformation($"Schindler Report 接收数据：{value} ");

            var documents = SchindlerHelper.GetXmls(value);
            if (documents?.FirstOrDefault() == null)
            {
                _logger.LogError($"Schindler Report 接收到的数据为空！");
                return;
            }
            foreach (var document in documents)
            {
                var rootNode = document.SelectSingleNode("PTDB3_201");
                if (rootNode == null)
                {
                    rootNode = document.SelectSingleNode("PTDBS");
                }
                if (rootNode == null)
                {
                    _logger.LogWarning($"迅达电梯接受Node类型不存在！");
                    continue;
                }
                var heartbeatDocument = rootNode.SelectSingleNode("Heartbeat");
                if (heartbeatDocument != null)
                {
                    _logger.LogInformation($"Schindler Report 接收心跳数据！ ");
                }
                //通行
                var allocationDocument = rootNode.SelectSingleNode("Allocation");
                if (allocationDocument != null)
                {
                    var serializer = new XmlSerializer(typeof(SchindlerReportAllocationResponse));
                    using var stream = new StringReader(allocationDocument.OuterXml);
                    var result = (SchindlerReportAllocationResponse)serializer.Deserialize(stream);

                    _logger.LogInformation($"Schindler Report 接收派梯数据：{JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");

                    await _schindlerReponseHandler.UploadPassAsync(result, CommunicateDeviceInfo);
                }
                var availabilityDocument = rootNode.SelectSingleNode("Availability");
                if (availabilityDocument != null)
                {

                }
                var timingsListDocument = rootNode.SelectSingleNode("TimingsList");
                if (timingsListDocument != null)
                {

                }
                var activeTimingsDocument = rootNode.SelectSingleNode("ActiveTimings");
                if (activeTimingsDocument != null)
                {

                }

            }
        }
    }
}
