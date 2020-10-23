using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Work.Api.Helpers
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if(!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }
            //获得路由“{ids}”的值并转化为字符串
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();
            if(string.IsNullOrWhiteSpace(value))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }
            //获得参数类型（Guid）
            var elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
            //定义转换器
            var converter = TypeDescriptor.GetConverter(elementType);
            //将路由的值转换为object数组
            var values = value.Split( new[] { "," } , StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x.Trim())).ToArray();

            var typeValues = Array.CreateInstance(elementType, value.Length);
            values.CopyTo(typeValues, 0);

            bindingContext.Model = typeValues;
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);

            return Task.CompletedTask;
        }
    }
}
