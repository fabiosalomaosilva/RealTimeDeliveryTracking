using Domain;
using Microsoft.EntityFrameworkCore;

namespace Postgres;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    { }

    public DbSet<Order> Orders { get; set; }
}

