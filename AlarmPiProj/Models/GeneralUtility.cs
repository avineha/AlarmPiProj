using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

namespace AlarmPiProj.Models
{
    public static class GeneralUtility
    {
        public static string AlarmXMLName = "Alarm.xml";
        public static string sPath = Path.Combine(HttpContext.Current.Request.ApplicationPath, "App_Data", AlarmXMLName);

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
            catch (Exception ex)
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
            catch (Exception exp)
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

        public static void AddAlarm(List<AlarmEntity> entity)
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

        private static List<AlarmEntity> LoadAlarmFromXML(string xml)
        {
            Object obj = ObjectToXML(xml, typeof(AlarmEntity));

            if (obj == null)
                obj = new List<AlarmEntity>();

            return obj as List<AlarmEntity>;

        }

        public static List<AlarmEntity> LoadAlarm()
        {
            return LoadAlarmFromXML(sPath) as List<AlarmEntity>;

        }

    }


}