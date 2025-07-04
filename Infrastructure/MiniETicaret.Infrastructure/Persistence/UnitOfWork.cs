using MiniETicaret.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaret.Infrastructure.Persistence
{
    public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose() => dbContext.Dispose();
    }
}
