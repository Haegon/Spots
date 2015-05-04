using UnityEngine;
using System.Collections;

public class ColorEx {

	public static Color Red = new Color(1.0f,0.0f,0.0f);
	public static Color Orange = new Color(1.0f,0.5f,0.0f);
	public static Color Yellow = new Color(1.0f,1.0f,0.0f);
	public static Color Green = new Color(0.0f,1.0f,0.0f);
	public static Color Blue = new Color(0.0f,0.0f,1.0f);
	public static Color Indigo = new Color(0.1f,0.0f,120f/256f);
	public static Color Violet = new Color(144f/256f,54f/256f,1.0f);

	public static Color SpotColor(string gameObjectName) {

		switch (gameObjectName) {
		case "Button_Spot_R" :
			return Red;
			break;
		case "Button_Spot_O" :
			return Orange;
			break;
		case "Button_Spot_Y" :
			return Yellow;
			break;
		case "Button_Spot_G" :
			return Green;
			break;
		case "Button_Spot_B" :
			return Blue;
			break;
		case "Button_Spot_I" :
			return Indigo;
			break;
		case "Button_Spot_V" :
			return Violet;
			break;
		default :
			return Color.black;
			break;
		}
	}

	public static Rainbow GetRainbow(string gameObjectName){
		
		switch (gameObjectName) {
		case "Button_Spot_R" :
			return Rainbow.RED;
			break;
		case "Button_Spot_O" :
			return Rainbow.ORANGE;
			break;
		case "Button_Spot_Y" :
			return Rainbow.YELLOW;
			break;
		case "Button_Spot_G" :
			return Rainbow.GREEN;
			break;
		case "Button_Spot_B" :
			return Rainbow.BLUE;
			break;
		case "Button_Spot_I" :
			return Rainbow.INDIGO;
			break;
		case "Button_Spot_V" :
			return Rainbow.VIOLET;
			break;
		default :
			return Rainbow.NONE;
			break;
		}
	}

	public static Color GetColor(Rainbow rainbow){
		
		switch (rainbow) {
		case Rainbow.RED :
			return Red;
			break;
		case Rainbow.ORANGE :
			return Orange;
			break;
		case Rainbow.YELLOW :
			return Yellow;
			break;
		case Rainbow.GREEN :
			return Green;
			break;
		case Rainbow.BLUE :
			return Blue;
			break;
		case Rainbow.INDIGO :
			return Indigo;
			break;
		case Rainbow.VIOLET :
			return Violet;
			break;
		default :
			return Color.black;
			break;
		}
	}
}
