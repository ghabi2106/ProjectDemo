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

        public void Insert(CustomerDto item)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager("Model"))
            {

                var newItem = new Customer
                {
                    Address = item.Address,
                    Name = item.Name
                };

                ctx.DbContext.Customers.Add(newItem);
                ctx.DbContext.SaveChanges();

                item.Id = newItem.IdCustomer;
            }
        }

        public void Update(CustomerDto item)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager("Model"))
            {
                var data = (from c in ctx.DbContext.Customers
                            where (c.IdCustomer.Equals(item.Id)) && (c.Enable.Equals(true))
                            select c).FirstOrDefault();

                if (data == null)
                    throw new DataNotFoundException("Customer");

                data.Address = item.Address;
                data.Name = item.Name;

                var count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new Exception();
            }
        }

        public void Delete(int idCustomer)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager("Model"))
            {
                var data = (from c in ctx.DbContext.Customers
                            where (c.IdCustomer.Equals(idCustomer)) && (c.Enable.Equals(true))
                            select c).FirstOrDefault();

                if (data == null)
                    throw new DataNotFoundException("Customer");

                data.Enable = false;
                ctx.DbContext.SaveChanges();
            }
        }
    }
}
