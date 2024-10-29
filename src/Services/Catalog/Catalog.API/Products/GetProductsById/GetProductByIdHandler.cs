namespace Catalog.API.Products.GetProductsById;

public record GetProductbyIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);
internal class GetProductByIdQueryHandler
    (IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductbyIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductbyIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", query);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        return new GetProductByIdResult(product);
    }
}
