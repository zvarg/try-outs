using System.Collections.Generic;
using FluentValidation;

namespace TryOuts.FluentValidation.ListWithBaseClass
{
    public abstract class FluentValidationTestClassBase
    {
        public string Name { get; set; }
    }

    public class FluentValidationTestClassBaseValidator :
        // DerivedValidatorBase
        // DerivedValidatorBase<FluentValidationTestClassBase>

        // Normal way
        AbstractValidator<FluentValidationTestClassBase>
    {
        public FluentValidationTestClassBaseValidator()
        {
            // DerivedValidatorBase
            //MapDerivedValidator<FluentValidationTestClassA, FluentValidationTestClassAValidator>();
            //MapDerivedValidator<FluentValidationTestClassB, FluentValidationTestClassBValidator>();

            // Normal way
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            //When(model => model.GetType() == typeof(FluentValidationTestClassA), () =>
            //{
            //    RuleFor(model => ((FluentValidationTestClassA)model).Age)
            //        .GreaterThanOrEqualTo(18);
            //});

            When(model => model.GetType() == typeof(FluentValidationTestClassA),
                () => RuleFor(model => model as FluentValidationTestClassA)
                    .SetValidator(new FluentValidationTestClassAValidator()));

            When(model => model.GetType() == typeof(FluentValidationTestClassB),
                () => RuleFor(model => model as FluentValidationTestClassB)
                    .SetValidator(new FluentValidationTestClassBValidator()));
        }
    }

    public class FluentValidationTestClassA : FluentValidationTestClassBase
    {
        public int Age { get; set; }

    }

    public class FluentValidationTestClassAValidator : AbstractValidator<FluentValidationTestClassA>
    {
        public FluentValidationTestClassAValidator()
        {
            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(18)
                .WithMessage("Age should be greater than or equal to 18");
        }
    }

    public class FluentValidationTestClassB : FluentValidationTestClassBase
    {
        public int PetCount { get; set; }
    }

    public class FluentValidationTestClassBValidator : DerivedValidatorBase<FluentValidationTestClassB>
    {
        public FluentValidationTestClassBValidator()
        {
            RuleFor(x => x.PetCount)
                .LessThan(5)
                .WithMessage("Pet count should be less than 5");
        }
    }
    public class FluentValidationTestClassDto
    {
        public IEnumerable<FluentValidationTestClassBase> Children { get; set; }
    }

    public class FluentValidationTestClassDtoValidator : AbstractValidator<FluentValidationTestClassDto>
    {
        public FluentValidationTestClassDtoValidator()
        {
            // DerivedValidatorBase
            //RuleForEach(x => x.Children)
            //    .NotNull()
            //    .NotEmpty()
            //    .SetValidator(new FluentValidationTestClassBaseValidator());

            // Normal way
            RuleForEach(x => x.Children)
                .NotNull()
                .NotEmpty()
                .SetValidator(new FluentValidationTestClassBaseValidator());
        }
    }
}