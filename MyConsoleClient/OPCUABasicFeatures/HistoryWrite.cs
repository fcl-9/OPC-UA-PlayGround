using System;
using Opc.Ua;
using Opc.Ua.Client;

namespace MyConsoleClient.OPCUABasicFeatures
{
    public class HistoryWrite
    {
        private ExtensionObjectCollection nodesToUpdate;
        private HistoryUpdateResultCollection results;
        private DiagnosticInfoCollection diagnosticInfo;
        public HistoryWrite()
        {
            nodesToUpdate = new ExtensionObjectCollection();
            results = new HistoryUpdateResultCollection();
            diagnosticInfo = new DiagnosticInfoCollection();
        }

        public void UpdateHistory(Session mySession, UpdateDataDetails updateDetails)
        {
            ExtensionObject myExtObject = new ExtensionObject(updateDetails);
            nodesToUpdate.Add(myExtObject);
            mySession.HistoryUpdate(null, nodesToUpdate, out results, out diagnosticInfo);
            foreach (var changedData in results)
            {
                Console.WriteLine($"This is my status: {changedData.StatusCode}");
                Console.WriteLine($"This is my new value {updateDetails.UpdateValues[0].Value.ToString()}");
            }
        }
    }
}