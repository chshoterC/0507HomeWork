using _0507HomeWrok.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _0507HomeWrok.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        ////// GET: 客戶銀行資訊
        客戶資料Entities db = new 客戶資料Entities();
        public ActionResult Index(string str類型, string str查詢值)
        {
            var all = db.客戶聯絡人.AsQueryable();
            var data = all.Where(p => p.是否刪除 == false).OrderBy(p => p.Id);
            if (str類型 != null && str查詢值 != null)
            {
                switch (str類型)
                {
                    case "職稱":
                        data = all.Where(p => p.職稱.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                    case "姓名":
                        data = all.Where(p => p.姓名.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                    case "Email":
                        data = all.Where(p => p.Email.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                    case "手機":
                        data = all.Where(p => p.手機.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                }
            }
            return View(data);
        }


        public ActionResult Create()
        {
            var 客戶資料s = db.客戶資料.AsQueryable().Where(p => p.是否刪除 == false);

            List<SelectListItem> ddlItem = new List<SelectListItem>();

            foreach (var 客戶資料Item in 客戶資料s)
            {
                ddlItem.Add(new SelectListItem() { Text = 客戶資料Item.客戶名稱, Value = 客戶資料Item.Id.ToString() });
            }
            ViewBag.ddl客戶資料 = ddlItem;
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶聯絡人 客戶聯絡人Item)
        {
            if (ModelState.IsValid)
            {
                db.客戶聯絡人.Add(客戶聯絡人Item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                var data = db.客戶聯絡人.Find(id);
                return View(data);
            }

            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            var item = db.客戶聯絡人.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶聯絡人 客戶聯絡人Item)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶聯絡人.Find(id);

                item.職稱 = 客戶聯絡人Item.職稱;
                item.姓名 = 客戶聯絡人Item.姓名;
                item.Email = 客戶聯絡人Item.Email;
                item.手機 = 客戶聯絡人Item.手機;
                item.電話 = 客戶聯絡人Item.電話;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var 客戶聯絡人Item = db.客戶聯絡人.Find(id);

            客戶聯絡人Item.是否刪除 = true;
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