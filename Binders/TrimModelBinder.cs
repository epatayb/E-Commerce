using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace dotnet_store.Binders
{
    public class TrimModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult != ValueProviderResult.None)
            {
                var value = valueProviderResult.FirstValue;

                if (value != null)
                {
                    bindingContext.Result = ModelBindingResult.Success(value.Trim());
                }
                else
                {
                    bindingContext.Result = ModelBindingResult.Success(null);
                }
            }

            return Task.CompletedTask;
        }
    }
}
