using System;
using Opc.Ua;
using Opc.Ua.Client;

namespace MyConsoleClient.OPCUABasicFeatures
{
    public class TranslateBrowsePathNames
    {
        private BrowsePathCollection _myBrowsePathCollection;
        private BrowsePathResultCollection _result;
        private DiagnosticInfoCollection _diagnosticInfo;


        public TranslateBrowsePathNames()
        {
            _myBrowsePathCollection = new BrowsePathCollection();
            _result = new BrowsePathResultCollection();
            _diagnosticInfo = new DiagnosticInfoCollection();
        }

        //
        /// <summary>
        /// Translates Browse Path into ID.
        /// Even knowing that this method supports "batch" operations, to try only, this method will only translate on browse path at each time.
        /// </summary>
        /// <param name="mySession">Established Session between cvlient and server</param>
        /// <param name="rootNodeId"></param>
        /// <param name="browsePath"></param>
        public void TranslateBrowsePaths(Session mySession, string rootNodeId, string browsePath )
        {
            var myBrowsePath = new BrowsePath();
            myBrowsePath.StartingNode = new NodeId(rootNodeId);
            myBrowsePath.RelativePath = new RelativePath(browsePath); 
            _myBrowsePathCollection.Add(myBrowsePath);
            try
            {
                mySession.TranslateBrowsePathsToNodeIds(null, _myBrowsePathCollection, out _result, out _diagnosticInfo);
                printInfo();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private void printInfo()
        {
            foreach (var requestedData in _result)
            {
                Console.WriteLine($"The Status Code is: {requestedData.StatusCode}");
                foreach (var translatedNodeId in requestedData.Targets)
                {
                    Console.WriteLine($"{translatedNodeId.TargetId.Identifier.ToString()}");
                }
            }
        }
    }
}