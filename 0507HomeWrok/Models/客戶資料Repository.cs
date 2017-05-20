using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace _0507HomeWrok.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public void Update(客戶資料 客戶資料)
        {
            this.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
        }

        public 客戶資料 Get客戶資料ById(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<客戶資料> Get客戶資料By條件(string str類型, string str查詢值, string str查詢值2)
        {
            var data = this.Where(p => p.是否刪除 == false);

            if (str類型 == "分類" && str查詢值2 != "")
            {
                int 分類Id = Convert.ToInt32(str查詢值2);
                data = this.Where(p => p.分類Id == 分類Id && p.是否刪除 == false)
                .OrderByDescending(p => p.Id);
            }
            else
            {
                if (str類型 != null && str查詢值 != null)
                {
                    switch (str類型)
                    {
                        case "客戶名稱":
                            data = this.Where(p => p.客戶名稱.Contains(str查詢值) && p.是否刪除 == false)
                            .OrderByDescending(p => p.Id);
                            break;
                        case "統一編號":
                            data = this.Where(p => p.統一編號.Contains(str查詢值) && p.是否刪除 == false)
                            .OrderByDescending(p => p.Id);
                            break;
                        case "電話":
                            data = this.Where(p => p.電話.Contains(str查詢值) && p.是否刪除 == false)
                            .OrderByDescending(p => p.Id);
                            break;
                        case "Email":
                            data = this.Where(p => p.Email.Contains(str查詢值) && p.是否刪除 == false)
                            .OrderByDescending(p => p.Id);
                            break;
                    }
                }
            }
            return data;
        }

        public IQueryable<客戶資料> 客戶資料排序(IQueryable<客戶資料> 客戶資料s, string sort, bool? desc)
        {
            var data = 客戶資料s;
            switch (sort)
            {
                case "客戶名稱":
                    if (desc.HasValue && desc.Value)
                    {
                        data = data.OrderByDescending(p => p.客戶名稱);
                    }
                    else
                    {
                        data = data.OrderBy(p => p.客戶名稱);
                    }
                    break;
                case "分類名稱":
                    if (desc.HasValue && desc.Value)
                    {
                        data = data.OrderByDescending(p => p.客戶分類.分類名稱);
                    }
                    else
                    {
                        data = data.OrderBy(p => p.客戶分類.分類名稱);
                    }
                    break;
            }
            return data;
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}