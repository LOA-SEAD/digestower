using UnityEngine;

public class ColorX {

	public ColorX () {
		//StartGame.numberOfColorXObjectsAlive++;
	}
	~ColorX() {
		//StartGame.numberOfColorXObjectsAlive--;
	}

	private static string GetHex(int num) {
		const string alpha = "0123456789abcdef";
		string ret = "" + alpha[num];
		return ret;
	}
	
	private static int HexToInt(char hexChar) {
		switch (hexChar) {
		case '0': return 0;
		case '1': return 1;
		case '2': return 2;
		case '3': return 3;
		case '4': return 4;
		case '5': return 5;
		case '6': return 6;
		case '7': return 7;
		case '8': return 8;
		case '9': return 9;
		case 'a': return 10;
		case 'b': return 11;
		case 'c': return 12;
		case 'd': return 13;
		case 'e': return 14;
		case 'f': return 15;
		}
		return -1;
	}
	
	public static string RGBToHex(Color color) {
		float red = color.r * 255;
		float green = color.g * 255;
		float blue = color.b * 255;
		
		string a = GetHex(Mathf.FloorToInt(red / 16));
		string b = GetHex(Mathf.RoundToInt(red) % 16);
		string c = GetHex(Mathf.FloorToInt(green / 16));
		string d = GetHex(Mathf.RoundToInt(green) % 16);
		string e = GetHex(Mathf.FloorToInt(blue / 16));
		string f = GetHex(Mathf.RoundToInt(blue) % 16);
		
		return a + b + c + d + e + f;
	}
	
	public static Color HexToRGB(string color) {
		float red = (HexToInt(color[1]) + HexToInt(color[0]) * 16f) / 255f;
		float green = (HexToInt(color[3]) + HexToInt(color[2]) * 16f) / 255f;
		float blue = (HexToInt(color[5]) + HexToInt(color[4]) * 16f) / 255f;
		Color finalColor = new Color { r = red, g = green, b = blue, a = 1 };
		return finalColor;
	}
	
}