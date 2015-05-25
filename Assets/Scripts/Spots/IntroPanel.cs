using UnityEngine;
using System.Collections;

public class IntroPanel : MonoBehaviour {
	
	void OnClick()
	{
		GameMain.Instance.GoHome();
	}
}
