using UnityEngine;
using System.Collections;

public class OptionHandler : MonoBehaviour {

	public void Option_BGM() {
		if ( GameMain.Instance.m_GameState == GameState.OPTION ) {
			GameMain.Instance.m_PlayerData.BGM = GameMain.Instance.m_OptionBGM.value.ToInt();
			GameMain.Instance.Save();
		}
	}

	public void Option_Sound() {
		if ( GameMain.Instance.m_GameState == GameState.OPTION ) {
			GameMain.Instance.m_PlayerData.Sound = GameMain.Instance.m_OptionSound.value.ToInt();
			GameMain.Instance.Save();
		}
	}

	public void Button_Cancel() {
		if ( GameMain.Instance.m_GameState == GameState.OPTION ) {
			GameMain.Instance.GoHome();
		}	
	}

}
