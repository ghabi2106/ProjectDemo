using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Csla.Data;
using Dal;
using Csla.Data.EF6;

namespace DalEF
{
    public class CustomerDal:ICustomerDal
    {
        public CustomerDto Fetch(int idCustomer)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {

                var result = (from c in ctx.DbContext.Customers
                              where c.IdCustomer.Equals(idCustomer) && c.Enable.Equals(true)
                              select c).FirstOrDefault();
                if (result == null)
                    return null;
                //return result.ConvertAll(cu => EntityToDto(cu));
                var res = EntityToDto(result);
                return res;
            }
        }

        private CustomerDto EntityToDto(Customer c)
        {
            return new CustomerDto
            {
                Id = c.IdCustomer,
                Address = c.Address,
                Name = c.Name
            };
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
