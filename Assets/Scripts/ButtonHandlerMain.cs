using UnityEngine;
using System.Collections;

public class ButtonHandlerMain : MonoBehaviour {

	public void Button_Play() {
		StartCoroutine(WaitAndPrint(0.5f));
	}

	public IEnumerator WaitAndPrint(float waitTime) {
		yield return new WaitForSeconds(waitTime);

		GameMain.Instance.NewGame();
	}
}
