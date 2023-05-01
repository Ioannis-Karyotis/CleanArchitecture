﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infastructure;
using Application.Interfaces.Repositories;

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
        public async Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
            await _dbContext
                .Set<Member>()
                .FirstOrDefaultAsync(
                member => member.Email.Value == email, cancellationToken
                );
        public void Add(Member member) =>
            _dbContext
                .Set<Member>()
                .Add(member);

    }
}
