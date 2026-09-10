using NCalc;

namespace givPayroll.Services;

public class PayrollFormulaService : IPayrollFormulaService
{
    public decimal Calculate(
        string formula,
        Dictionary<string, object> variables)
    {
        if (string.IsNullOrWhiteSpace(formula))
            return 0;

        var expression = new Expression(formula);

        foreach (var variable in variables)
        {
            expression.Parameters[variable.Key] = variable.Value;
        }

        var result = expression.Evaluate();

        if (result == null)
            return 0;

        return Convert.ToDecimal(result);
    }
}