using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IMemberRepository
    {
        Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        void Add(Member member);

    }
}
