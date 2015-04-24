namespace EasyERP.Web.Framework
{
    using EasyErp.Core.Infrastructure;
    using FluentValidation;
    using FluentValidation.Attributes;
    using System;

    public class ValidatorFactory : AttributedValidatorFactory
    {
        public override IValidator GetValidator(Type type)
        {
            if (type == null)
            {
                return null;
            }

            var attribute = (ValidatorAttribute)Attribute.GetCustomAttribute(type, typeof(ValidatorAttribute));
            if (attribute == null || attribute.ValidatorType == null)
            {
                return null;
            }

            var instance = EngineContext.Current.ContainerManager.ResolveUnregistered(attribute.ValidatorType);

            return instance as IValidator;
        }
    }
}