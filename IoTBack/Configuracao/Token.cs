using IOTBack.Model.Empregado;
using IOTBack.Model.Utilizador;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IOTBack.Configuracao
{
    public class Token
    {
        public static object GerarToken(Utilizador dado)
        {
            var key = Encoding.ASCII.GetBytes(Key.Secret);

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                        new Claim("utilizadorId",dado.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandeler = new JwtSecurityTokenHandler();  
            var token = tokenHandeler.CreateToken(tokenConfig);
            var tokenString = tokenHandeler.WriteToken(token);

            return  tokenString;

        }
    }
}
