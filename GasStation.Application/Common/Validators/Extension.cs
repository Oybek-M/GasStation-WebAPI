using System.Text;
using FluentValidation.Results;


namespace GasStation.Application.Common.Validators;

public static class Extension
{
    public static string GetErrorMessage(this ValidationResult result)
    {
        var resultMessage = new StringBuilder();
        foreach (var error in result.Errors)
        {
            resultMessage.AppendLine(error.ErrorMessage);
        }

        return resultMessage.ToString();
    }
}
