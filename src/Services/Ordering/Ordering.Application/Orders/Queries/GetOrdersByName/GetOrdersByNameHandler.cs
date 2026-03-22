
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                            .AsNoTracking()
                            .Include(o => o.OrderItems)
                            .AsSplitQuery()
                            .Where(o => EF.Functions.Like(o.OrderName.Value, $"%{query.Name}%"))
                            .OrderBy(o => o.OrderName.Value)
                            .ToListAsync(cancellationToken);

            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }
    }
}
