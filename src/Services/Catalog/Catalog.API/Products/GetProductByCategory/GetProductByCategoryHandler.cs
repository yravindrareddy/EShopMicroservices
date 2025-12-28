namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category): IQuery<GetProductByCateogoryResult>;

    public record GetProductByCateogoryResult(IEnumerable<Product> Products);

    public class GetProductByCategoryQueryHandler(IDocumentSession session) 
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCateogoryResult>
    {
        public async Task<GetProductByCateogoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var result = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.category))
                .ToListAsync();

            return new GetProductByCateogoryResult(result);
        }
    }
}
