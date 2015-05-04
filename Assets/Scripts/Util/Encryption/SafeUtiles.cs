using UnityEngine;
using System;

	public static class SafeUtils
	{
		public static byte GetRandomByte()
		{
			try
			{
				return System.Convert.ToByte(UnityEngine.Random.Range(0, 255));
			}
			catch(ArgumentException e ) 
			{
				Debug.LogException( e );
				
				return 0;
			}
		}
		
		public static byte XorByte(byte a)
		{
			return System.Convert.ToByte(a ^ 0xc8);
		}
	}
	
	[System.SerializableAttribute]
	public class SafeInt
	{
		[SerializeField]
		byte[] arr1 = new byte[4];
		[SerializeField]
		byte[] arr2 = new byte[4];
		
		public SafeInt(){}
		public SafeInt( int a )
		{
			Set(a);
		}
		
		public int Get()
		{
			var b0 = arr1[0];
			var b1 = arr1[2];
			var b2 = arr2[1];
			var b3 = arr2[3];
			
			var bytes = new byte[4];
			bytes[0] = SafeUtils.XorByte(b0);
			bytes[1] = SafeUtils.XorByte(b1);
			bytes[2] = SafeUtils.XorByte(b2);
			bytes[3] = SafeUtils.XorByte(b3);
			return BitConverter.ToInt32(bytes, 0);
		}
		
		public void Set(int a)
		{
			var bytes = BitConverter.GetBytes(a);
			var b0 = bytes[0];
			var b1 = bytes[1];
			var b2 = bytes[2];
			var b3 = bytes[3];
			
			arr1[0] = SafeUtils.XorByte(b0);
			arr1[1] = SafeUtils.GetRandomByte();
			arr1[2] = SafeUtils.XorByte(b1);
			arr1[3] = SafeUtils.GetRandomByte();
			
			arr2[0] = SafeUtils.GetRandomByte();
			arr2[1] = SafeUtils.XorByte(b2);
			arr2[2] = SafeUtils.GetRandomByte();
			arr2[3] = SafeUtils.XorByte(b3);
		}
	}
	
	public class SafeSingle
	{
		byte[] arr1 = new byte[4];
		byte[] arr2 = new byte[4];
		
		public SafeSingle(){}
		public SafeSingle( float single )
		{
			Set( single );
		}
		
		public Single Get()
		{
			var b0 = arr1[0];
			var b1 = arr1[2];
			var b2 = arr2[1];
			var b3 = arr2[3];
			
			var bytes = new byte[4];
			bytes[0] = SafeUtils.XorByte(b0);
			bytes[1] = SafeUtils.XorByte(b1);
			bytes[2] = SafeUtils.XorByte(b2);
			bytes[3] = SafeUtils.XorByte(b3);
			return BitConverter.ToSingle(bytes, 0);
		}
		
		public void Set(Single a)
		{
			var bytes = BitConverter.GetBytes(a);
			var b0 = bytes[0];
			var b1 = bytes[1];
			var b2 = bytes[2];
			var b3 = bytes[3];
			
			arr1[0] = SafeUtils.XorByte(b0);
			arr1[1] = SafeUtils.GetRandomByte();
			arr1[2] = SafeUtils.XorByte(b1);
			arr1[3] = SafeUtils.GetRandomByte();
			
			arr2[0] = SafeUtils.GetRandomByte();
			arr2[1] = SafeUtils.XorByte(b2);
			arr2[2] = SafeUtils.GetRandomByte();
			arr2[3] = SafeUtils.XorByte(b3);
		}
	}
