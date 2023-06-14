using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OnlineMarketplace.Products.Api.Infrastructure
{
    public class FromMultiSourceAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource? BindingSource => CompositeBindingSource.Create(
            new[] { BindingSource.Query, BindingSource.Path },
            nameof(FromMultiSourceAttribute));
    }
}
