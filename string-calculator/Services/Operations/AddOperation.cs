namespace string_calculator.Services.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AddOperation : IOperation
    {
        public int Evaluate(IEnumerable<int> numbersToEvaluate)
        {
            if (numbersToEvaluate == null) throw new ArgumentNullException(nameof(numbersToEvaluate));

            return numbersToEvaluate.Sum();
        }
    }
}