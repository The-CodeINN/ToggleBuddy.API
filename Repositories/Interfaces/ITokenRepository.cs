﻿using Microsoft.AspNetCore.Identity;
using ToggleBuddy.API.Models.Domain;

namespace ToggleBuddy.API.Respositories.Interfaces
{
    public interface ITokenRepository
    {
        // string CreateJWTToken(IdentityUser user, List<string> roles);
        string CreateJWTToken(User user);
    }
}
