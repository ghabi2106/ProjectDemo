using System;
using Csla;
using Csla.Serialization;
using System.Configuration;
using System.Threading.Tasks;
#if !SILVERLIGHT
using Dal;
#endif

namespace Library
{
    [Serializable]
    public class CustomerInfo : ReadOnlyBase<CustomerInfo>
    {
        #region Properties

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> AddressProperty = RegisterProperty<string>(c => c.Address);
        public string Address
        {
            get { return GetProperty(AddressProperty); }
            private set { LoadProperty(AddressProperty, value); }
        }

        //public override string ToString()
        //{
        //    return CompanyName;
        //}

        #endregion

        #region Constructor

        public CustomerInfo(CustomerEdit item)
        {
            Address = item.Address;
            Name = item.Name;
            Id = item.Id;
        }

        public CustomerInfo()
        {
        }

        public CustomerInfo(int id)
        {
            Id = id;
        }

        #endregion

        #region Factory

        public static CustomerInfo GetCustomer(int idCustomer)
        {
            var t = DataPortal.Fetch<CustomerInfo>(idCustomer);
            return DataPortal.Fetch<CustomerInfo>(idCustomer);
        }

        public static async Task<CustomerInfo> GetCustomerAsync(int idCustomer)
        {
            //var t = DataPortal.FetchAsync<CustomerInfo>(idCustomer);
            return await DataPortal.FetchAsync<CustomerInfo>(idCustomer);
        }

        #endregion

        #region Data

#if !SILVERLIGHT


        private void DataPortal_Fetch(int idCustomer)
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<ICustomerDal>();
                CustomerDto customer = dal.Fetch(idCustomer);

                if (customer != null)
                {
                    Id = customer.Id;
                    Name = customer.Name;
                    Address = customer.Address;
                }
            }
        }

        private void Child_Fetch(CustomerDto item)
        {
            Address = item.Address;
            Name = item.Name;
            Id = item.Id;
        }


#endif
        #endregion
    }
}
