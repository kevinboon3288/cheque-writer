using Prism.Mvvm;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace CommonModule.ValidationRules;

public class UserInputNotifyDataErrorInfo : BindableBase, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> Errors = new();

    public bool HasErrors => Errors.Count != 0;

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public IEnumerable GetErrors(string propertyName)
    {
        if (!string.IsNullOrEmpty(propertyName) && Errors.TryGetValue(propertyName, out List<string> errors))
        { 
            return errors;
        }

        return null;
    }

    private void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    protected void RemoveError(string propertyName)
    {
        Errors.Remove(propertyName);
        OnErrorsChanged(propertyName);
    }

    protected void AddError(string propertyName, string error)
    {
        Errors.TryAdd(propertyName, new List<string>());

        if (!Errors[propertyName].Any(existError => existError == error))
        {
            Errors[propertyName].Add(error);
            OnErrorsChanged(propertyName);
        }
    }

    protected void Validate(string propertyName, string stringValue)
    {
        RemoveError(propertyName);

        if (string.IsNullOrEmpty(stringValue))
        {
            AddError(propertyName, "Input cannot be empty or null");
            return;
        }
    }

    protected void ValidateEmailFormat(string propertyName, string stringValue)
    {
        RemoveError(propertyName);

        if (string.IsNullOrEmpty(stringValue))
        {
            AddError(propertyName, "Input cannot be empty or null");
            return;
        }

        if (!string.IsNullOrEmpty(stringValue)) 
        {
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            Match match = Regex.Match(stringValue, pattern);
            if (!match.Success)
            {
                AddError(propertyName, "Invalid email format. Please enter the correct format: [test@gmail.com]");
            }
        }
    }
}
