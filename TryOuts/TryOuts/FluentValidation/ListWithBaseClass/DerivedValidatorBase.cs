using System.Collections.Generic;
using FluentValidation;

namespace TryOuts.FluentValidation.ListWithBaseClass
{
    /// <summary>
    /// See more information at
    /// https://stackoverflow.com/questions/24948143/fluentvalidation-setcollectionvalidator-for-derived-types/30150819#30150819
    /// </summary>
    /// <typeparam name="TBase"></typeparam>
    public class DerivedValidatorBase<TBase> : AbstractValidator<TBase>
    {
        public void MapDerivedValidator<TType, TValidatorType>()
            where TValidatorType : IEnumerable<IValidationRule>, IValidator<TType>, new()
            where TType : TBase
        {
            When(t => t.GetType() == typeof(TType), AddDerivedRules<TValidatorType>);
        }

        public void MapDerivedValidator<TType, TValidatorType>(TValidatorType validator)
            where TValidatorType : IEnumerable<IValidationRule>, IValidator<TType>
            where TType : TBase
        {
            When(t => t.GetType() == typeof(TType), () => AddDerivedRules(validator));
        }

        private void AddDerivedRules<T>(T validator)
            where T : IEnumerable<IValidationRule>
        {
            foreach (var rule in validator)
            {
                this.AddRule(rule);
            }
        }

        private void AddDerivedRules<T>()
            where T : IEnumerable<IValidationRule>, new()
        {
            IEnumerable<IValidationRule> validator = new T();
            foreach (var rule in validator)
            {
                this.AddRule(rule);
            }
        }
    }
}
