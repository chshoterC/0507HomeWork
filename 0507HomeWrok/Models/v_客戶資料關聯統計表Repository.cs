using System;
using System.Linq;
using System.Collections.Generic;

using System.Web.Mvc;

namespace _0507HomeWrok.Models
{
    public class v_客戶資料關聯統計表Repository : EFRepository<v_客戶資料關聯統計表>, Iv_客戶資料關聯統計表Repository
    {
        public IQueryable<v_客戶資料關聯統計表> Get客戶資料關聯資料()
        {
            IQueryable<v_客戶資料關聯統計表> all = this.All();
            return all;
        }
    }

    public interface Iv_客戶資料關聯統計表Repository : IRepository<v_客戶資料關聯統計表>
    {

    }
}