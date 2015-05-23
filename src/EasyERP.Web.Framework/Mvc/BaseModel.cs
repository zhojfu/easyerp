namespace EasyERP.Web.Framework.Mvc
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    [ModelBinder(typeof(BaseModelBinder))]
    public class BaseModel
    {
        public BaseModel()
        {
            CustomProperties = new Dictionary<string, object>();
            PostInitialize();
        }

        public Dictionary<string, object> CustomProperties { get; set; }

        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }

        protected virtual void PostInitialize()
        {
        }
    }

    public class BaseEntityModel : BaseModel
    {
        public virtual int Id { get; set; }
    }
}