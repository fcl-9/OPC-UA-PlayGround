using System;
using System.Runtime.CompilerServices;
using Opc.Ua;
using Opc.Ua.Client;
using Org.BouncyCastle.Asn1;

namespace MyConsoleClient.OPCUABasicFeatures
{
    public class ReadAttribute
    {
        //Reads value attribute of a node
        public void ReadValueAttribute(Session mySession,string nodeId)
        {
            var connecting = new Connection();
            try
            {
                var dataValue = mySession.ReadValue(new NodeId(nodeId));
                printInfo(dataValue);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured: " + e);
            }

        }
        // Reads value attribute of a node and check if the returned type is the supposed
        public void ReadValueAttributeWithExpectedType(Session mySession, string nodeID, Type expectedType)
        {
            try
            {
                var myObject = mySession.ReadValue(nodeID, expectedType);
                printInfo(myObject);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured: " + e);
            }

        }
        //Reads all attributes of a node
        public void ReadAllAttributes(Session mySession, string nodeId)
        {
            var connecting = new Connection();
            var myNode = mySession.ReadNode(new NodeId(nodeId));
            printInfo(myNode);
        }

        private void printInfo(DataValue myData)
        {
            Console.WriteLine($"The status code is {myData.StatusCode}");
            Console.WriteLine($"The Data Value is {myData.Value}");
        }

        private void printInfo(Object myData)
        {
            Console.WriteLine(myData.GetType());
        }

        private void printInfo(Node myNode)
        {
            Console.WriteLine($"Read the node with id {myNode.NodeId}");
            Console.WriteLine($"DisplayName is: {myNode.DisplayName.Text}");
            Console.WriteLine($"BrowseName is: {myNode.BrowseName.Name}");
            Console.WriteLine($"Description is: {myNode.Description}");
            Console.WriteLine($"NodeClass is: {myNode.NodeClass.ToString()}");
            if (myNode.NodeClass == NodeClass.Variable)
            {
                VariableNode variableNode = myNode as VariableNode;
                Console.WriteLine($"  DataType is  {variableNode.DataType}");
                Console.WriteLine($"  Value Rank is  {variableNode.ValueRank.ToString()}" );
                printInfo(variableNode.Value);
                Console.WriteLine($"  Value is  {variableNode.UserAccessLevel.ToString()}");
                Console.WriteLine($"  Value is Historizing: {variableNode.Historizing}");
                Console.WriteLine($"  Value sampling interval: {variableNode.MinimumSamplingInterval.ToString()}");
            }
        }

          

    }
}