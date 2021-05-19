    using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModelClassLibrary.area.auth;
using ModelClassLibrary.area.respond;
using ModelClassLibrary.area.model;
using ModelClassLibrary.connection;
using ModelClassLibrary.service;
using ModelClassLibrary.service;

namespace AuthServer.service.impl
{
    public class Auth : IAuth
    {
        private PermissionContext m_context;
        private IOptions<Audience> m_audience;
        private IHashPass m_hashPass;
        private ITranslate m_translate;
        public Auth(PermissionContext context, IOptions<Audience> audience, IHashPass hashPass, ITranslate translate)
        {
            m_context = context;
            m_audience = audience;
            m_hashPass = hashPass;
            m_translate = translate;
        }

        public Users getUser(Users user)
        {
            return m_context.Users
                            .Where(m => m.username == user.username)
                            .FirstOrDefault();
        }
        //check username or password
        private Boolean checkUser(Users us)
        {
            Users user = getUser(us);
            if (user == null)
            {
                return false;
            }
            if (m_hashPass.checkPass(user.password, us.password) == true)
            {
                return true;
            }
            return false;
        }
        public DataRespond login(Users user,string lang)
        {
            DataRespond data = new DataRespond();
            Users us = getUser(user);
            if(us is null)
            {
                data.success = false;
                data.message = m_translate.loadJsonLogin(lang).usernotexist;
                return data;
            }
            if (checkUser(user))
            {
                data.success = true;
                data.data= new { token = genToken(us), user = us };
                data.message = m_translate.loadJsonLogin(lang).loginsuccess;
            }
            else
            {
                data.success = false;
                data.message = m_translate.loadJsonLogin(lang).passwordnotcorrect;
            }
            return data;
        }
        public dynamic genToken(Users user)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.Role,user.role.ToString()),//check quyen

            new Claim(ClaimTypes.NameIdentifier, user.usid.ToString()),
            new Claim(ClaimTypes.Email, user.email),
            new Claim(ClaimTypes.Name, user.username),
            //new Claim(ClaimTypes.Role, user.role.ToString()),//check quyen
        };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(m_audience.Value.Secret));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = m_audience.Value.Iss,
                ValidateAudience = true,
                ValidAudience = m_audience.Value.Aud,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,

            };

            var jwt = new JwtSecurityToken(
                issuer: m_audience.Value.Iss,
                audience: m_audience.Value.Aud,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(1)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var responseJson = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromDays(1).TotalSeconds
            };

            return responseJson;
        }
    }
}
