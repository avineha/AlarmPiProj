using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlarmPi.Models;
using System.ServiceModel.Syndication;
using System.Xml;

namespace AlarmPi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("AlarmList");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AlarmList()
        {

            return View();
        }

        [HttpPost]
        public JsonResult AlarmLists(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                //Get data from database

                var lstAlarmEntity = GeneralUtility.LoadAlarm();

                int AlarmCount = lstAlarmEntity.AlarmList.Count();
                List<AlarmEntity> oAlarmList = lstAlarmEntity.AlarmList.ToList();
                //_repository.StudentRepository.GetStudents(jtStartIndex, jtPageSize, jtSorting);

                //Return result to jTable
                return Json(new { Result = "OK", Records = oAlarmList.ToList(), TotalRecordCount = AlarmCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        public ActionResult DisplayAlarm()
        {
            var lstAlarmEntity = GeneralUtility.LoadAlarm();

            return View(lstAlarmEntity.AlarmList.ToList());
        }

        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public JsonResult CreateAlarmClock(AlarmEntity oAlarmEntity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }
                AlarmModel oAlarmModel = new AlarmModel();

                AlarmEntity addAlarmEntity = oAlarmModel.AddAlaram(oAlarmEntity, true);

                return Json(new { Result = "OK", Record = addAlarmEntity });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        // GET: Home/Create
        public ActionResult CreateAlarm()
        {


            return View();
        }

        [HttpPost]
        public JsonResult LoadWeek()
        {
            try
            {
                var oList = GeneralUtility.LoadDays();
                var vList = oList.Select(p => new { DisplayText = p, Value = p });

                return Json(new { Result = "OK", Options = vList });

            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }


        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult CreateAlarm(FormCollection collection)
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
        public ActionResult Feed()
        {

            var lstAlarmEntity = GeneralUtility.LoadAlarm();


            List<SyndicationItem> items = new List<SyndicationItem>();

            items.Add(new SyndicationItem("Current DateTime", DateTime.Now.ToString("dd MMM yyyy h:mm:00"), null));

            foreach (var alarmitem in lstAlarmEntity.AlarmList)
            {
                if (alarmitem.IsActive)
                {
                    SyndicationItem Syncitem = new SyndicationItem();
                    Syncitem.Copyright = new TextSyndicationContent(string.Format("{0}-{1}", alarmitem.AlarmDays, alarmitem.AlarmTime));
                    Syncitem.Summary = new TextSyndicationContent(alarmitem.AlarmDesc);
                    items.Add(Syncitem);
                    Syncitem = null;

                }
            }


            SyndicationFeed feed = new SyndicationFeed("Avinash RPi", "http://aviautomation.somee.com/Home/feed", Request.Url, items);


            return new RssActionResult(feed);
        }
    }

    public class RssActionResult : ActionResult
    {
        public SyndicationFeed Feed { get; set; }

        public RssActionResult() { }

        public RssActionResult(SyndicationFeed feed)
        {
            this.Feed = feed;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";

            Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(Feed);
            using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                rssFormatter.WriteTo(writer);
            }
        }
    }
}