using FluentValidator;
using System;

namespace Invillia.Domain.Entities
{
    public class Loan : Notifiable
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid GameId { get; private set; }
        public Guid FriendId { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime? ReturnDate{ get; private set; }

        public virtual User User { get; private set; }
        public virtual Game Game { get; private set; }
        public virtual Friend Friend { get; private set; }

        public Loan(Guid userId, Guid gameId, Guid friendId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            GameId = gameId;
            FriendId = friendId;
            LoanDate = DateTime.Now;
        }

        public void ReturnLoan() => ReturnDate = DateTime.Now;
    }
}
