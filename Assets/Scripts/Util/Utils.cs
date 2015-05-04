using UnityEngine;
using System.Collections;

public class Utils {
		
	private static readonly System.Random getrandom = new System.Random();
	private static readonly object syncLock = new object();
	public static int GetRandomNumber(int min, int max)
	{
		lock(syncLock) { // synchronize
			return getrandom.Next(min, max);
		}
	}
}
