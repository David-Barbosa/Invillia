using FluentValidator.Validation;
using System.Text;
using System;
using FluentValidator;

namespace Invillia.Domain.Entities
{
    public class User : Notifiable
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool Active { get; private set; }

        protected User() { }

        public User(string name, string username, string password, string confirmPassword)
        {
            Id = Guid.NewGuid();
            Name = name;
            Username = username;
            Password = EncryptPassword(password);
            Active = true;

            _ = new ValidationContract()
                    .AreEquals(Password, EncryptPassword(confirmPassword), "Password", "As senhas não coincidem")
                    .IsNotNullOrEmpty(Name, "Nome", "Nome não pode ser nulo")
                    .HasMinLen(Name, 3, "Nome", "Nome deve conter no mínimo 3 caracteres")
                    .IsNotNullOrEmpty(Username, "Username", "Usuário não pode ser nulo")
                    .HasMinLen(Username, 3, "Username", "Usuário deve conter no mínimo 3 caracteres")
                    .Notifications;
        }

        public bool Authenticate(string username, string password)
        {
            if (Username == username && Password == EncryptPassword(password))
                return true;

            AddNotification("User", "Usuário ou senha inválidos");
            return false;
        }

        public void Activate() => Active = true;
        public void Deactivate() => Active = false;

        private string EncryptPassword(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";
            var password = (pass += "|2d331cca-f6c0-40c0-bb43-6e32989c2881");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(password));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }

    }
}
