using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


	public static class ByteIO
	{
		static public bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
		{
			try
			{
				System.IO.FileStream _FileStream = 
					new System.IO.FileStream(_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

				_FileStream.Write(_ByteArray, 0, _ByteArray.Length);
				_FileStream.Close();	

				return true;
			}
			catch (Exception _Exception)
			{
				Debug.LogException(_Exception);
				return false;
			}
		}
		
		static public byte[] FileToByteArray(string pathSource)
		{
			try
			{
				using (FileStream fsSource = new FileStream(pathSource, FileMode.Open, FileAccess.Read))
				{
					byte[] bytes = new byte[fsSource.Length];
					int numBytesToRead = (int)fsSource.Length;
					int numBytesRead = 0;
					while (numBytesToRead > 0)
					{
						int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);
						
						if (n == 0)	break;
						
						numBytesRead += n;
						numBytesToRead -= n;
					}
					numBytesToRead = bytes.Length;
					
					return bytes;
				}
			}
			catch (FileNotFoundException ioEx)
			{
				//Debug.LogException(ioEx);
				
				byte[] bytes = new byte[0];
				
				return bytes;
			}
			
		}

	}