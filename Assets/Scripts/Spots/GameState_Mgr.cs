//using UnityEngine;
//using System.Collections;
//
//public class GameState_Mgr : MonoBehaviour {
//
//	public static GameState GetGameState(string gameObjectName) {
//		
//		switch (gameObjectName) {
//		case "Button_Play" :
//			return GameState.HOME;
//			break;
//		case "Button_Option" :
//			return GameState.HOME;
//			break;
//		case "Button_Help" :
//			return GameState.HOME;
//			break;
//		case "Button_Share" :
//			return GameState.HOME;
//			break;
//		case "Button_Acheivment" :
//			return GameState.HOME;
//			break;
//		case "Button_LeaderBoard" :
//			return GameState.;
//			break;
//		default :
//			return GameState.NONE;
//			break;
//		}
//	}
//	
//	public static Rainbow GetRainbow(string gameObjectName){
//		
//		switch (gameObjectName) {
//		case "Button_Spot_R" :
//			return Rainbow.RED;
//			break;
//		case "Button_Spot_O" :
//			return Rainbow.ORANGE;
//			break;
//		case "Button_Spot_Y" :
//			return Rainbow.YELLOW;
//			break;
//		case "Button_Spot_G" :
//			return Rainbow.GREEN;
//			break;
//		case "Button_Spot_B" :
//			return Rainbow.BLUE;
//			break;
//		case "Button_Spot_I" :
//			return Rainbow.INDIGO;
//			break;
//		case "Button_Spot_V" :
//			return Rainbow.VIOLET;
//			break;
//		default :
//			return Rainbow.NONE;
//			break;
//		}
//	}
//	
//	public static Color GetColor(Rainbow rainbow){
//		
//		switch (rainbow) {
//		case Rainbow.RED :
//			return Red;
//			break;
//		case Rainbow.ORANGE :
//			return Orange;
//			break;
//		case Rainbow.YELLOW :
//			return Yellow;
//			break;
//		case Rainbow.GREEN :
//			return Green;
//			break;
//		case Rainbow.BLUE :
//			return Blue;
//			break;
//		case Rainbow.INDIGO :
//			return Indigo;
//			break;
//		case Rainbow.VIOLET :
//			return Violet;
//			break;
//		default :
//			return Color.black;
//			break;
//		}
//	}
//}
