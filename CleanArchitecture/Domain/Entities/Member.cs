using Domain.Primitives;
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

        public FirstName FirstName { get; private set; } = null;
        public LastName LastName { get; private set; } = null;
        public Email Email { get; private set; } = null;

        public static Member Create(
            Guid id,
            Email email,
            FirstName firstName,
            LastName lastName)
        {
            //Any kind of logic that has to do with Entity's creation, goes here!!!

            return new Member(
                id,
                email,
                firstName,
                lastName);
        }
    }
}
