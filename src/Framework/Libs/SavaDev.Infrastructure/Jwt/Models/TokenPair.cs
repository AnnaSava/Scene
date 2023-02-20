using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Jwt.Models
{
    public class TokenPair
    {
        public TokenData AuthToken { get; }

        public TokenData RefreshToken { get; }

        public DateTime DateCreated { get; }

        public TokenPair(TokenData authToken, TokenData refreshToken, DateTime dateCreated)
        {
            AuthToken = authToken;
            RefreshToken = refreshToken;
            DateCreated = dateCreated;
        }
    }
}
