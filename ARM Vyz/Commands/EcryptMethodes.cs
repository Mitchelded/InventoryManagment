using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ARM_Vyz.Commands
{
	internal class EcryptMethodes
	{


		string Text { get; set; }

		public string Ecrypt(string text)
		{
			using (MD5 md5 = MD5.Create())
			{

				// Hash the password
				byte[] passwordBytes = Encoding.UTF8.GetBytes(text);
				byte[] hashBytes = md5.ComputeHash(passwordBytes);

				// Convert hash to a hex string (or store as byte array, depending on your database)
				StringBuilder sb = new StringBuilder();
				foreach (byte b in hashBytes)
				{
					sb.Append(b.ToString("x2"));
				}
				return sb.ToString(); // Store the hashed password

			}
		}
	}
}
