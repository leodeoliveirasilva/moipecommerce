using MoipTesteEcommerce.Models;
using MoipTesteEcommerce.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace MoipTesteEcommerce.WebHook
{
    class SerializationUtility : IRequestLogger
    {
        private const string REQUEST_DATA_FILEPATH = @"/App_Data/requestdata.xml";

        #region IRequestLogger Members

        public void CaptureRequestData(HttpRequestBase request)
        {
            //load any previuous data from file
            var xDoc = LoadRequestData(HttpContext.Current.Server.MapPath(REQUEST_DATA_FILEPATH));

            //add <Request> element containg data for all the request collections we're interested in
            xDoc.Element("Requests").Add(
                new XElement("Request",
                    CreateXmlFromNameValueCollection("Headers", request.Headers),
                    CreateXmlFromNameValueCollection("QueryString", request.QueryString),
                    CreateXmlFromNameValueCollection("Form", request.Form),
                    XElement.Parse("<InputStream>" + GetContent(request.InputStream) + "</InputStream>")
                )
            );

            // finally save the document
            xDoc.Save(HttpContext.Current.Server.MapPath(REQUEST_DATA_FILEPATH));
        }

        #endregion

        private static string GetContent(Stream stream)
        {
            string documentContents;
            using (Stream receiveStream = stream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    documentContents = readStream.ReadToEnd();
                }
            }
            return documentContents;
        }

        private static XElement CreateXmlFromNameValueCollection(string rootElementName, NameValueCollection nvc)
        {
            XElement element = new XElement(rootElementName);
            for (int i = 0; i < nvc.Count; i++)
            {
                var key = nvc.Keys[i];
                element.Add(
                    new XElement(key, nvc[key])
                );
            }
            return element;
        }

        private static XDocument LoadRequestData(string filepath)
        {
            try
            {
                XDocument xdoc = XDocument.Load(filepath);
                return xdoc;
            }
            catch (FileNotFoundException fnfe)
            {
                return new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("List of received requests"),
                    new XElement("Requests")
                );
            }
        }

        public static IEnumerable<string> GetSavedBeanstalkWebHooks()
        {
            //load all request data
            var xDoc = LoadRequestData(HttpContext.Current.Server.MapPath(REQUEST_DATA_FILEPATH));

            //then find all saved requests that contained an element called "commit" within the posted form
            return from xel in xDoc.Descendants("Request").Descendants("InputStream")
                   select xel.Value;
        }

        public static TesteModels GetChangelistFromJsonString(string json)
        {
            //wrap in a try/catch block
            try
            {
                //create DataContractJsonSerializer for deserializing
                var serializer = new DataContractJsonSerializer(typeof(TesteModels));
                //load form data into memorystream
                MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
                //deserialize
                TesteModels changeList = serializer.ReadObject(ms) as TesteModels;
                //close the stream
                ms.Close();
                //return deserialized instance
                return changeList;
            }
            catch (Exception e)
            {
                return new TesteModels();
            }
        }
    }
}
