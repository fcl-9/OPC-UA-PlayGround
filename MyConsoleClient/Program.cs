using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyConsoleClient.OPCUABasicFeatures;
using Opc.Ua;
using Org.BouncyCastle.Asn1.CryptoPro;

namespace MyConsoleClient
{
    class Program
    {
        private static Connection _connectionChannel = new Connection();

        static void Main(string[] args)
        {
            //ReadAttribute("ns=4;s=Demo.History.DoubleWithHistory");
            //TranslateNodeId("ns=0;i=85", "/Root/Objects/Demo/History/DoubleWithHistory");
            AddNode();
        }

        /// <summary>
        /// Enables writing a value to the history
        /// </summary>
        /// <param name="myNodeId">The NodeId of a node</param>
        private static void UpdateHistory(string myNodeId)
        {
            var historUpdateDataDetails = new UpdateDataDetails
            {
                NodeId = new NodeId(myNodeId),
                PerformInsertReplace = PerformUpdateType.Insert,
                UpdateValues = new DataValueCollection()
            };

            //Generates new values
            var dataToHistory = new DataValue
            {
                SourceTimestamp = DateTime.Now.ToUniversalTime(),
                ServerTimestamp = DateTime.Now.ToUniversalTime(),
                StatusCode = new StatusCode(StatusCodes.Good),
                Value = 0.21
            };
            //Add it to data Value
            historUpdateDataDetails.UpdateValues.Add(dataToHistory);
            var hw = new HistoryWrite();
            hw.UpdateHistory(_connectionChannel.MSession, historUpdateDataDetails);
        }

        /// <summary>
        /// Allows Client Calls (Sample by me)
        /// </summary>
        /// <param name="objectNodeId">Object that provides the method</param>
        /// <param name="methodNodeId">Object that represents the method in the address space</param>
        private static void MethodCall(string objectNodeId, string methodNodeId)
        {
            IList<object> inputParameters = new List<object>(); //TODO: Pass the parameters
            if (inputParameters.Count == 0)
            {
                new MethodCall(_connectionChannel.MSession, objectNodeId, methodNodeId, null);
            }
            else
            {
                new MethodCall(_connectionChannel.MSession, objectNodeId, methodNodeId, inputParameters);
            }

        }

        /// <summary>
        /// Enales a user to write a value for the Value Attribute of a NodeId
        /// This method can be modified to enable a user to write other value attribute
        /// </summary>
        /// <param name="myNodeId">NodeId of a node</param>
        private static void WriteAttribute(string myNodeId)
        {
            var writter = new WriteAttribute();
            writter.WriteAttributes(_connectionChannel.MSession,myNodeId);
        }

        /// <summary>
        /// Read Attribute Value of a Node and After it reads All Attributes of a Node
        /// </summary>
        /// <param name="myNodeId">NodeId of a node</param>
        private static void ReadAttribute(string myNodeId)
        {
            var readVar = new ReadAttribute();
            readVar.ReadValueAttribute(_connectionChannel.MSession, myNodeId);
            readVar.ReadAllAttributes(_connectionChannel.MSession, myNodeId);
        }

        /// <summary>
        /// Reads History of the node with nodeId 
        /// </summary>
        /// <param name="myNodeId">NodeId of a node</param>
        private static void ReadHistory(string myNodeId)
        {
            // For History Read
            var readDetails = new ReadRawModifiedDetails();
            readDetails.StartTime = DateTime.Now.AddHours(-1).ToUniversalTime();
            readDetails.EndTime = DateTime.Now.ToUniversalTime();
            readDetails.NumValuesPerNode = 0;
            readDetails.IsReadModified = false;
            readDetails.ReturnBounds = false;
            var readRawNodeIs = new NodeId(myNodeId);
            new HistoryRead(readRawNodeIs, readDetails);

        }

        /// <summary>
        /// Translate a BrowsePath into a node id
        /// </summary>
        /// <param name="rootNodeId">The nodeId of the root node where the browse path starts</param>
        /// <param name="browsePath">The browse path</param>
        private static void TranslateNodeId(string rootNodeId,string browsePath)
        {
            var myTranslatedPath = new TranslateBrowsePathNames();
            myTranslatedPath.TranslateBrowsePaths(_connectionChannel.MSession, rootNodeId, browsePath);

        }


        private static void AddNode()
        {
            var node = new AddNode(_connectionChannel.MSession);
        }
    }
}
