using Opc.Ua;
using Opc.Ua.Client;

namespace MyConsoleClient.OPCUABasicFeatures
{
    public class Query
    {
        private ViewDescription _myView;
        private NodeTypeDescriptionCollection _myNodeTypeCollection;
        private ContentFilter _myFilter;
        private uint _myMaxDataSetsToReturn;
        private uint _myMaxReferenceToReturn;
        private QueryDataSetCollection _myQueryDataSets;
        private byte[] _myContinuationPoint;
        private ParsingResultCollection _myParsingResults;
        private DiagnosticInfoCollection _myDiagnosticInfos;
        private ContentFilterResult _myFilterResult;

        public Query()
        {
            _myView = new ViewDescription()
            {
                ViewId = new NodeId() //Id of the view we want to query

            };
        }

        public void QueryRunner(Session mySession)
        {
            mySession.QueryFirst(null, 
                _myView,
                _myNodeTypeCollection,
                _myFilter,
                _myMaxDataSetsToReturn,
                _myMaxReferenceToReturn,
                out _myQueryDataSets,
                out _myContinuationPoint,
                out _myParsingResults,
                out _myDiagnosticInfos,
                out _myFilterResult
                );
            printData();
        }

        private void printData()
        {
            
        }
    }
}