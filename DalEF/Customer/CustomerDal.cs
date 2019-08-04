using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Csla.Data;

namespace DalEF.Customer
{
    public class CustomerDal:ICustomerDal
    {
        public CustomerDto Fetch(int idCustomer)
        {
            using (var ctx = ObjectContextManager<Customer>.GetManager("Model"))
            {

                var result = (from c in ctx.ObjectContext.Customer
                              where c.IdCustomer.Equals(idCustomer) && c.Enable.Equals(true)
                              select new CustomerDto
                              {
                                  Address = c.Address,
                                  Name = c.Name
                              }).FirstOrDefault();
                //if (result == null)
                //    throw new DataNotFoundException("Customer");
                return result;
            }
        }
        //public List<CustomerDto> Fetch()
        //{
        //    using (var ctx = ObjectContextManager<RTIMEntitiesContainer>.GetManager("RTIMEntities"))
        //    {
        //        var result = from c in ctx.ObjectContext.Customer
        //                     where c.Enable.Equals(true)
        //                     orderby c.Name
        //                     select new CustomerDto
        //                     {
        //                         Address = c.Address,
        //                         Name = c.Name
        //                     };
        //        return result.ToList();
        //    }
        //}
    }
}
