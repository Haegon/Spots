using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	public void Button_Play() {
		Debug.Log("Play");
		Application.LoadLevel(1);
	}
}
