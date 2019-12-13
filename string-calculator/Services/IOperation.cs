namespace string_calculator.Services
{
    using System.Collections.Generic;

    public interface IOperation
    {
        int Evaluate(IEnumerable<int> numbersToEvaluate);
    }
}