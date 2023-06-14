namespace OnlineMarketplace.Products.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long Price { get; set; }

        public virtual List<ProductAttributes> Attributes { get; set; } = new List<ProductAttributes>();

        public int SellerId { get; set; }

        public void Update(
            string name, 
            string description, 
            decimal price, 
            List<ProductAttributes> attributes)
        {
            Name = name;
            Description = description;
            Price = (long)(price * 100);
            Attributes.Clear();
            Attributes.AddRange(attributes);
        }
    }
}
