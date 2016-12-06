using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace AlarmPi.Models
{
    public class AlarmModel
    {
        public AlarmCollection lstAlarm { get; set; }

        public AlarmModel()
        {
            lstAlarm = GeneralUtility.LoadAlarm();
        }

        public void AddAlaram(AlarmEntity entity)
        {
            lstAlarm.AlarmList.Add(entity);
            GeneralUtility.AddAlarm(lstAlarm);
        }

        public AlarmEntity AddAlaram(AlarmEntity entity,bool jtReturn)
        {
            lstAlarm.AlarmList.Add(entity);
            GeneralUtility.AddAlarm(lstAlarm);
            return entity;
        }
    }

    [Serializable]
    [XmlRoot("AlarmCollection")]
    public class AlarmEntity
    {

        public int AlarmId { get; set; }
        public string AlarmTime { get; set; }
        public string AlarmDays { get; set; }
        public bool IsActive { get; set; }
        public string AlarmDesc { get; set; }
        public string UserId { get; set; }

    }

    [Serializable]
    public class AlarmCollection
    {
        [XmlArray("AlarmList"), XmlArrayItem(typeof(AlarmEntity), ElementName = "AlarmEntity")]
        public List<AlarmEntity> AlarmList = new List<AlarmEntity>();

    }

}