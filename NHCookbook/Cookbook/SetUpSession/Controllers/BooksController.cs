using ActionFilterExample.Models;
using SetUpSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActionFilterExample.Controllers
{
    public class BooksController : Controller
    {

        //[NHibernateSession]
        //public ActionResult Index()
        //{
        //    var books = DataAccessLayer.GetBooks()
        //    .Select(x => new Book
        //    {
        //        Id = x.Id,
        //        Name = x.Name,
        //        Author = x.Author
        //    });
        //    return View(books);
        //}

        [NeedsPersistence]
        public ActionResult Index()
        {
            var books = DataAccessLayer.GetBooks()
 .Select(x => new Book
 {
     Id = x.Id,
     Name = x.Name,
     Author = x.Author
 });
            return View(books);
        }
    }
}