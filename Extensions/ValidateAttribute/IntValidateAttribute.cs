using Extensions.ApiContext;
using System.ComponentModel.DataAnnotations;

namespace Extensions.ValidateAttribute;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class IntValidateAttribute : ValidationAttribute
{
    public int MinVal { get; set; }
    public int MaxVal { get; set; }
    public IntValidateAttribute(int minValue = int.MinValue, int maxValue = int.MaxValue)
    {
        MinVal = minValue;
        MaxVal = maxValue;
        ValidRange(MinVal, MaxVal);
    }
    protected override ValidationResult IsValid(object? value, ValidationContext ctx)
    {
        if (value == null) return new ValidationResult($"{ctx.DisplayName}不可为空");
        if ((int)value > MaxVal || (int)value < MinVal) return new ValidationResult($"{ctx.DisplayName}超出设定范围");
        return ValidationResult.Success;
    }
    static void ValidRange(int minVal, int maxVal)
    {
        if (minVal >= maxVal)
            throw new Exception("请正确填写上下限范围");
    }
}


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class StringValidateAttribute : ValidationAttribute
{
    public int MinLen { get; set; }
    public int MaxLen { get; set; }
    public StringValidateAttribute(int minLen = 0, int maxLen = int.MaxValue)
    {
        MinLen = minLen;
        MaxLen = maxLen;
        ValidRange(MinLen, MaxLen);
    }
    protected override ValidationResult IsValid(object? value, ValidationContext ctx)
    {
        if (value == null) return new ValidationResult($"{ctx.DisplayName}不可为空");
        var valLen = value.ToString().Length;
        if (valLen > MaxLen || valLen < MinLen) return new ValidationResult($"{ctx.DisplayName}超出设定长度范围");
        return ValidationResult.Success;
    }
    void ValidRange(int minLen, int maxLen)
    {
        if (minLen < 0 || maxLen > int.MaxValue || maxLen < minLen)
            throw new Exception("请正确填写参数长度范围");
    }
}
