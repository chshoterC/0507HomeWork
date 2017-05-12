using _0507HomeWrok.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _0507HomeWrok.Controllers
{
    public class 客戶銀行資訊Controller : Controller
    {
        //// GET: 客戶銀行資訊
        客戶資料Entities db = new 客戶資料Entities();
        public ActionResult Index(string str類型, string str查詢值, string str查詢值2)
        {
            var all = db.客戶銀行資訊.AsQueryable();
            var data = all.Where(p => p.是否刪除 == false).OrderBy(p => p.Id);
            if (str類型 != null && (str查詢值 != null || str查詢值2 != null))
            {
                if (str查詢值 != "")
                {
                    switch (str類型)
                    {
                        case "銀行代碼":
                            data = all.Where(p => p.銀行代碼 == Convert.ToInt32(str查詢值) && p.是否刪除 == false)
                            .OrderByDescending(p => p.Id);
                            break;
                        case "銀行名稱":
                            data = all.Where(p => p.銀行名稱.Contains(str查詢值) && p.是否刪除 == false)
                            .OrderByDescending(p => p.Id);
                            break;
                        case "帳戶號碼":
                            data = all.Where(p => p.帳戶號碼.Contains(str查詢值) && p.是否刪除 == false)
                            .OrderByDescending(p => p.Id);
                            break;
                    }
                }
                else
                {
                    data = all.Where(p => p.客戶Id == Convert.ToInt32(1) && p.是否刪除 == false)
                    .OrderByDescending(p => p.Id);
                }
            }

            var 客戶資料s = db.客戶資料.AsQueryable().Where(p => p.是否刪除 == false);

            List<SelectListItem> ddlItem = new List<SelectListItem>();

            foreach (var 客戶資料Item in 客戶資料s)
            {
                ddlItem.Add(new SelectListItem() { Text = 客戶資料Item.客戶名稱, Value = 客戶資料Item.Id.ToString() });
            }
            ViewBag.ddl客戶資料 = ddlItem;


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
        public ActionResult Create(客戶銀行資訊 客戶銀行資訊Item)
        {
            if (ModelState.IsValid)
            {
                db.客戶銀行資訊.Add(客戶銀行資訊Item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                var data = db.客戶銀行資訊.Find(id);
                return View(data);
            }

            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            var item = db.客戶銀行資訊.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶銀行資訊 客戶銀行資訊Item)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶銀行資訊.Find(id);

                item.帳戶名稱 = 客戶銀行資訊Item.帳戶名稱;
                item.帳戶號碼 = 客戶銀行資訊Item.帳戶號碼;
                item.分行代碼 = 客戶銀行資訊Item.分行代碼;
                item.銀行代碼 = 客戶銀行資訊Item.銀行代碼;
                item.銀行名稱 = 客戶銀行資訊Item.銀行名稱;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var 客戶銀行資訊Item = db.客戶銀行資訊.Find(id);

            客戶銀行資訊Item.是否刪除 = true;
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