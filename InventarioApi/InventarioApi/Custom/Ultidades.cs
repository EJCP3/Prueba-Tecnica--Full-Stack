
using InventarioApi.Models;
using InventarioApi.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;

namespace InventarioApi.Custom
{
    public class Ultidades
    {
        private readonly IConfiguration _configuration;

        public Ultidades(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string encriptarSHA256(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));


                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();

            }
        }

         public string generarJwt(Models.Users modelo)
        {
            var userClaim = new[]
            {
               new Claim(ClaimTypes.NameIdentifier, modelo.ID.ToString()),
               new Claim(ClaimTypes.Email, modelo.Email!),
                new Claim(ClaimTypes.Name, modelo.Nombre!),

            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            var jwtconfig = new JwtSecurityToken(
                claims: userClaim,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
                );
                
            return new JwtSecurityTokenHandler().WriteToken(jwtconfig);
        }
    }
}
