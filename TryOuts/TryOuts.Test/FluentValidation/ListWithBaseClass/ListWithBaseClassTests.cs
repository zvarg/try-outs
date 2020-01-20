using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TryOuts.FluentValidation.ListWithBaseClass;

namespace TryOuts.Test.FluentValidation.ListWithBaseClass
{
    public class ListWithBaseClassTests
    {
        [Test]
        public void DerivedBaseClassValidation()
        {
            var dto = new FluentValidationTestClassDto
            {
                Children = new List<FluentValidationTestClassBase>
                {
                    new FluentValidationTestClassA
                    {
                        Name = "ClassA",
                        Age = 18
                    },
                    new FluentValidationTestClassB
                    {
                        Name = "ClassB",
                        PetCount = 3
                    }
                }
            };
            
            var validator = new FluentValidationTestClassDtoValidator();
            var result = validator.Validate(dto);

            foreach (var validationFailure in result.Errors)
            {
                Console.WriteLine(validationFailure.ErrorMessage);
            }

            result.IsValid.Should().BeTrue();
        }
    }
}