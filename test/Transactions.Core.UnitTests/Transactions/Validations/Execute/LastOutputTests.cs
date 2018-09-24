using System;
using Shouldly;
using Transactions.Core.Exceptions;
using Transactions.Core.Transactions;
using Transactions.Core.Transactions.Validations.Transaction.Execute;
using Transactions.Core.UnitTests.Mocks.TransactionSteps;
using Transactions.Core.UnitTests.Mocks.TransactionSteps.Generic;
using Xunit;

namespace Transactions.Core.UnitTests.Transactions.Validations.Execute
{
    public class LastOutputTests : ValidationBaseTests
    {
        private readonly IValidation _instance;

        public LastOutputTests()
        {
            _instance = new LastOutput(Entities);
        }

        [Fact]
        public void Validate_Throws_WhenLastStepIsOutput()
        {
            Entities.Add(new MockOutputTransactionStep<object>());

            Action act = _instance.Validate;

            act.ShouldThrow<WrongSuccessionException>();
        }

        [Fact]
        public void Validate_ThrownExceptionHasMessage_WhenStepIsOutput()
        {
            Entities.Add(new MockOutputTransactionStep<object>());

            Action act = _instance.Validate;

            var actual = act.ShouldThrow<WrongSuccessionException>();
            actual.Message.ShouldNotBeEmpty();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenLastStepIsInput()
        {
            Entities.Add(new MockInputTransactionStep<object>());

            Action act = _instance.Validate;

            act.ShouldNotThrow();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenLastStepIsVoid()
        {
            Entities.Add(new MockTransactionStep());

            Action act = _instance.Validate;

            act.ShouldNotThrow();
        }

        [Fact]
        public void Validate_DoesNotThrow_WhenLastStepIsIO()
        {
            Entities.Add(new MockTransactionStep<object, object>());

            Action act = _instance.Validate;

            act.ShouldNotThrow();
        }
    }
}