using UnityEngine;
using System.IO;
using System.Collections;

public class MakePNG : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Save(720,1280);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Save(int width, int height) {
		Texture2D newTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);

		int cx = width/2;
		int cy = height/2;

		float[,] array1 = new float[width,height];

		for (int i = 0 ; i < width ; i++ ) {
			for (int j = 0 ; j < height ; j++ ) {
				int b1 = width/9*2;
				int b2 = width-width/9*2;
				int distance;
				int standard = width/9*2;

				float a;
				if ( i < b1 ) {
					distance = Mathf.Abs(i - b1);
					a = (float)Mathf.Pow(distance,2)/(float)Mathf.Pow(standard,2);
				} else if ( i > b2 ){
					distance = Mathf.Abs(i - b2);
					a = (float)Mathf.Pow(distance,2)/(float)Mathf.Pow(standard,2);
				} else {
					a = 0.0f;
				}
				array1[i,j] = a;
			}
		}

		float[,] array2 = new float[width,height];
		
		for (int i = 0 ; i < width ; i++ ) {
			for (int j = 0 ; j < height ; j++ ) {
				int b1 = height/16*2;
				int b2 = height-height/16*2;
				int distance;
				int standard = height/16*2;
				
				float a;
				if ( j < b1 ) {
					distance = Mathf.Abs(j - b1);
					a = (float)Mathf.Pow(distance,2)/(float)Mathf.Pow(standard,2);
				} else if ( j > b2 ){
					distance = Mathf.Abs(j - b2);
					a = (float)Mathf.Pow(distance,2)/(float)Mathf.Pow(standard,2);
				} else {
					a = 0.0f;
				}
				array2[i,j] = a;
			}
		}
		
		for (int i = 0 ; i < width ; i++ ) {
			for (int j = 0 ; j < height ; j++ ) { 
				newTexture.SetPixel(i,j, new Color(1.0f,1.0f,1.0f,(array1[i,j]+array2[i,j])/2.0f));
			}
		}

		newTexture.Apply();
		byte[] bytes = newTexture.EncodeToPNG();
		File.WriteAllBytes(Application.dataPath + "/../testscreen2.png", bytes);

	}    
}
