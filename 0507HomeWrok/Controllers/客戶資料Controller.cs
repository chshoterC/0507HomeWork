using _0507HomeWrok.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;

namespace _0507HomeWrok.Controllers
{
    public class 客戶資料Controller : Controller
    {
        // GET: 客戶資料

        private v_客戶資料關聯統計表Repository v_repo = RepositoryHelper.Getv_客戶資料關聯統計表Repository();
        private 客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
        private 客戶分類Repository repo2 = RepositoryHelper.Get客戶分類Repository();

        public ActionResult Index(string str類型, string str查詢值)
        {
            var data = repo.Where(p => p.是否刪除 == false).OrderBy(p => p.Id);


             
            if (str類型 != null && str查詢值 != null)
            {
                switch (str類型)
                {
                    case "客戶名稱":
                        data = repo.Where(p => p.客戶名稱.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                    case "統一編號":
                        data = repo.Where(p => p.統一編號.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                    case "電話":
                        data = repo.Where(p => p.電話.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                    case "Email":
                        data = repo.Where(p => p.Email.Contains(str查詢值) && p.是否刪除 == false)
                        .OrderByDescending(p => p.Id);
                        break;
                }
            }
            return View(data);
        }

        public ActionResult 客戶資料關聯統計表()
        {
            var data = v_repo.Get客戶資料關聯資料();
            ViewData.Model = data;
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.分類Id = new SelectList(repo2.Where(p => p.是否刪除 == false), "Id", "分類名稱");
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶資料 客戶資料Item)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶資料Item);
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            ViewBag.分類Id = new SelectList(repo2.Where(p => p.是否刪除 == false), "Id", "分類名稱");
            return View(客戶資料Item);
        }

        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                var data = repo.Get客戶資料ById(id.Value);
                ViewData.Model = data;
                return View();
            }

            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            
            var item = repo.Get客戶資料ById(id);
            ViewBag.分類Id = new SelectList(repo2.Where(p => p.是否刪除 == false), "Id", "分類名稱", item.分類Id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶資料 客戶資料Item)
        {
            if (ModelState.IsValid)
            {
                var item = (客戶資料)repo.Get客戶資料ById(id);

                item.客戶名稱 = 客戶資料Item.客戶名稱;
                item.統一編號 = 客戶資料Item.統一編號;
                item.電話 = 客戶資料Item.電話;
                item.傳真 = 客戶資料Item.傳真;
                item.地址 = 客戶資料Item.地址;
                item.Email = 客戶資料Item.Email;
                item.分類Id = 客戶資料Item.分類Id;

                repo.Update(item);
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            ViewBag.分類Id = new SelectList(repo2.Where(p => p.是否刪除 == false), "Id", "分類名稱", 客戶資料Item.分類Id);
            return View(客戶資料Item);
        }

        public ActionResult Delete(int id)
        {
            var 客戶資料Item = repo.Get客戶資料ById(id);


            客戶資料Item.是否刪除 = true;
            try
            {
                repo.Delete(客戶資料Item);
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