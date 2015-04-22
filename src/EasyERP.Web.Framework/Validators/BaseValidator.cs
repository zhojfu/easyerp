namespace EasyERP.Web.Framework.Validators
{
    using FluentValidation;

    public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
    {
        protected BaseValidator()
        {
            PostInitialize();
        }

        protected virtual void PostInitialize()
        {
        }
    }
}