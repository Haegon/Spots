using UnityEngine;
using System.Collections;

public static class BoolEx
{
	public static int ToInt(this bool b)
	{
		if ( b ) return 1;
		else return 0;
	}
	
	public static bool ToBool(this int i)
	{
		if ( i > 0 ) return true;
		else return false;
	}
}
