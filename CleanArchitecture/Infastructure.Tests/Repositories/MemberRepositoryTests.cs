using Domain.Entities;
using FluentAssertions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;



namespace Infastructure.Tests.Repositories
{
    public class MemberRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly MemberRepository _sut;

        public MemberRepositoryTests()
        {
            _context = GetDatabaseContext().GetAwaiter().GetResult();

            //System Under Test
            _sut = new MemberRepository(_context);
        }

        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Members.CountAsync() <= 0)
            {
                var newMember = Member.Create(
                    new Guid("4379e954-480d-48aa-a6a3-84c25181c6ac"),
                    "whatever@gmail.com",
                    "What",
                    "Ever"
                    );

                if (newMember.IsFailure)
                {
                    throw new InvalidOperationException("Member Repository tests failed");
                }

                databaseContext.Members.Add(newMember.Value);

                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }


        //InMemory Approach
        [Fact]
        public async void GetByIdAsync_ShouldReturnMember()
        {
            //Arrange
            var id = new Guid("4379e954-480d-48aa-a6a3-84c25181c6ac");

            //Act
            var result = await _sut.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Member>();
        }

        //FakeItEasyApproach
        //[Fact]
        //public async void GetByIdAsync_ReturnsMember2()
        //{
        //Arrange
        //            var id = Guid.NewGuid();
        //            CancellationToken cancelationToken = new CancellationToken();
        //            var existingMember = Member.Create(
        //                    id,
        //                    "whatever@gmail.com",
        //                    "What",
        //                    "Ever"
        //                );

        //            if (existingMember.IsFailure)
        //            {
        //                throw new InvalidOperationException("Member Repository Tests Failed");
        //            }
        //;


        //A.CallTo(() => _dbSetMemberMock.FirstOrDefaultAsync(member => member.Id == id, cancelationToken))
        //    .Returns(Task.FromResult(existingMember.Value));

        //IQueryable<Member> fakeIQueryable = new List<Member>().AsQueryable();



        //var fakeDbSet = A.Fake<DbSet<Member>>(d => d
        //        .Implements(typeof(IDbAsyncEnumerable<Member>))
        //        .Implements(typeof(IDbAsyncEnumerator<Member>))
        //        .Implements(typeof(IDbAsyncQueryProvider))
        //        .Implements(typeof(IQueryable<Member>)));

        //A.CallTo(() => ((IQueryable<Member>)fakeDbSet).GetEnumerator())
        //    .Returns(fakeIQueryable.GetEnumerator());
        //A.CallTo(() => ((IQueryable<Member>)fakeDbSet).Provider)
        //    .Returns(fakeIQueryable.Provider);
        //A.CallTo(() => ((IQueryable<Member>)fakeDbSet).Expression)
        //    .Returns(fakeIQueryable.Expression);
        //A.CallTo(() => ((IQueryable<Member>)fakeDbSet).ElementType)
        //  .Returns(fakeIQueryable.ElementType);

        //    A.CallTo(() => ((IQueryable<Member>)fakeDbSet).Provider)
        //     .Returns(new TestDbAsyncQueryProvider<Member>(fakeIQueryable.Provider));

        //    //A.CallTo(() => ((IDbAsyncEnumerable<Member>)fakeDbSet).GetAsyncEnumerator())
        //    //    .Returns(new TestDbAsyncEnumerator<Member>(fakeIQueryable.GetEnumerator()));

        //    var fakeContext = A.Fake<ApplicationDbContext>(x =>
        //        x.WithArgumentsForConstructor(() =>
        //            new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().Options)
        //            )
        //    );

        //    A.CallTo(() => fakeContext.Set<Member>())
        //        .Returns(fakeDbSet);

        //    var repo = new MemberRepository(fakeContext);


        //    //Act
        //    var result = await repo.GetByIdAsync(id);

        //    //Assert
        //    result.Should().NotBeNull();
        //    result.Should().BeOfType<Member>();
        //}
    }
}
