using System;
using Opc.Ua.Client;
using Opc.Ua;

namespace MyConsoleClient.OPCUABasicFeatures
{
    public class WriteAttribute
    {
        private WriteValueCollection myCollectionOfValuesToWrite;
        private StatusCodeCollection myRequestStatus;
        private DiagnosticInfoCollection myDiagnoticCollection;

        public WriteAttribute()
        {
            myCollectionOfValuesToWrite = new WriteValueCollection();
            myRequestStatus = new StatusCodeCollection();
            myDiagnoticCollection = new DiagnosticInfoCollection();
        }

        public void WriteAttributes(Session mySession,string nodeId)
        {
            //Generate a random number
            Random rand = new Random();
            DataValue value = new DataValue();
            value.Value = rand.NextDouble();

            //Create value to be written to the value attribute of the current node
            WriteValue myValue = new WriteValue();
            myValue.NodeId = new NodeId(nodeId);
            myValue.AttributeId = Attributes.Value;
            myValue.Value = value;
            myCollectionOfValuesToWrite.Add(myValue);
            try
            {
                //Write Request
                ResponseHeader myStatus = mySession.Write(null, myCollectionOfValuesToWrite, out myRequestStatus, out myDiagnoticCollection);
                ClientBase.ValidateResponse(myRequestStatus, myCollectionOfValuesToWrite);
                ClientBase.ValidateDiagnosticInfos(myDiagnoticCollection, myCollectionOfValuesToWrite);
                Console.WriteLine($"The NodeID {nodeId}, was written with the value of {myValue.Value}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}