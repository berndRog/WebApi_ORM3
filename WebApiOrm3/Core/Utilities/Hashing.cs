using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace WebApiOrm.Core.Utilities;

public static class Hashing {
   
   public static string GenerateSalt() {
      // 128-bit salt using a secure PRNG
      byte[] saltBytes = new byte[16];
      using (var rng = RandomNumberGenerator.Create()) {
         rng.GetBytes(saltBytes);
      }
      return Convert.ToBase64String(saltBytes);
   }
   
   public static string HashPassword(string password, string salt) {
      // derive a 256-bit subkey (use HMACSHA256 with 10000 iterations)
      byte[] saltBytes = Convert.FromBase64String(salt);
      byte[] subkey = KeyDerivation.Pbkdf2(password, saltBytes, KeyDerivationPrf.HMACSHA256, 10000, 32);
      return Convert.ToBase64String(subkey);
   }
   
   public static bool VerifyPassword(string password, string passwordHashed, string salt) {
      // re\-hash the given password using the stored salt and
      // compare it to the stored hashed password
      string hashedInput = HashPassword(password, salt);
      return hashedInput == passwordHashed;
   }
}