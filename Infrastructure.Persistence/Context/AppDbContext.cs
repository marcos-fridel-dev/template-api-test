using Infrastructure.Persistence.Context.Interceptors;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Context
{
    public partial class AppDbContext : DbContext, IAppDbContext
    {
        public readonly EntityAuditableSaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public AppDbContext(DbContextOptions<AppDbContext> options, EntityAuditableSaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            foreach (Type typeOfModelToCreate in Assembly.GetExecutingAssembly().GetTypes()
                 .Where(mytype => mytype.GetInterfaces().Contains(typeof(IWithOnModelCreating))))
            {
                var modelToCreate = (IWithOnModelCreating)Activator.CreateInstance(typeOfModelToCreate);
                if (modelToCreate != null)
                {
                    modelToCreate.OnCreating(modelBuilder);
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override void Dispose()
        {
        }

    }
}