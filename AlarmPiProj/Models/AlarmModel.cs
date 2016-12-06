using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace AlarmPiProj.Models
{
    public class AlarmModel
    {
        public List<AlarmEntity> lstAlarm { get; set; }

        public AlarmModel()
        {
            lstAlarm = GeneralUtility.LoadAlarm();
        }

        public void AddAlaram(AlarmEntity entity)
        {
            lstAlarm.Add(entity);
            GeneralUtility.AddAlarm(lstAlarm);
        }

    }
    public class AlarmEntity
    {

        public int AlarmId { get; set; }
        public string AlarmTime { get; set; }
        public string AlarmDays { get; set; }
        public bool IsActive { get; set; }
        public string AlarmDesc { get; set; }
        public string UserId { get; set; }

    }



}