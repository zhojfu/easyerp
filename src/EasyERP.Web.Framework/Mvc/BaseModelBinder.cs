namespace EasyERP.Web.Framework.Mvc
{
    using System.Web.Mvc;

    public class BaseModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            if (model is BaseModel)
            {
                ((BaseModel)model).BindModel(controllerContext, bindingContext);
            }

            return model;
        }
    }
}