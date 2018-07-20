using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movies.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MoviesView mService = new MoviesView();
            var result = mService.getMaster();
            return View(result);
        }

        public ActionResult Season(String id)
        {
            MoviesView mService = new MoviesView();
            var result = mService.getSeaSon(id);
            return View(result);
        }

        public ActionResult Episode(String id)
        {

            MoviesView mService = new MoviesView();
            var result = mService.getEpisode(id);
            Totaltime tt = new Totaltime();
            var Movies_Total_Time = new TimeSpan(result.Sum(r => r.Time.Ticks));
            tt.total = new TimeSpan(result.Sum(r => r.Time.TimeOfDay.Ticks));
            return View(tt);
        }
        public ActionResult Manage()
        {
          
            return View();
        }
        [HttpPost]
        public JsonResult MasterList()
        {
            MoviesView mService = new MoviesView();
            var result = mService.getMaster();
            var Obj = new
            {
                data = result
            };
            var jsonResult = Json(Obj, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            jsonResult.ContentType = "application/json";
            jsonResult.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            return jsonResult;
          
        }
    }
}