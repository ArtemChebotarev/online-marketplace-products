namespace OnlineMarketplace.Products.BL.Dto
{
    public record ProductDto (
        int Id, 
        string Name, 
        string Description, 
        decimal Price, 
        IEnumerable<ProductAttributesDto> Attributes, 
        int SellerId);
}
