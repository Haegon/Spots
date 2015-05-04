using UnityEngine;
using System;

using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;

	public class Crypto
	{
		byte[] arr0 = new byte[16];
		byte[] arr1 = new byte[16];
		byte[] arr2 = new byte[16];
		byte[] arr3 = new byte[16];
		
		public void Init()
		{
			byte b00 = 	0x76;
			byte b01 = 	0x6C;
			byte b02 = 	0x4A;
			byte b03 = 	0x93;
			byte b04 = 	0xF5;
			byte b05 = 	0x2F;
			byte b06 = 	0xAA;
			byte b07 = 	0x36;
			byte b08 = 	0x77;
			byte b09 = 	0x46;
			byte b10 = 	0xB9;
			byte b11 = 	0x79;
			byte b12 = 	0x58;
			byte b13 = 	0x4A;
			byte b14 = 	0xDA;
			byte b15 = 	0xBA;
			byte b16 = 	0x41;
			byte b17 = 	0x13;
			byte b18 = 	0x53;
			byte b19 = 	0xEA;
			byte b20 = 	0x31;
			byte b21 = 	0x2B;
			byte b22 = 	0x91;
			byte b23 = 	0x96;
			byte b24 = 	0x54;
			byte b25 = 	0x95;
			byte b26 = 	0x86;
			byte b27 = 	0xB8;
			byte b28 = 	0xBC;
			byte b29 = 	0x0B;
			byte b30 = 	0x0A;
			byte b31 = 	0xBB;


			arr0[ 0] = SafeUtils.XorByte(b00); arr0[ 1] = SafeUtils.GetRandomByte();
			arr0[ 2] = SafeUtils.XorByte(b01); arr0[ 3] = SafeUtils.GetRandomByte();
			arr0[ 4] = SafeUtils.XorByte(b02); arr0[ 5] = SafeUtils.GetRandomByte();
			arr0[ 6] = SafeUtils.XorByte(b03); arr0[ 7] = SafeUtils.GetRandomByte();
			arr0[ 8] = SafeUtils.XorByte(b04); arr0[ 9] = SafeUtils.GetRandomByte();
			arr0[10] = SafeUtils.XorByte(b05); arr0[11] = SafeUtils.GetRandomByte();
			arr0[12] = SafeUtils.XorByte(b06); arr0[13] = SafeUtils.GetRandomByte();
			arr0[14] = SafeUtils.XorByte(b07); arr0[15] = SafeUtils.GetRandomByte();
			
			arr1[ 0] = SafeUtils.XorByte(b08); arr1[ 1] = SafeUtils.GetRandomByte();
			arr1[ 2] = SafeUtils.XorByte(b09); arr1[ 3] = SafeUtils.GetRandomByte();
			arr1[ 4] = SafeUtils.XorByte(b10); arr1[ 5] = SafeUtils.GetRandomByte();
			arr1[ 6] = SafeUtils.XorByte(b11); arr1[ 7] = SafeUtils.GetRandomByte();
			arr1[ 8] = SafeUtils.XorByte(b12); arr1[ 9] = SafeUtils.GetRandomByte();
			arr1[10] = SafeUtils.XorByte(b13); arr1[11] = SafeUtils.GetRandomByte();
			arr1[12] = SafeUtils.XorByte(b14); arr1[13] = SafeUtils.GetRandomByte();
			arr1[14] = SafeUtils.XorByte(b15); arr1[15] = SafeUtils.GetRandomByte();
			
			arr2[ 0] = SafeUtils.XorByte(b16); arr2[ 1] = SafeUtils.GetRandomByte();
			arr2[ 2] = SafeUtils.XorByte(b17); arr2[ 3] = SafeUtils.GetRandomByte();
			arr2[ 4] = SafeUtils.XorByte(b18); arr2[ 5] = SafeUtils.GetRandomByte();
			arr2[ 6] = SafeUtils.XorByte(b19); arr2[ 7] = SafeUtils.GetRandomByte();
			arr2[ 8] = SafeUtils.XorByte(b20); arr2[ 9] = SafeUtils.GetRandomByte();
			arr2[10] = SafeUtils.XorByte(b21); arr2[11] = SafeUtils.GetRandomByte();
			arr2[12] = SafeUtils.XorByte(b22); arr2[13] = SafeUtils.GetRandomByte();
			arr2[14] = SafeUtils.XorByte(b23); arr2[15] = SafeUtils.GetRandomByte();
			
			arr3[ 0] = SafeUtils.XorByte(b24); arr3[ 1] = SafeUtils.GetRandomByte();
			arr3[ 2] = SafeUtils.XorByte(b25); arr3[ 3] = SafeUtils.GetRandomByte();
			arr3[ 4] = SafeUtils.XorByte(b26); arr3[ 5] = SafeUtils.GetRandomByte();
			arr3[ 6] = SafeUtils.XorByte(b27); arr3[ 7] = SafeUtils.GetRandomByte();
			arr3[ 8] = SafeUtils.XorByte(b28); arr3[ 9] = SafeUtils.GetRandomByte();
			arr3[10] = SafeUtils.XorByte(b29); arr3[11] = SafeUtils.GetRandomByte();
			arr3[12] = SafeUtils.XorByte(b30); arr3[13] = SafeUtils.GetRandomByte();
			arr3[14] = SafeUtils.XorByte(b31); arr3[15] = SafeUtils.GetRandomByte();

		}
		
		public byte[] GetKey()
		{
			return new byte[] {
				SafeUtils.XorByte(arr0[0]), SafeUtils.XorByte(arr0[ 2]), SafeUtils.XorByte(arr0[ 4]), SafeUtils.XorByte(arr0[ 6]),
				SafeUtils.XorByte(arr0[8]), SafeUtils.XorByte(arr0[10]), SafeUtils.XorByte(arr0[12]), SafeUtils.XorByte(arr0[14]),
				
				SafeUtils.XorByte(arr1[0]), SafeUtils.XorByte(arr1[ 2]), SafeUtils.XorByte(arr1[ 4]), SafeUtils.XorByte(arr1[ 6]),
				SafeUtils.XorByte(arr1[8]), SafeUtils.XorByte(arr1[10]), SafeUtils.XorByte(arr1[12]), SafeUtils.XorByte(arr1[14]),
				
				SafeUtils.XorByte(arr2[0]), SafeUtils.XorByte(arr2[ 2]), SafeUtils.XorByte(arr2[ 4]), SafeUtils.XorByte(arr2[ 6]),
				SafeUtils.XorByte(arr2[8]), SafeUtils.XorByte(arr2[10]), SafeUtils.XorByte(arr2[12]), SafeUtils.XorByte(arr2[14]),
				
				SafeUtils.XorByte(arr3[0]), SafeUtils.XorByte(arr3[ 2]), SafeUtils.XorByte(arr3[ 4]), SafeUtils.XorByte(arr3[ 6]),
				SafeUtils.XorByte(arr3[8]), SafeUtils.XorByte(arr3[10]), SafeUtils.XorByte(arr3[12]), SafeUtils.XorByte(arr3[14]),
			};
		}
		

		
		public byte[] ClientEncypt( string cyper )
		{
			return Encrypt( cyper, GetKey() );
		}

		public string ClientDecrypt(byte[] cyper)
		{
			return Decrypt(cyper, GetKey());
		}
		
		public byte[] Encrypt(string cyper, byte[] key)
		{
			byte[] enctypted;
			using (Aes aes = Aes.Create())
			{
				aes.Mode = CipherMode.ECB;
				aes.Padding = PaddingMode.PKCS7;
				aes.KeySize = 128;
				aes.Key = key;
				aes.IV = System.Convert.FromBase64String("MTIzNDU2NzgxMjM0NTY3OA==");
				
				ICryptoTransform trabs = aes.CreateEncryptor( aes.Key, aes.IV );
				
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, trabs, CryptoStreamMode.Write))
					{		
						//cs.FlushFinalBlock();
						using (StreamWriter sw = new StreamWriter(cs))
						{
							sw.Write(cyper);
						}
						enctypted = ms.ToArray();
					}
				}
			}
			return enctypted;
		}
		
		string Decrypt(byte[] cyper, byte[] key)
		{
			string ret = null;
			using (Aes aes = Aes.Create())
			{
				aes.Mode = CipherMode.ECB;
				aes.Padding = PaddingMode.PKCS7;
				aes.KeySize = 128;
				aes.Key = key;
				aes.IV = System.Convert.FromBase64String("MTIzNDU2NzgxMjM0NTY3OA==");
				
				StringBuilder sb = new StringBuilder();
				for(int i=0; i< key.Length; ++i){
					sb.Append(key[i]);
					if(i!=key.Length-1){
						sb.Append("-");
					}
				}
				//TODO : IV셋팅 필요.
				ICryptoTransform trabs = aes.CreateDecryptor(aes.Key, aes.IV);
				
				using (MemoryStream ms = new MemoryStream(cyper))
				{
					using (CryptoStream cs = new CryptoStream(ms, trabs, CryptoStreamMode.Read))
					{					
						using (StreamReader sr = new StreamReader(cs))
						{
							ret = sr.ReadToEnd();
						}
					}
				}
			}
			return ret.Trim();
		}
	}