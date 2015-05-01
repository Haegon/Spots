using UnityEngine;
using System.Collections;

public class ButtonHandlerInGame : MonoBehaviour {

	public void Button_Spot_1() {
		if ( GameMain.Instance.m_GameState == GameState.INGAME ) { 
			GameMain.Instance.m_Count ++;
			GameObject.Find("Button_Spot_1").GetComponent<Spot>().Init();
		}
	}
	public void Button_Spot_2() {
		if ( GameMain.Instance.m_GameState == GameState.INGAME ) { 
			GameMain.Instance.m_Count ++;
			GameObject.Find("Button_Spot_2").GetComponent<Spot>().Init();
		}
	}
	public void Button_Spot_3() {
		if ( GameMain.Instance.m_GameState == GameState.INGAME ) { 
			GameMain.Instance.m_Count ++;
			GameObject.Find("Button_Spot_3").GetComponent<Spot>().Init();
		}	
	}
	public void Button_Home() {
		if ( GameMain.Instance.m_GameState == GameState.FINISH ) { 
			GameMain.Instance.GoHome();
		}	
	}
	public void Button_Retry() {
		if ( GameMain.Instance.m_GameState == GameState.FINISH ) { 
			GameMain.Instance.NewGame();
		}	
	}
}
