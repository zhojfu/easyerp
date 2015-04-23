namespace EasyERP.Web.Framework.Mvc
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    [ModelBinder(typeof(BaseModelBinder))]
    public partial class BaseModel
    {
        public BaseModel()
        {
            this.CustomProperties = new Dictionary<string, object>();
            PostInitialize();
        }

        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }

        protected virtual void PostInitialize()
        {
        }

        public Dictionary<string, object> CustomProperties { get; set; }
    }

    public partial class BaseEntityModel : BaseModel
    {
        public virtual int Id { get; set; }
    }
}