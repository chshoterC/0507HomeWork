using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace _0507HomeWrok.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public 客戶聯絡人 Get客戶聯絡人ById(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public void Update(客戶聯絡人 客戶聯絡人Item)
        {
            this.UnitOfWork.Context.Entry(客戶聯絡人Item).State = EntityState.Modified;
        }

        public override void Delete(客戶聯絡人 entity)
        {
            entity.是否刪除 = true;
        }

    }

    public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}