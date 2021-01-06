using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;

namespace Invillia.Domain.Entities
{
    public class Game : Notifiable
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool Available { get; private set; }
        public Guid UserId { get; private set; }
        public bool Active { get; private set; }
        public bool Exclude { get; private set; }

        public virtual User User { get; private set; }

        public virtual ICollection<Loan> Loans { get; set; }

        public Game(string name, Guid userId)
        {
            Id = Guid.NewGuid();
            Name = name;
            UserId = userId;
            Available = true;
            Active = true;
            Exclude = false;

            _ = new ValidationContract()
                   .IsNotNullOrEmpty(Name, "Nome", "Nome não pode ser nulo")
                   .HasMinLen(Name, 3, "Nome", "Nome deve conter no mínimo 3 caracteres")
                   .Notifications;
        }

        public void UpdateInfo(string name)
        {
            Name = name;

            _ = new ValidationContract()
                  .IsNotNullOrEmpty(Name, "Nome", "Nome não pode ser nulo")
                  .HasMinLen(Name, 3, "Nome", "Nome deve conter no mínimo 3 caracteres")
                  .Notifications;
        }

        public void Activate() => Active = true;
        public void Deactivate() => Active = false;

        public void AvailableLoan() => Available = true;
        public void NotAvailableLoan() => Available = false;

        public void Delete() => Exclude = true;
    }
}
