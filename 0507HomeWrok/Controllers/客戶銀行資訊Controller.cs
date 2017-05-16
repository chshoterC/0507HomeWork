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
        //客戶資料Entities db = new 客戶資料Entities();

        客戶銀行資訊Repository repo = RepositoryHelper.Get客戶銀行資訊Repository();
        客戶資料Repository repo2 = RepositoryHelper.Get客戶資料Repository();

        public ActionResult Index(string str類型, string str查詢值, string str查詢值2)
        {
            var data = repo.Where(p => p.是否刪除 == false).OrderBy(p => p.Id);

            if (str類型 != null && (str查詢值 != null || str查詢值2 != null))
            {
                if (str類型 != "客戶名稱" && str查詢值 != "")
                {
                    switch (str類型)
                    {
                        case "銀行代碼":
                            int qId2 = Convert.ToInt32(str查詢值);
                            data = repo.Where(p => p.銀行代碼 == qId2 && p.是否刪除 == false)
                            .OrderByDescending(p => p.Id);
                            break;
                        case "銀行名稱":
                            data = repo.Where(p => p.銀行名稱.Contains(str查詢值) && p.是否刪除 == false)
                            .OrderByDescending(p => p.Id);
                            break;
                        case "帳戶號碼":
                            data = repo.Where(p => p.帳戶號碼.Contains(str查詢值) && p.是否刪除 == false)
                            .OrderByDescending(p => p.Id);
                            break;
                    }
                }
                else
                {
                    if (str類型 == "客戶名稱" && str查詢值2 != "")
                    {
                        int qId = Convert.ToInt32(str查詢值2);
                        data = repo.Where(p => p.客戶Id == qId && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                    }
                }
            }

            var 客戶資料s = repo2.Where(p => p.是否刪除 == false);

            List<SelectListItem> ddlItem = new List<SelectListItem>();

            foreach (var 客戶資料Item in 客戶資料s)
            {
                ddlItem.Add(new SelectListItem() { Text = 客戶資料Item.客戶名稱, Value = 客戶資料Item.Id.ToString() });
            }
            ddlItem.Insert(0, new SelectListItem() { Text = "請選擇", Value = "" });
            ViewBag.ddl客戶資料 = ddlItem;


            return View(data);
        }


        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo2.Where(p => p.是否刪除 == false), "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶銀行資訊 客戶銀行資訊Item)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶銀行資訊Item);
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repo2.Where(p => p.是否刪除 == false), "Id", "客戶名稱");
            return View(客戶銀行資訊Item);
        }

        public ActionResult Details(int id)
        {
            if (id != null)
            {
                var data = repo.Get客戶銀行資訊ById(id);
                return View(data);
            }

            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            var item = repo.Get客戶銀行資訊ById(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶銀行資訊 客戶銀行資訊Item)
        {
            if (ModelState.IsValid)
            {
                var item = repo.Get客戶銀行資訊ById(id);

                item.帳戶名稱 = 客戶銀行資訊Item.帳戶名稱;
                item.帳戶號碼 = 客戶銀行資訊Item.帳戶號碼;
                item.分行代碼 = 客戶銀行資訊Item.分行代碼;
                item.銀行代碼 = 客戶銀行資訊Item.銀行代碼;
                item.銀行名稱 = 客戶銀行資訊Item.銀行名稱;

                repo.Update(item);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var 客戶銀行資訊Item = repo.Get客戶銀行資訊ById(id);
           
            try
            {
                repo.Delete(客戶銀行資訊Item);
                repo.UnitOfWork.Commit();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
    }
}