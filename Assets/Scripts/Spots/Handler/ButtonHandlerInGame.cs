using UnityEngine;
using System.Collections;

public class ButtonHandlerInGame : MonoBehaviour {

	public AudioClip rightClip;
	public AudioClip wrongClip;

	void OnClick() {

		if ( this.gameObject.name == "Button_Spot_Gold" ) {
			if ( GameMain.Instance.m_GameState == GameState.READY )
				GameMain.Instance.StartGame();
			GameMain.Instance.GetGold();
			return;
		}

		switch ( GameMain.Instance.m_GameState ) {
		case GameState.READY :
			if ( GameMain.Instance.m_curRainbow == ColorEx.GetRainbow(this.gameObject.name)) {
//				GameMain.Instance.m_Count ++;
				GameMain.Instance.Spot(this.gameObject);
				Camera.main.audio.PlayOneShot(rightClip);
				GameMain.Instance.StartGame();
			} else {
				Camera.main.audio.PlayOneShot(wrongClip);
				GameMain.Instance.WrongSpot();
			}
			break;

		case GameState.INGAME :
			if(GameMain.Instance.isFeverTime)
			{
				GameMain.Instance.Spot(this.gameObject);
				Camera.main.audio.PlayOneShot(rightClip);
				GameMain.Instance.RePositionSpots();
			}
			else if ( GameMain.Instance.m_curRainbow == ColorEx.GetRainbow(this.gameObject.name) ) {
				if ( GameMain.Instance.m_curRainbow == Rainbow.VIOLET ) {
					GameMain.Instance.OneCycle();
				}
				GameMain.Instance.Spot(this.gameObject);
				Camera.main.audio.PlayOneShot(rightClip);
				GameMain.Instance.RePositionSpots();
			} else {
				Camera.main.audio.PlayOneShot(wrongClip);
				GameMain.Instance.WrongSpot();
			}
			break;
		} 
	}
	
	public void Button_Home() {
		if ( GameMain.Instance.m_GameState == GameState.FINISH ||
		    GameMain.Instance.m_GameState == GameState.PAUSE || 
		    GameMain.Instance.m_GameState == GameState.READY) { 
			GameMain.Instance.GoHome();
		}	
	}

	public void Button_Retry() {
		if ( GameMain.Instance.m_GameState == GameState.FINISH ||
		    GameMain.Instance.m_GameState == GameState.PAUSE || 
		    GameMain.Instance.m_GameState == GameState.READY) { 
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
