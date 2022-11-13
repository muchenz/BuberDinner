using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Guest.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Entities
{
    public class User: AggregateRoot<UserId>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public DateTime CreatedDatetime { get; }
        public DateTime UpdatedDatetime { get; }

        private User(UserId userId,
            string firstName,
            string lastName,
            string email,
            string password,
            DateTime createdDatetime,
            DateTime updatedDatetime):base(userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            CreatedDatetime = createdDatetime;
            UpdatedDatetime = updatedDatetime;
        }

        public static User Create(UserId userId,
          string firstName,
          string lastName,
          string email,
          string password)
        {
            return new User(UserId.CreateUnique(), firstName, lastName, email, password, DateTime.UtcNow, DateTime.UtcNow);
        }
    }
}