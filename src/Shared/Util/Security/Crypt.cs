#region copyright
// Copyright (c) 2018 Project Agonyl
#endregion

using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Agonyl.Shared.Util.Security
{
	public class Crypt
	{
		private int m_ConstKey1;
		private int m_ConstKey2;
		private int m_DynamicKey;
		private byte p_DynamicKey1;
		private byte p_DynamicKey2;
		private uint p_ConstKey_En;
		private uint p_ConstKey_De;

		public Crypt()
		{
			this.m_ConstKey1 = 0x241AE7;
			this.m_ConstKey2 = 0x15DCB2;
			this.m_DynamicKey = 0x4C478BD;
			this.p_DynamicKey1 = 0x02;
			this.p_DynamicKey2 = 0x01;
			this.p_ConstKey_En = 0xA7F0753B;
			this.p_ConstKey_De = 0xAAF29BF3;
		}

		public Crypt(int ConstKey1, int ConstKey2, int DynamicKey)
		{
			this.m_ConstKey1 = ConstKey1;
			this.m_ConstKey2 = ConstKey2;
			this.m_DynamicKey = DynamicKey;
			this.p_DynamicKey1 = 0x02;
			this.p_DynamicKey2 = 0x01;
			this.p_ConstKey_En = 0xA7F0753B;
			this.p_ConstKey_De = 0xAAF29BF3;
		}

		public Crypt(int ConstKey1, int ConstKey2, int DynamicKey, byte DynamicKey1, byte DynamicKey2, uint ConstKey_En, uint ConstKey_De)
		{
			this.m_ConstKey1 = ConstKey1;
			this.m_ConstKey2 = ConstKey2;
			this.m_DynamicKey = DynamicKey;
			this.p_DynamicKey1 = DynamicKey1;
			this.p_DynamicKey2 = DynamicKey2;
			this.p_ConstKey_En = ConstKey_En;
			this.p_ConstKey_De = ConstKey_De;
		}

		public void Decrypt(ref byte[] buffer)
		{
			int sOffset = 0x0C;
			for (int i = sOffset; i + 4 <= buffer.Length; i += 4)
			{
				int DynamicKey = this.m_DynamicKey;
				for (int j = i; j < i + 4; j++)
				{
					byte pSrc = buffer[j];
					buffer[j] = (byte)(buffer[j] ^ (DynamicKey >> 8));
					DynamicKey = (pSrc + DynamicKey) * this.m_ConstKey1 + this.m_ConstKey2;
				}
			}
		}

		public void Encrypt(ref byte[] buffer)
		{
			int sOffset = 0x0C;
			for (var i = sOffset; i + 4 <= buffer.Length; i += 4)
			{
				int DynamicKey = this.m_DynamicKey;
				for (int j = i; j < i + 4; j++)
				{
					buffer[j] = (byte)(buffer[j] ^ (DynamicKey >> 8));
					DynamicKey = (buffer[j] + DynamicKey) * this.m_ConstKey1 + this.m_ConstKey2;
				}
			}
		}

		/// <summary>
		/// This method is used to convert the plain text to Encrypted/UnReadable Text format.
		/// Reference: http://www.developerin.net/a/56-Encryption-and-Decryption/39-Encryption-and-Decryption-in-Csharp
		/// </summary>
		/// <param name="plainText">Plain Text to Encrypt before transferring over the network.</param>
		/// <param name="key">Key to encrypt</param>
		/// <returns>Cipher Text</returns>
		public string TripleDESEncrypt(string plainText, string key)
		{
			//Getting the bytes of Input String.
			byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(plainText);

			var objMD5CryptoService = new MD5CryptoServiceProvider();

			//Getting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
			byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

			//Deallocating the memory after doing the Job.
			objMD5CryptoService.Clear();

			var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();

			//Assigning the Security key to the TripleDES Service Provider.
			objTripleDESCryptoService.Key = securityKeyArray;

			//Mode of the Crypto service is Electronic Code Book.
			objTripleDESCryptoService.Mode = CipherMode.ECB;

			//Padding Mode is PKCS7 if there is any extra byte is added.
			objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

			var objCrytpoTransform = objTripleDESCryptoService.CreateEncryptor();

			//Transform the bytes array to resultArray
			byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);

			//Releasing the Memory Occupied by TripleDES Service Provider for Encryption.
			objTripleDESCryptoService.Clear();

			//Convert and return the encrypted data/byte into string format.
			return Convert.ToBase64String(resultArray, 0, resultArray.Length);
		}

		/// <summary>
		/// This method is used to convert the Cipher/Encrypted text to Plain Text.
		/// Reference: http://www.developerin.net/a/56-Encryption-and-Decryption/39-Encryption-and-Decryption-in-Csharp
		/// </summary>
		/// <param name="cipherText">Encrypted Text</param>
		/// <param name="key">Key to decrypt</param>
		/// <returns>Plain/Decrypted Text</returns>
		public string TripleDESDecrypt(string cipherText, string key)
		{
			byte[] toEncryptArray = Convert.FromBase64String(cipherText);

			var objMD5CryptoService = new MD5CryptoServiceProvider();

			//Getting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
			byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

			//De-allocating the memory after doing the Job.
			objMD5CryptoService.Clear();

			var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();

			//Assigning the Security key to the TripleDES Service Provider.
			objTripleDESCryptoService.Key = securityKeyArray;

			//Mode of the Crypto service is Electronic Code Book.
			objTripleDESCryptoService.Mode = CipherMode.ECB;

			//Padding Mode is PKCS7 if there is any extra byte is added.
			objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

			var objCrytpoTransform = objTripleDESCryptoService.CreateDecryptor();

			//Transform the bytes array to resultArray
			byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

			//Releasing the Memory Occupied by TripleDES Service Provider for Decryption.          
			objTripleDESCryptoService.Clear();

			//Convert and return the decrypted data/byte into string format.
			return Encoding.UTF8.GetString(resultArray);
		}
	}
}
