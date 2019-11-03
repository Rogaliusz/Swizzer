using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Swizzer.Shared.Common.Domain.Users.Dto;
using Swizzer.Shared.Common.Exceptions;
using Swizzer.Shared.Common.Extensions;
using Swizzer.Web.Infrastructure.Domain.Users.Models;
using Swizzer.Web.Infrastructure.Framework.Security;

namespace Swizzer.Web.Infrastructure.Services
{
    public interface ISecurityService
    {
        JwtDto GetJwt(UserDto user);
        string GetRandomPassword(int length);
        string GetSalt();
        public string GetHash(string value, string salt);
    }
    public class SecurityService : ISecurityService
    {
        private const int DeriveBytesIterationsCount = 10000;
        private const int SaltSize = 40;

        private readonly SecuritySettings _securitySettings;

        public SecurityService(SecuritySettings securitySettings)
        {
            this._securitySettings = securitySettings;
        }

        public string GetRandomPassword(int length)
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            var chars = new char[length];
            var rd = new Random();

            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        public string GetSalt()
        {
            var random = new Random();
            var saltBytes = new byte[SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string GetHash(string value, string salt)
        {
            if (value.IsEmpty())
            {
                throw new SwizzerServerException(ServerErrorCodes.InvalidParamter,
                    "Can not generate hash from empty value");
            }
            if (salt.IsEmpty())
            {
                throw new SwizzerServerException(ServerErrorCodes.InvalidParamter,
                    "Can not use an empty salt from hashing value.");
            }

            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(salt), DeriveBytesIterationsCount);

            return Convert.ToBase64String(pbkdf2.GetBytes(SaltSize));
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }

        public JwtDto GetJwt(UserDto user)
        {
            var expiresAt = DateTime.UtcNow + _securitySettings.TokenDuration;
            var signCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securitySettings.SecredKey)), SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signCredentials,
                expires: expiresAt
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new JwtDto
            {
                Expires = expiresAt,
                User = user,
                Token = token
            };
        }
    }
}
