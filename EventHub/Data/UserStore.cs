using System;
using System.Linq;
using System.Security.Cryptography;
using EventHub.Models;

namespace EventHub.Data
{
    // User accounts are saved in the SQLite database (see AppDbContext).
    public static class UserStore
    {
        static readonly object Sync = new object();
        const int Iterations = 100000;

        static string Hash(string password, byte[] salt)
        {
            return Convert.ToBase64String(
                Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, 32));
        }

        // Returns the new user, or null if the email is already taken.
        public static User Create(string fullName, string email, string password)
        {
            lock (Sync)
            {
                email = email.Trim();
                using (var db = new AppDbContext())
                {
                    if (db.Users.Any(u => u.Email == email)) return null;

                    byte[] salt = RandomNumberGenerator.GetBytes(16);
                    var user = new User
                    {
                        FullName = fullName.Trim(),
                        Email = email,
                        Salt = Convert.ToBase64String(salt),
                        PasswordHash = Hash(password, salt),
                        CreatedOn = DateTime.Now
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                    return user;
                }
            }
        }

        // Returns the user if email and password match, otherwise null.
        public static User Validate(string email, string password)
        {
            User user;
            string e = (email ?? "").Trim();
            using (var db = new AppDbContext())
            {
                user = db.Users.FirstOrDefault(u => u.Email == e);
            }
            if (user == null) return null;

            byte[] salt = Convert.FromBase64String(user.Salt);
            byte[] expected = Convert.FromBase64String(user.PasswordHash);
            byte[] actual = Rfc2898DeriveBytes.Pbkdf2(password ?? "", salt, Iterations, HashAlgorithmName.SHA256, 32);
            return CryptographicOperations.FixedTimeEquals(expected, actual) ? user : null;
        }
    }
}
