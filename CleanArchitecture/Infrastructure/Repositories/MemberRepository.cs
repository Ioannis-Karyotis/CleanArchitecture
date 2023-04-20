using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Infastructure;

namespace Infrastructure.Repositories
{
    internal sealed class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MemberRepository(ApplicationDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _dbContext
                .Set<Member>()
                .FirstOrDefaultAsync(member => member.Id == id, cancellationToken);

    }
}
