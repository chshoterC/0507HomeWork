using _0507HomeWrok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _0507HomeWrok.Controllers
{
    public class 客戶資料Controller : Controller
    {
        // GET: 客戶資料
        客戶資料Entities db = new 客戶資料Entities();
        public ActionResult Index(string str類型, string str查詢值)
        {
            var all = db.客戶資料.AsQueryable();
            var data = all.OrderBy(p => p.Id);
            if (str類型 != null && str查詢值 != null)
            {
                switch (str類型)
                {
                    case "客戶名稱":
                        data = all.Where(p => p.客戶名稱.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                    case "統一編號":
                        data = all.Where(p => p.統一編號.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                    case "電話":
                        data = all.Where(p => p.電話.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                    case "Email":
                        data = all.Where(p => p.Email.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                }
            }
            return View(data);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶資料 客戶資料Item)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(客戶資料Item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}