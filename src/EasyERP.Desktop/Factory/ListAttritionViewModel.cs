using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyERP.Desktop.Factory
{
    using Caliburn.Micro;

    using EasyERP.Desktop.Contacts;

    using PropertyChanged;

    [ImplementPropertyChanged]
    public class ListAttritionViewModel : Screen, IViewModel
    {
        public override string DisplayName
        {
            get { return "Factory Management"; }
        }

        public string Tag
        {
            get { return "FactoryManagement"; }
        }
    }
}
