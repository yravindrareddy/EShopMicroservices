

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                            .AsNoTracking()
                            .Include(o => o.OrderItems)
                            .AsSplitQuery()
                            .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                            .OrderBy(o => o.OrderName.Value)
                            .ToListAsync(cancellationToken);

            return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
        }
    }
}
