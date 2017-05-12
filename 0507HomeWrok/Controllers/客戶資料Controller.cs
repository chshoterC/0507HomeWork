using _0507HomeWrok.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
            var data = all.Where(p => p.是否刪除 == false).OrderBy(p => p.Id);
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

        public ActionResult 客戶資料關聯統計表()
        {
            return View(db.v_客戶資料關聯統計表.ToList());
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

        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                var data = db.客戶資料.Find(id);
                return View(data);
            }

            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            var item = db.客戶資料.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶資料 客戶資料Item)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶資料.Find(id);

                item.客戶名稱 = 客戶資料Item.客戶名稱;
                item.統一編號 = 客戶資料Item.統一編號;
                item.電話 = 客戶資料Item.電話;
                item.傳真 = 客戶資料Item.傳真;
                item.地址 = 客戶資料Item.地址;
                item.Email = 客戶資料Item.Email;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var 客戶資料Item = db.客戶資料.Find(id);

            客戶資料Item.是否刪除 = true;
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
    }
}