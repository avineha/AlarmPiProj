using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace AlarmPiProj.Models
{
    public class DBLib
    {
        /*
        public string connString = ""; 
        public void LoadAllAlarm()
        {

         

            //using (var context = new AlarmContext())
            //{
            //    var vList = context.tblAlarms.ToList();

            //}
        }
        
       public int InsertAlarm(AlarmEntity entity)
       {
           using (var context = new AlarmContext())
           {
               var alarmEntity = context.tblAlarms.Add(entity);
               context.SaveChanges();

               return alarmEntity.AlarmId;
           }
       }
       public int UpdateAlarm(AlarmEntity entity)
       {
           using (var context = new AlarmContext())
           {
               var alarmEntity = context.tblAlarms.Where(p => p.AlarmId.Equals(entity.AlarmId));
               context.tblAlarms.Remove(alarmEntity.SingleOrDefault());
               context.SaveChanges();
               var AlarmId = InsertAlarm(entity);
               context.SaveChanges();

               return AlarmId;
           }
       }
       */
    }


}