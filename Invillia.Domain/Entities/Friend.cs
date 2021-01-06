using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;

namespace Invillia.Domain.Entities
{
    public class Friend : Notifiable
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string CellPhone { get; private set; }
        public Guid UserId { get; private set; }
        public bool Active { get; private set; }
        public bool Exclude { get; private set; }

        public virtual User User { get; private set; }
        public virtual ICollection<Loan> Loans { get; set; }

        public Friend(){}

        public Friend(string name, string cellPhone, Guid userId)
        {
            Id = Guid.NewGuid();
            Name = name;
            CellPhone = cellPhone;
            UserId = userId;
            Active = true;
            Exclude = false;

            _ = new ValidationContract()
                    .IsNotNullOrEmpty(Name, "Nome", "Nome não pode ser nulo")
                    .HasMinLen(Name, 3, "Nome", "Nome deve conter no mínimo 3 caracteres")
                    .IsNotNullOrEmpty(CellPhone, "CellPhone", "Número de celular não pode ser nulo")
                    .Notifications;
        }

        public void UpdateInfo(string name, string cellPhone)
        {
            Name = name;
            CellPhone = cellPhone;

            _ = new ValidationContract()
                    .IsNotNullOrEmpty(Name, "Nome", "Nome não pode ser nulo")
                    .HasMinLen(Name, 3, "Nome", "Nome deve conter no mínimo 3 caracteres")
                    .IsNotNullOrEmpty(CellPhone, "CellPhone", "Número de celular não pode ser nulo")
                    .Notifications;
        }


        public void Activate() => Active = true;
        public void Deactivate() => Active = false;
        public void Delete() => Exclude = true;
    }
}
