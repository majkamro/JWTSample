using JWTSample.Database.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWTSample.Services
{
	public class TokenService
	{
		public static string GenerateToken(User user)
		{
			var claims = new List<Claim>() {
				new Claim(ClaimTypes.NameIdentifier, user.UId),
				new Claim(ClaimTypes.Name, user.FullName),
				new Claim(ClaimTypes.Role, user.Role),
				new Claim("SampleClaim", "SampleClaimValue"),
			};

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmnopqrstuvwxyz!@#$%^&*()_+=~ABCDEFGHIJKLMNOPQRSTUVWXYZ"));

			var signCredintial = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

			var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!@#$%^&*()_+=~/-"));

			var encryptionCredintial = new EncryptingCredentials(encryptionKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

			var descriptor = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(claims),
				Audience = "JWTSampleClients",
				Issuer = "JWTSampleServer",
				IssuedAt = DateTime.Now,
				Expires = DateTime.Now.AddDays(1),
				NotBefore = DateTime.Now,
				SigningCredentials = signCredintial,
				EncryptingCredentials = encryptionCredintial,
				CompressionAlgorithm = CompressionAlgorithms.Deflate,
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var securityToken = tokenHandler.CreateToken(descriptor);

			return tokenHandler.WriteToken(securityToken);
		}

        public static string GenerateRefreshToken(User user)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.UId),
                new Claim(ClaimTypes.Name, user.FullName),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmnopqrstuvwxyz!@#$%^&*()_+=~ABCDEFGHIJKLMNOPQRSTUVWXYZ"));

            var signCredintial = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!@#$%^&*()_+=~/-"));

            var encryptionCredintial = new EncryptingCredentials(encryptionKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Audience = "JWTSampleClients",
                Issuer = "JWTSampleServer",
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                NotBefore = DateTime.Now,
                SigningCredentials = signCredintial,
                EncryptingCredentials = encryptionCredintial,
                CompressionAlgorithm = CompressionAlgorithms.Deflate,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
