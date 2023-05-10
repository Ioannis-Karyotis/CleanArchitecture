using Application.Interfaces.Repositories;
using Domain.Entities;
using FakeItEasy;
using FluentAssertions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Infastructure.Tests.Repositories
{
    public class MemberRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Member> _dbSetMemberMock;
        private readonly MemberRepository _sut;
        public MemberRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                //.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            //var context = GetDatabaseContext().GetAwaiter().GetResult();
            _context = A.Fake<ApplicationDbContext>(x => x.WithArgumentsForConstructor(() => 
                new ApplicationDbContext(options))
            );
            _dbSetMemberMock = A.Fake<DbSet<Member>>();


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
        //[Fact]
        //public async void GetByIdAsync_ReturnsMember()
        //{
        //    //Arrange
        //    var id = new Guid("4379e954-480d-48aa-a6a3-84c25181c6ac");

        //    //Act
        //    var result = await _sut.GetByIdAsync(id);

        //    //Assert
        //    result.Should().NotBeNull();
        //    result.Should().BeOfType<Member>();
        //}

        //FakeItEasyApproach
        [Fact]
        public async void GetByIdAsync_ReturnsMember2()
        {
            //Arrange
            var id = Guid.NewGuid();
            CancellationToken cancelationToken = new CancellationToken();
            var existingMember = Member.Create(
                    id,
                    "whatever@gmail.com",
                    "What",
                    "Ever"
                );

            if (existingMember.IsFailure)
            {
                throw new InvalidOperationException("Member Repository Tests Failed");
            }

            //A.CallTo(() => _context.Set<Member>())
            //    .Returns(_dbSetMemberMock);

            //A.CallTo(() => _dbSetMemberMock.FirstOrDefaultAsync(member => member.Id == id, cancelationToken))
            //    .Returns(existingMember.Value);
            A.CallTo(() => _dbSetMemberMock.FirstOrDefaultAsync(member => member.Id == id, cancelationToken))
                .Returns(Task.FromResult(existingMember.Value));

            A.CallTo(() => _context.Set<Member>())
                .Returns(_dbSetMemberMock);

            //Act
            var result = await _sut.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Member>();
        }
    }
}
