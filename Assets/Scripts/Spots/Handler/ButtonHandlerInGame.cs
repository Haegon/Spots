using UnityEngine;
using System.Collections;

public class ButtonHandlerInGame : MonoBehaviour {

	public void Button_Spot() {
		switch ( GameMain.Instance.m_GameState ) {
		case GameState.READY :
			if ( GameMain.Instance.m_Count == (int)ColorEx.GetRainbow(this.gameObject.name)) {
				Debug.Log("@@@@@@");
				GameMain.Instance.m_Count ++;
			} else {
				GameMain.Instance.WrongSpot();
			}
			GameMain.Instance.StartGame();
			break;

		case GameState.INGAME :
			if ( GameMain.Instance.m_Count % 7 == (int)ColorEx.GetRainbow(this.gameObject.name) ) {
				if ( GameMain.Instance.m_Count % 7 == (int)Rainbow.VIOLET ) {
					GameMain.Instance.m_TimeLimit += 2.0f;
				}
				GameMain.Instance.m_Count ++;
			} else {
				GameMain.Instance.WrongSpot();
			}
			break;
		} 


		GameMain.Instance.RePositionSpots();	
	}
	
	public void Button_Home() {
		if ( GameMain.Instance.m_GameState == GameState.FINISH ||
		    GameMain.Instance.m_GameState == GameState.PAUSE ) { 
			GameMain.Instance.GoHome();
		}	
	}

	public void Button_Retry() {
		if ( GameMain.Instance.m_GameState == GameState.FINISH ||
		    GameMain.Instance.m_GameState == GameState.PAUSE) { 
			GameMain.Instance.NewGame();
		}	
	}

	public void Button_Pause() {
		if ( GameMain.Instance.m_GameState == GameState.INGAME ||
		    GameMain.Instance.m_GameState == GameState.READY ) { 
			GameMain.Instance.PauseGame();
		}	
	}

	public void Button_Cancel() {
		if ( GameMain.Instance.m_GameState == GameState.PAUSE ) { 
			GameMain.Instance.ResumeGame();
		}	
	}
}
