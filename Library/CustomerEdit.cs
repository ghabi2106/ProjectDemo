using System;
using System.Linq;
using Csla;
using Csla.Data;
using Csla.Security;
using Csla.Serialization;
using System.ComponentModel;
using System.Configuration;
using System.Threading.Tasks;
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
    }
}
