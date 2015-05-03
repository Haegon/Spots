using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ButtonHandlerMain : MonoBehaviour {

	public void Button_Play() {
		StartCoroutine(Wait_Button_Play(0.5f));
	}

	public void Button_Achievement() {
		StartCoroutine(Wait_Button_Achievement(0.5f));
	}

	public void Button_LeaderBoard() {
		StartCoroutine(Wait_Button_LeaderBoard(0.5f));
	}


	public IEnumerator Wait_Button_Play(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		GameMain.Instance.NewGame();
	}

	public IEnumerator Wait_Button_Achievement(float waitTime) {
		yield return new WaitForSeconds(waitTime);
#if !UNITY_EDITOR
		Social.ShowAchievementsUI();
#endif
	}

	public IEnumerator Wait_Button_LeaderBoard(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkItIOIzdQOEAIQAQ");
	}

}
