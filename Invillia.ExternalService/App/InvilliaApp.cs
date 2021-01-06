using Invillia.ExternalService.Models;
using Invillia.ExternalService.Utils;
using System;

namespace Invillia.ExternalService.App
{
    public class InvilliaApp
    {
        public static void InitializeConfig(string urlPortal)
        {
            try
            {
                InvilliaService.InitializeConfig(urlPortal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static UserLoginModelLib Login(string username, string password)
        {
            try
            {
                var saida = InvilliaService.ExecuteRequestLogin<UserLoginModelLib>(ServiceUrls.LOGIN, username, password);
                return saida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
