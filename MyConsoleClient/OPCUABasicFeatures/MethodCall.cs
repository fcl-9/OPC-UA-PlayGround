using System;
using System.Collections.Generic;
using Opc.Ua;
using Opc.Ua.Client;


namespace MyConsoleClient.OPCUABasicFeatures
{
    public class MethodCall
    {
        private StatusCode statusCode = new StatusCode();
        private IList<object> outputArgs;
        public MethodCall(Session mySession, string objectNodeId, string methodNodeId, params object[] arguments)
        {
            outputArgs = new List<object>();
            var myObjectNodeId = new NodeId(objectNodeId);
            var myMethodObject = new NodeId(methodNodeId);
            try
            {
                outputArgs = mySession.Call(myObjectNodeId, myMethodObject, arguments);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Method call throws an exception {e.Message}");
            }
        }

        private void showOutPutArgs()
        {
            int i = 0;
            foreach (var arg in outputArgs)
            {
                Console.WriteLine($"Output[\"{i}\"] = {arg.ToString()}");
                i++;
            }
        }
    }
}