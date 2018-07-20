using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Movies.database;

namespace Movies.Models
{
    public class Totaltime
    {
        public TimeSpan total { get; set; }
    }
    public class MoviesView
    {
        public int Movies_ID { get; set; }
        public String Movies_Name { get; set; }
        public String Movies_Time { get; set; }
        public DateTime Time { get; set; }
        public String Movies_Image { get; set; }

        //public String Movies_Name { get; set; }
        public List<MoviesView> getMovies()
        {
            List<MoviesView> mResult = null;
            using (var db = new MoviesEntities())
            {
                var result = db.SP_GET_MOVIES().ToList();
                mResult = result.Select(s => new MoviesView
                {
                    Movies_Name = s.EPName,
                    Movies_Time = s.EPTime,

                }).ToList();
            }
            return mResult;
        }
        public List<MoviesView> getMaster()
        {
            List<MoviesView> mResult = null;
            using (var db = new MoviesEntities())
            {
                var result = db.Master.ToList();
                mResult = result.Select(s => new MoviesView
                {
                    Movies_ID = s.id,
                    Movies_Name = s.Name,
                    Movies_Image = String.Format("Content/MasterImage/{0}", s.Image),
                    Movies_Time = s.CreatedOn.HasValue ? s.CreatedOn.Value.ToShortDateString() : "-"
                }).ToList();
            }
            return mResult;
        }
        public List<MoviesView> getSeaSon(String id)
        {
            List<MoviesView> mResult = new List<MoviesView>();
            if (id != null)
            {
                using (var db = new MoviesEntities())
                {
                    var result = db.Season.ToList();
                    mResult = result.Where(w => w.idMaster == int.Parse(id)).Select(s => new MoviesView
                    {
                        Movies_ID = s.id,
                        Movies_Name = s.Name,
                        Movies_Image = String.Format("/Content/SeasonImage/{0}", s.Image)
                    }).ToList();
                }
            }
            return mResult;
        }
        public List<MoviesView> getEpisode(String id)
        {
            List<MoviesView> mResult = new List<MoviesView>();
            if (id != null)
            {
                using (var db = new MoviesEntities())
                {
                    var result = db.Episode.ToList();
                    mResult = result.Where(w => w.idSeason == int.Parse(id)).Select(s => new MoviesView
                    {
                        Movies_ID = s.id,
                        Movies_Name = s.Name,
                        Movies_Image = String.Format("/Content/SeasonImage/{0}", s.Image),
                        Movies_Time = s.Time,
                        Time = StringToTime(s.Time)
                    }).ToList();
                }
            }

            return mResult;
        }
        private DateTime StringToTime(string time)
        {
            DateTime dateTime = DateTime.ParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture);
            return dateTime;
        }
    }

}