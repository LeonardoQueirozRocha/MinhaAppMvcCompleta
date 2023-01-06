using DevIO.Business.Models.Base;
using FluentValidation;
using FluentValidation.Results;

namespace DevIO.Business.Services
{
    public abstract class BaseService
    {
        protected void Notify(ValidationResult validationResult)
        {
            validationResult.Errors.ForEach(error => Notify(error.ErrorMessage));
        }

        protected void Notify(string message) { }

        protected bool Validate<TValidator, TEntity>(TValidator validator, TEntity entity)
            where TValidator : AbstractValidator<TEntity> 
            where TEntity : Entity
        {
            var result = validator.Validate(entity);

            if (result.IsValid) return true;

            Notify(result);

            return false;
        }
    }
}
