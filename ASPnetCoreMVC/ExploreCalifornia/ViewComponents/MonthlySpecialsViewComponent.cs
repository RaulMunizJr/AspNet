using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExploreCalifornia.ViewComponents//must be ended with viewComponents
{
    [ViewComponent]
    public class MonthlySpecialsViewComponent : ViewComponent
    {
        private readonly BlogDataContext db;

        public MonthlySpecialsViewComponent(BlogDataContext db)//inject to construct
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()//invoke is always looked for                   action
        {
            var specials = db.MonthlySpecials.ToArray();//updated specials on page
            return View(specials); //like a mini-controller                                 view
        }

    }
}
