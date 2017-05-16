using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace _0507HomeWrok.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public void Update(客戶銀行資訊 客戶銀行資訊Item)
        {
            this.UnitOfWork.Context.Entry(客戶銀行資訊Item).State = EntityState.Modified;
        }

        public 客戶銀行資訊 Get客戶銀行資訊ById(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.是否刪除 = true;
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}