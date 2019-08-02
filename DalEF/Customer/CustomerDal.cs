using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace DalEF.Customer
{
    public class CustomerDal:ICustomerDal
    {
        public List<CustomerDto> Fetch()
        {
            using (var ctx = ObjectContextManager<RTIMEntitiesContainer>.GetManager("RTIMEntities"))
            {
                var result = from c in ctx.ObjectContext.Customer
                             where c.Enable.Equals(true)
                             orderby c.Name
                             select new CustomerDto
                             {
                                 Address = c.Address,
                                 Name = c.Name
                             };
                return result.ToList();
            }
        }
    }
}
