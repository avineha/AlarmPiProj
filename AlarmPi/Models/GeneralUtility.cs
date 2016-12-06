using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

namespace AlarmPi.Models
{
    public static class GeneralUtility
    {
        public static string AlarmXMLName = "Alarm.xml";
        public static string sPath = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Data", AlarmXMLName);

        public static string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }

        public static T DeserializeXMLFileToObject<T>(string XmlFilename)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(XmlFilename)) return default(T);

            StreamReader xmlStream = null;
            XmlSerializer serializer = null;
            try
            {
                xmlStream = new StreamReader(XmlFilename);
                serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(xmlStream);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                serializer = null;

                if (xmlStream != null)
                {
                    xmlStream.Close();
                }

            }
            return returnObject;
        }
        public static Object ObjectToXML(string xml, Type objectType)
        {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(objectType);
                xmlReader = new XmlTextReader(strReader);
                obj = serializer.Deserialize(xmlReader);
            }
            catch (Exception)
            {
                //Handle Exception Code
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                if (strReader != null)
                {
                    strReader.Close();
                }
            }
            return obj;
        }

        public static void AddAlarm(AlarmCollection entity)
        {
            try
            {
                string xml = GetXMLFromObject(entity);
                File.WriteAllText(sPath, xml);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private static AlarmCollection LoadAlarmFromXML(string xml)
        {
            AlarmCollection MyObj = DeserializeXMLFileToObject<AlarmCollection>(xml);

            return MyObj;

        }

        public static AlarmCollection LoadAlarm()
        {
            return LoadAlarmFromXML(sPath) as AlarmCollection;

        }
        public static List<string> LoadDays()
        {
            DateTime now = new DateTime(2016, 9, 5);
            List<string> week = new List<string>();

            for (int i = 0; i < 7; i++)
            {
                week.Add(now.ToString("dddd"));
                now = now.AddDays(1);

            }
            return week;
        }
    }

}