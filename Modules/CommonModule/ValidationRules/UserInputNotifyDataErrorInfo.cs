﻿using Prism.Mvvm;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace CommonModule.ValidationRules;

public class UserInputNotifyDataErrorInfo : BindableBase, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

    public bool HasErrors => Errors.Count != 0;

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public IEnumerable GetErrors(string propertyName)
    {
        if (!string.IsNullOrEmpty(propertyName) && Errors.TryGetValue(propertyName, out List<string> value))
        { 
            return value;
        }

        return null;
    }

    private void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    protected void ResetError(string propertyName)
    {
        Errors.Remove(propertyName);
        OnErrorsChanged(propertyName);
    }

    protected void AddError(string propertyName, string error)
    {
        Errors.TryAdd(propertyName, new List<string>());

        if (!Errors[propertyName].Contains(error))
        {
            Errors[propertyName].Add(error);
            OnErrorsChanged(propertyName);
        }
    }

    protected void Validate(string propertyName, string stringValue)
    {
        ResetError(propertyName);

        if (stringValue.Length == 0)
        {
            AddError(propertyName, "Input cannot be empty");
        }
    }

    protected void ValidateEmailFormat(string propertyName, string stringValue)
    {
        ResetError(propertyName);

        if (stringValue.Length == 0)
        {
            AddError(propertyName, "Input cannot be empty");
        }
        else 
        {
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
}
