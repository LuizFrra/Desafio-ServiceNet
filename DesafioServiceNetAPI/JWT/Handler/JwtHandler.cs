using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DesafioServiceNetAPI.JWT.Keys;
using DesafioServiceNetAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DesafioServiceNetAPI.JWT.Handler
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings jwtSettings;

        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        private SecurityKey securityKey;

        private SigningCredentials signingCredentials;

        private JwtHeader jwtHeader;

        private PrivateKey privateKey;

        private PublicKey publicKey;

        public TokenValidationParameters parameters { get; private set; }

        public JwtHandler(IOptions<JwtSettings> settings, PrivateKey privateKey, PublicKey publicKey)
        {
            this.privateKey = privateKey;
            this.publicKey = publicKey;
            this.jwtSettings = settings.Value;
            InitializeRSA();
            InitializeJwtParameters();

        }

        private void InitializeRSA()
        {

            RSA publicRSA = publicRSA = RSA.Create();
            publicRSA.ImportParameters(publicKey.GetParameters());
            securityKey = new RsaSecurityKey(publicRSA);

            RSA privateRSA = RSA.Create();
            privateRSA.ImportParameters(privateKey.GetParameters());
            var securityKeyRSA = new RsaSecurityKey(privateRSA);
            signingCredentials = new SigningCredentials(securityKeyRSA, SecurityAlgorithms.RsaSha256);
        }

        private void InitializeJwtParameters()
        {
            jwtHeader = new JwtHeader(signingCredentials);
            parameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateActor = false,
                ValidIssuer = jwtSettings.Issuer,
                IssuerSigningKey = securityKey,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateTokenReplay = true
            };
        }

        public JwtToken Create(User user)
        {
            user.Password = null;

            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(jwtSettings.expiryMinutes);
            var centuryBegin = new DateTime(1970, 1, 1);
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var now = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);
            var issuer = jwtSettings.Issuer;
            var payload = new JwtPayload
            {
                { "sub", Convert.ToString(user.UserID) },
                { "user_name", user.Name },
                { "user_id", user.UserID },
                { "iss",  issuer },
                { "iat", now },
                { "nbf", now },
                { "exp", exp },
                { "jti", Guid.NewGuid().ToString("N") }
            };

            var jwt = new JwtSecurityToken(jwtHeader, payload);
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            return new JwtToken { Expires = exp, TokenType = "bearer", Token = token };
        }
    }
}
