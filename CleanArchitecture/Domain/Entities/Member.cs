using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Member : Entity
    {

        private Member(Guid id) : base(id){ } 

        private Member(
            Guid id,
            Email email,
            FirstName firstName,
            LastName lastName
            ) : base(id)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }

        public static Result<Member> Create(
            Guid id,
            string email,
            string firstName,
            string lastName)
        {
            //Any kind of logic that has to do with Entity's creation, goes here!!!

            var emailResult = Email.Create(email);
            var firstNameResult = FirstName.Create(firstName);
            var lastNameResult = LastName.Create(lastName);

            if (emailResult.IsFailure)
            {
                return Result.Failure<Member>(emailResult.Error); ;
            }

            if (firstNameResult.IsFailure)
            {
                return Result.Failure<Member>(firstNameResult.Error); ;
            }

            if (lastNameResult.IsFailure)
            {
                return Result.Failure<Member>(lastNameResult.Error); ;
            }

            return new Member(
                id,
                emailResult.Value,
                firstNameResult.Value,
                lastNameResult.Value
                );
        }
    }
}
