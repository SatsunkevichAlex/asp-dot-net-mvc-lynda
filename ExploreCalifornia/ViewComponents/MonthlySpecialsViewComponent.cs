using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExploreCalifornia.ViewComponents
{
    [ViewComponent]
    public class MonthlySpecialsViewComponent : ViewComponent
    {
        private readonly BlogDataContext db;

        public MonthlySpecialsViewComponent(BlogDataContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            MonthlySpecial[] specials = db.MonthlySpecials.ToArray();
            return View(specials);
        }
    }
}