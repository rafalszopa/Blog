using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace Infrastructure.Security.Models
{
    public class PasswordHash : IEquatable<PasswordHash>
    {
        #region Public properites

        public byte[] Bytes { get; }

        public int IterationCount
        {
            get
            {
                return BitConverter.ToInt32(this.Bytes, 4);
            }
        }

        public KeyDerivationPrf Prf 
        {
            get
            {
                return (KeyDerivationPrf)BitConverter.ToInt32(this.Bytes, 0);
            }
        }

        public byte[] Salt 
        {
            get
            {
                byte[] salt = new byte[this.SaltLength];
                Buffer.BlockCopy(this.Bytes, 12, salt, 0, this.SaltLength);

                return salt;
            }
        }

        public int SaltLength 
        { 
            get 
            {
                return BitConverter.ToInt32(this.Bytes, 8);
            } 
        }

        public byte[] Subkey
        {
            get
            {
                int subkeyLength = this.Bytes.Length - (12 + this.SaltLength);

                byte[] subkey = new byte[subkeyLength];
                Buffer.BlockCopy(this.Bytes, 12 + this.SaltLength, subkey, 0, subkeyLength);

                return subkey;
            }
        }

        #endregion
        #region Constructors

        public PasswordHash(byte[] passwordHash)
        {
            this.Bytes = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));

            if (this.SaltLength != this.Salt.Length)
            {
                throw new ArgumentException("Incorrect salt length.");
            }

            if (passwordHash.Length < 12 + this.SaltLength)
            {
                throw new ArgumentException("Incorrect size of password hash.");
            }
        }

        public PasswordHash(KeyDerivationPrf prf, int iterationCount, int saltLength, byte[] salt, byte[] hash)
        {
            if (salt == null)
            {
                throw new ArgumentNullException(nameof(salt));
            }

            if (hash == null)
            {
                throw new ArgumentNullException(nameof(hash));
            }

            if (saltLength != salt.Length)
            {
                throw new ArgumentException($"Incorrect salt length.");
            }

            this.Bytes = AssemblyByteArray(prf, iterationCount, saltLength, salt, hash);
        }

        #endregion
        #region Private methods

        private byte[] AssemblyByteArray(KeyDerivationPrf prf, int iterationCount, int saltLength, byte[] salt, byte[] hash)
        {
            byte[] outputBytes = new byte[12 + saltLength + hash.Length];

            Buffer.BlockCopy(BitConverter.GetBytes((int)prf), 0, outputBytes, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(iterationCount), 0, outputBytes, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(saltLength), 0, outputBytes, 8, 4);
            Buffer.BlockCopy(salt, 0, outputBytes, 12, salt.Length);
            Buffer.BlockCopy(hash, 0, outputBytes, 12 + salt.Length, hash.Length);

            return outputBytes;
        }

        private static bool AreByteAraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            bool areSame = true;

            for (int i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }

            return areSame;
        }

        public override int GetHashCode()
        {
            return this.Bytes.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Equals(obj as PasswordHash);
        }

        public bool Equals(PasswordHash other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return AreByteAraysEqual(this.Bytes, other.Bytes);
        }

        public static bool operator ==(PasswordHash lhs, PasswordHash rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                if (ReferenceEquals(rhs, null))
                {
                    return true;
                }

                return false;
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(PasswordHash lhs, PasswordHash rhs)
        {
            return !(lhs == rhs);
        }

        #endregion
    }
}
