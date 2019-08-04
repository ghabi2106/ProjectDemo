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

        public static PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        public static PropertyInfo<string> AddressProperty = RegisterProperty<string>(c => c.Address);
        public string Address
        {
            get { return GetProperty(AddressProperty); }
            private set { LoadProperty(AddressProperty, value); }
        }

        #region Link

        public static PropertyInfo<string> LinkForEditProperty = RegisterProperty<string>(c => c.LinkForEdit);
        public string LinkForEdit
        {
            get { return GetProperty(LinkForEditProperty); }
            set { LoadProperty(LinkForEditProperty, value); }
        }

        public static PropertyInfo<string> LinkForDetailProperty = RegisterProperty<string>(c => c.LinkForDetail);
        public string LinkForDetail
        {
            get { return GetProperty(LinkForDetailProperty); }
            set { LoadProperty(LinkForDetailProperty, value); }
        }

        public static PropertyInfo<string> LinkForDeleteProperty = RegisterProperty<string>(c => c.LinkForDelete);
        public string LinkForDelete
        {
            get { return GetProperty(LinkForDeleteProperty); }
            set { LoadProperty(LinkForDeleteProperty, value); }
        }

        #endregion

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

            LinkForEdit = string.Empty;
            LinkForDetail = string.Empty;
            LinkForDelete = string.Empty;
        }

        public CustomerInfo()
        {
        }

        #endregion

        #region Factory

        public static async Task<CustomerInfo> GetCustomer(int idCustomer)
        {
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

            LinkForEdit = string.Empty;
            LinkForDetail = string.Empty;
            LinkForDelete = string.Empty;
        }


#endif
        #endregion
    }
}
