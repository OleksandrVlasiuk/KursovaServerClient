﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtExampleIdentity.Abstract
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
    }
}
