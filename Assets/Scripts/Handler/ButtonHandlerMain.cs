using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ButtonHandlerMain : MonoBehaviour {

	public void Button_Main() {
		if ( GameMain.Instance.m_GameState != GameState.HOME ) return;

		switch (this.gameObject.name) {
		case "Button_Play" :
			GameMain.Instance.NewGame();
			break;
		case "Button_Option" :
			GameMain.Instance.Option();
			break;
		case "Button_Acheivment" :
			#if !UNITY_EDITOR
			Social.ShowAchievementsUI();
			#endif
			break;
		case "Button_LeaderBoard" :
			#if !UNITY_EDITOR
			PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkItIOIzdQOEAIQAQ");
			#endif
			break;
		case "Button_Help" :
			GameMain.Instance.NewGame();
			break;
		case "Button_Share" :
			GameMain.Instance.NewGame();
			break;
		}
	}
}
