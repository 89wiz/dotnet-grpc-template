using gRPCTemplate.Context.Mappings;
using Microsoft.EntityFrameworkCore;

namespace gRPCTemplate.Context;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new ChatRoomMap());
        modelBuilder.ApplyConfiguration(new ChatRoomUserMap());
        modelBuilder.ApplyConfiguration(new ChatMessageMap());
    }
}

/* Commands:
 * Add Migration: dotnet ef migrations add <migration_name> --project gRPCTemplate.Context --startup-project gRPCTemplate.gRPC
 * Update Database: dotnet ef database update --project gRPCTemplate.Context --startup-project gRPCTemplate.gRPC -- --environment <environment>
 */