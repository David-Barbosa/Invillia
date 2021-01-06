using Invillia.Api.Security;
using Invillia.Domain.Commands.Handlers;
using Invillia.Domain.Commands.Handlers.FriendHandlers;
using Invillia.Domain.Commands.Handlers.GameHandlers;
using Invillia.Domain.Commands.Handlers.LoanHandlers;
using Invillia.Domain.Interfaces;
using Invillia.Infra.Context;
using Invillia.Infra.Repositories;
using Invillia.Infra.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Invillia.Api.Configurations
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<InvilliaDataContext, InvilliaDataContext>();
            services.AddScoped<AuthenticatedUser, AuthenticatedUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //Repository
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<ILoanRepository, LoanRepository>();
            services.AddTransient<IFriendRepository, FriendRepository>();

            //Commands Handlers
            services.AddTransient<UserCommandHandler, UserCommandHandler>();

            #region Friend
            services.AddTransient<RegisterFriendCommandHandler, RegisterFriendCommandHandler>();
            services.AddTransient<DeleteFriendCommandHandler, DeleteFriendCommandHandler>();
            services.AddTransient<UpdateFriendCommandHandler, UpdateFriendCommandHandler>();
            #endregion

            #region Game
            services.AddTransient<RegisterGameCommandHandler, RegisterGameCommandHandler>();
            services.AddTransient<DeleteGameCommandHandler, DeleteGameCommandHandler>();
            services.AddTransient<UpdateGameCommandHandler, UpdateGameCommandHandler>();
            #endregion

            #region Loan
            services.AddTransient<RegisterLoanCommandHandler, RegisterLoanCommandHandler>();
            services.AddTransient<ReturnLoanCommandHandler, ReturnLoanCommandHandler>(); 
            #endregion
        }
    }
}
