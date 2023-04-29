using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Errors
{
    public static class DomainErrors
    {
        public static class Member
        {
            public static readonly Error AlreadyExists = new (
                "Member.AlreadyExists",
                "The member with this Email already exists.");

            public static readonly Error NotFound = new(
                "Member.NotFound",
                "Member was not found");
        }

        public static class Email
        {
            public static readonly Error Empty = new(
                "Email.Empty",
                "Email is empty.");

            public static readonly Error InvalidFormat = new(
                "Email.InvalidFormat",
                "Email format is invalid.");
        }

        public static class FirstName
        {
            public static readonly Error Empty = new(
                "FirstName.Empty",
                "First name is empty.");

            public static readonly Error TooLong = new(
                "LastName.TooLong",
                "FirstName name is too long.");
        }

        public static class LastName
        {
            public static readonly Error Empty = new(
                "LastName.Empty",
                "Last name is empty.");

            public static readonly Error TooLong = new(
                "LastName.TooLong",
                "Last name is too long.");
        }
    }
}
