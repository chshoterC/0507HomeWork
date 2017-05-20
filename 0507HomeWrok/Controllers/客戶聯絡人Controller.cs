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
       
        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
        客戶資料Repository repo2 = RepositoryHelper.Get客戶資料Repository();

        public ActionResult Index(string str類型, string str查詢值)
        {
            var data = repo.Where(p => p.是否刪除 == false).OrderBy(p => p.Id);
            if (str類型 != null && str查詢值 != null)
            {
                switch (str類型)
                {
                    case "職稱":
                        data = repo.Where(p => p.職稱.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                    case "姓名":
                        data = repo.Where(p => p.姓名.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                    case "Email":
                        data = repo.Where(p => p.Email.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                    case "手機":
                        data = repo.Where(p => p.手機.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                }
            }
            return View(data);
        }


        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo2.Where(p => p.是否刪除 == false), "Id", "客戶名稱");
            return View();
        }


        [HttpPost]
        public ActionResult Create(客戶聯絡人 客戶聯絡人Item)
        {
            if (ModelState.IsValid)
            {

                repo.Add(客戶聯絡人Item);
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repo2.Where(p => p.是否刪除 == false), "Id", "客戶名稱");
            return View(客戶聯絡人Item);
        }

        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                var data = repo.Get客戶聯絡人ById(id.Value);
                return View(data);
            }

            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            var item = repo.Get客戶聯絡人ById(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶聯絡人 客戶聯絡人Item)
        {
            if (ModelState.IsValid)
            {
                var item = repo.Get客戶聯絡人ById(id);

                item.職稱 = 客戶聯絡人Item.職稱;
                item.姓名 = 客戶聯絡人Item.姓名;
                item.Email = 客戶聯絡人Item.Email;
                item.手機 = 客戶聯絡人Item.手機;
                item.電話 = 客戶聯絡人Item.電話;

                repo.Update(item);
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var 客戶聯絡人Item = repo.Get客戶聯絡人ById(id);

            try
            {
                repo.Delete(客戶聯絡人Item);
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