using Opc.Ua;
using Opc.Ua.Client;

namespace MyConsoleClient.OPCUABasicFeatures
{
    public class AddNode
    {
        private AddNodesItemCollection listOfNodesToAdd;
        private AddNodesResultCollection listofReturnedNodes;
        private DiagnosticInfoCollection requestStatus;
        private Session _mySession;
        public AddNode(Session mySession)
        {
            _mySession = mySession;
            listOfNodesToAdd = new AddNodesItemCollection(1);
            var connecting = new Connection();
            GenerateNewNode();
            connecting.MSession.AddNodes(null, listOfNodesToAdd, out listofReturnedNodes, out requestStatus);
        }

        private void GenerateNewNode()
        {
            AddNodesItem newNode = new AddNodesItem();
            newNode.NodeAttributes = new ExtensionObject();
            newNode.BrowseName = QualifiedName.Create("HelloWorld", _mySession.NamespaceUris.GetString(1), _mySession.NamespaceUris);
            listOfNodesToAdd.Add(newNode);

        }

    }
}