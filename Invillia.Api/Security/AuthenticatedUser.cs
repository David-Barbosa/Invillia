using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Invillia.Api.Security
{
    public class AuthenticatedUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AuthenticatedUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public Guid GetIdUserIdentity()
        {
            var clains = _accessor.HttpContext.User.Identity as ClaimsIdentity;

            var userid = clains.FindFirst("userId").Value;

            return Guid.Parse(userid);
        }
    }
}
