using UnityEngine;
using System.Collections;

public class ButtonHandlerInGame : MonoBehaviour {

	public void Button_Spot() {
		if ( GameMain.Instance.m_GameState == GameState.READY ) { 
			if ( GameMain.Instance.m_Count == 0 ) {
				GameMain.Instance.StartGame();
				GameMain.Instance.m_Count ++;
				GameMain.Instance.RePositionSpots();
			}
		}

		if ( GameMain.Instance.m_GameState == GameState.INGAME ) { 
			if ( GameMain.Instance.m_Count % 7 == (int)ColorEx.GetRainbow(this.gameObject.name) ) {
				if ( GameMain.Instance.m_Count % 7 == (int)Rainbow.VIOLET ) {
					GameMain.Instance.m_TimeLimit += 2.0f;
				}
				GameMain.Instance.m_Count ++;
				GameMain.Instance.RePositionSpots();
			} else {
				GameMain.Instance.m_TimeLimit -= 1.0f;
			}
		} 
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
