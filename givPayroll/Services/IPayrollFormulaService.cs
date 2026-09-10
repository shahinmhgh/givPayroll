public interface IPayrollFormulaService
{
    decimal Calculate(
        string formula,
        Dictionary<string, object> variables);
}