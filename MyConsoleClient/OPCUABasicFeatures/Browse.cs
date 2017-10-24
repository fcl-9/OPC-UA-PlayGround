using System;
using Opc.Ua;
using Opc.Ua.Client;

namespace MyConsoleClient.OPCUABasicFeatures { 
    public class Browse
    {
        private byte[] continuationPoint;
        private ReferenceDescriptionCollection nodeReferences;
        private byte[] nextConnectionPoint;
        private ReferenceDescriptionCollection nextNodeReferences;

        public Browse()
        {
            var connecting = new Connection();
            connecting.MSession.Browse(null,
                    null,
                    ObjectIds.ObjectsFolder,
                    0u,
                    BrowseDirection.Forward,
                    ReferenceTypeIds.HierarchicalReferences,
                    true,
                    (uint)NodeClass.Variable | (uint)NodeClass.Object | (uint)NodeClass.Method,
                    out continuationPoint,
                    out nodeReferences);

            foreach (var node in nodeReferences)
            {
                Console.WriteLine($" - {node.DisplayName} {node.BrowseName} {node.NodeClass} ");

                connecting.MSession.Browse(null, null,ExpandedNodeId.ToNodeId(node.NodeId, connecting.MSession.NamespaceUris),0u,
                    BrowseDirection.Forward, ReferenceTypeIds.HierarchicalReferences,
                    true,
                    (uint)NodeClass.Variable | (uint)NodeClass.Object | (uint)NodeClass.Method,
                    out nextConnectionPoint,
                    out nextNodeReferences);
                foreach (var nextNode in nextNodeReferences)
                {
                    Console.WriteLine($"     {nextNode.DisplayName} {nextNode.BrowseName}, {nextNode.NodeClass} {nextNode.TypeDefinition}");
                }
            }
        }
    }
}