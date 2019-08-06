using System;
using System.Linq;
using Csla;
using Csla.Data;
using Csla.Security;
using Csla.Serialization;
using System.ComponentModel;
using System.Configuration;
using System.Threading.Tasks;
using Csla.Configuration;
#if !SILVERLIGHT
using Dal;
#endif

namespace Library
{
    [Serializable()]
    public class CustomerEdit : BusinessBase<CustomerEdit>
    {
        // When a CustomerEdit is created, first thing is to set it AdminId

        #region Business Properties

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> AddressProperty = RegisterProperty<string>(c => c.Address);
        public string Address
        {
            get { return GetProperty(AddressProperty); }
            set { SetProperty(AddressProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }
        #endregion

        #region Factory

        public static CustomerEdit NewCustomer()
        {
            return DataPortal.Create<CustomerEdit>();
        }

        public static async Task<CustomerEdit> NewCustomerAsync()
        {
            return await DataPortal.CreateAsync<CustomerEdit>();
        }

        public static CustomerEdit GetCustomer(int id)
        {
            return DataPortal.Fetch<CustomerEdit>(id);
        }

        public static async Task<CustomerEdit> GetCustomerAsync(int id)
        {
            return await DataPortal.FetchAsync<CustomerEdit>(id);
        }

        public static async Task<CustomerEdit> GetCustomerByNameAsync(string name)
        {
            return await DataPortal.FetchAsync<CustomerEdit>(name);
        }

        public static void DeleteCustomer(int id)
        {
            DataPortal.Delete<CustomerEdit>(id);
        }

        public static async Task DeleteCustomerAsync(int id)
        {
            await DataPortal.DeleteAsync<CustomerEdit>(id);
        }

        public static async Task<CustomerEdit> GetExistingCustomerAsync(int customerId)
        {
            return await DataPortal.FetchAsync<CustomerEdit>(new Criteria { CustomerId = customerId });
        }

        #endregion

        #region Base

        //protected override void DataPortal_Create()
        //{
        //    base.DataPortal_Create();
        //}

        private void DataPortal_Fetch(int id)
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<ICustomerDal>();
                var data = dal.Fetch(id);
                if (data == null)
                    return;
                using (BypassPropertyChecks)
                {
                    Address = data.Address;
                    Id = data.Id;
                    Name = data.Name;
                }
            }
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<ICustomerDal>();
                using (BypassPropertyChecks)
                {
                    var item = new CustomerDto
                    {
                        Address = this.Address,
                        Name = this.Name
                    };

                    dal.Insert(item);

                    Id = item.Id;
                }
                //FieldManager.UpdateChildren(this);
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<ICustomerDal>();
                using (BypassPropertyChecks)
                {
                    var item = new CustomerDto
                    {
                        Address = this.Address,
                        Id = this.Id,
                        Name = this.Name
                    };

                    dal.Update(item);
                }

                FieldManager.UpdateChildren(this);
            }
        }

        //private void DataPortal_Delete(int id)
        //{
        //    DataPortal_Delete(id);
        //}
        #endregion

        #region Criteria

        [Serializable]
        public class Criteria : CriteriaBase<Criteria>
        {
            public static readonly PropertyInfo<int> CustomerIdProperty = RegisterProperty<int>(c => c.CustomerId);
            public int CustomerId
            {
                get { return ReadProperty(CustomerIdProperty); }
                set { LoadProperty(CustomerIdProperty, value); }
            }
        }

        #endregion
    }
}
