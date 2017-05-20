using _0507HomeWrok.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using _0507HomeWrok.Views.ViewModel;

namespace _0507HomeWrok.Controllers
{
    public class 客戶資料Controller : Controller
    {
        // GET: 客戶資料

        private v_客戶資料關聯統計表Repository v_repo = RepositoryHelper.Getv_客戶資料關聯統計表Repository();
        private 客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
        private 客戶分類Repository repo2 = RepositoryHelper.Get客戶分類Repository();
        private 客戶聯絡人Repository repo3 = RepositoryHelper.Get客戶聯絡人Repository();

        public ActionResult Index(string str類型, string str查詢值, string str查詢值2, string sort, bool? desc)
        {
            var data = repo.Get客戶資料By條件(str類型, str查詢值, str查詢值2);

            List<SelectListItem> ddlItem = new List<SelectListItem>();
            var 分類s = repo2.Where(p => p.是否刪除 == false);
            foreach (var 分類Item in 分類s)
            {
                ddlItem.Add(new SelectListItem() { Text = 分類Item.分類名稱, Value = 分類Item.Id.ToString() });
            }
            ddlItem.Insert(0, new SelectListItem() { Text = "請選擇", Value = "" });
            ViewBag.str查詢值2 = ddlItem;


            data = repo.客戶資料排序(data, sort, desc);


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

        public ActionResult BatchUpdate(int id)
        {
            ViewData.Model = repo3.Where(p => p.客戶Id == id && p.是否刪除 == false);
            return View();
        }

        [HttpPost]
        public ActionResult BatchUpdate(int? id, IList<批次更新客戶聯絡人VM> items)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var 客戶聯絡人item = repo3.Get客戶聯絡人ById(item.Id);
                    客戶聯絡人item.職稱 = item.職稱;
                    客戶聯絡人item.手機 = item.手機;
                    客戶聯絡人item.電話 = item.電話;
                }
                repo3.UnitOfWork.Commit();

                return RedirectToAction("Details", new { id = id });
            }

            客戶資料 客戶資料data = repo.Get客戶資料ById(id.Value);
            if (客戶資料data == null)
            {
                return HttpNotFound();
            }
            return View("Details", 客戶資料data);
        }
    }
}