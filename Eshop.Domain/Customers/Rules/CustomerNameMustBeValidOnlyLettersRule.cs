using System.Text.RegularExpressions;
using Eshop.Domain.SeedWork;

namespace Eshop.Domain.Customers.Rules;

public class CustomerNameMustBeValidOnlyLettersRule : IBusinessRule
{
    private readonly string _customerName;
    private const string NamePattern = "^[A-Za-z]{1,30}$";

    public CustomerNameMustBeValidOnlyLettersRule(string customerName)
    {
        _customerName = customerName;
    }
    
    public bool IsBroken() => !Regex.IsMatch(_customerName, NamePattern);
    public string Message => "The customer's name must be alphabetical, and its length must be between 1 and 30 characters.";
}