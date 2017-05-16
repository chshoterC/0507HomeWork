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

    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}