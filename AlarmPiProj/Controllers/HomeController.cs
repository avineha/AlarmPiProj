using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AlarmPiProj.Models;
using System.ServiceModel.Syndication;
using System.Xml;

namespace AlarmPiProj.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            //AlarmModel oAlarmModel = new AlarmModel();
            //var vList = oAlarmModel.lstAlarm.ToList();

            var oAlarmEntity = new List<AlarmEntity>();

            return View(oAlarmEntity.ToList());



        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            var oAlarmEntity = new List<AlarmEntity>();

            return View(oAlarmEntity.ToList());
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                AlarmModel oAlarmModel = new AlarmModel();
                AlarmEntity oAlarmEntity = new AlarmEntity();
                // TODO: Add insert logic here
                //  oAlarmEntity.AlarmId = Convert( Request.Form["AlarmId"];
                oAlarmEntity.AlarmDays = Request.Form["AlarmDays"];
                oAlarmEntity.AlarmDesc = Request.Form["AlarmDesc"];
                oAlarmEntity.AlarmTime = Request.Form["AlarmTime"];
                oAlarmEntity.IsActive = true;
                oAlarmModel.AddAlaram(oAlarmEntity);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
    }


}
