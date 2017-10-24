using System;
using Opc.Ua;

namespace MyConsoleClient.OPCUABasicFeatures
{
    public class HistoryRead
    {
        private HistoryReadResultCollection _listOfReadNodes = null;
        private DiagnosticInfoCollection _diagnosticInfo = null;
        private ExtensionObject _myExtensionObject = null;

        public HistoryRead(NodeId nodeToReadId, HistoryReadDetails readDetails)
        {
            HistoryReadValueIdCollection historicalNodesToRead = new HistoryReadValueIdCollection();
            HistoryReadValueId nodeToRead = new HistoryReadValueId();
            _myExtensionObject = new ExtensionObject(readDetails);
            nodeToRead.NodeId = nodeToReadId;
            historicalNodesToRead.Add(nodeToRead);

            var connecting = new Connection();
            connecting.MSession.HistoryRead(null, _myExtensionObject, TimestampsToReturn.Both,false,historicalNodesToRead,out _listOfReadNodes, out _diagnosticInfo);

            ClientBase.ValidateResponse(_listOfReadNodes, historicalNodesToRead);
            ClientBase.ValidateDiagnosticInfos(_diagnosticInfo, historicalNodesToRead);

            if (StatusCode.IsBad(_listOfReadNodes[0].StatusCode))
            {
                throw new ServiceResultException(_listOfReadNodes[0].StatusCode);
            }

            ShowResults();
        }

        /// <summary>
        /// Used to Print
        /// </summary>
        private void ShowResults()
        {
            int m_index = 0;

            if (_listOfReadNodes == null)
            {
                return;
            }
            //TODO: This is just a try nothing more should update this in order to be bale to read mode History Data Values when Batch Requests are used
            HistoryData results = ExtensionObject.ToEncodeable(_listOfReadNodes[0].HistoryData) as HistoryData;
            if (results == null)
            {
                return;
            }

            for (int ii = 0; ii < results.DataValues.Count; ii++)
            {
                StatusCode status = results.DataValues[ii].StatusCode;

                string index = Utils.Format("[{0}]", m_index++);
                string timestamp = results.DataValues[ii].SourceTimestamp.ToLocalTime().ToString("yyyy-MM-dd hh:mm:ss");
                string value = Utils.Format("{0}", results.DataValues[ii].WrappedValue);
                string quality = Utils.Format("{0}", (StatusCode)status.CodeBits);
                string historyInfo = Utils.Format("{0:X2}", (int)status.AggregateBits);

                Console.WriteLine(" "+ timestamp + " " + value + " " + " " + quality + " " + historyInfo);
            }
            Console.WriteLine("END");

        }

    }
}