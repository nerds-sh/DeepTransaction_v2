using System.Collections.Generic;
using System.Linq;
using Transactions.Core.Exceptions;

namespace Transactions.Core.Validations.Generic.OutputTransaction.Execute
{
    public class WrongLastOutputValidation : IValidation
    {
        private readonly IList<ITransactionStep> _transactionSteps;
        private readonly IHasOutput _transactionOutput;

        public WrongLastOutputValidation(IList<ITransactionStep> transactionSteps, IHasOutput transactionOutput)
        {
            _transactionSteps = transactionSteps;
            _transactionOutput = transactionOutput;
        }

        public void Validate()
        {
            if (!_transactionSteps.Any()) return;
            
            if (_transactionSteps.Last() is IHasOutput lastStepOutput)
            {
                if (lastStepOutput.OutputType != _transactionOutput.OutputType)
                {
                    var message = "The last step output differs from the transaction output. Last step output: " +
                                  $"{lastStepOutput.OutputType}. Transaction output: {_transactionOutput.OutputType}.";

                    throw new WrongSuccessionException(message);
                }
            }
        }
    }
}