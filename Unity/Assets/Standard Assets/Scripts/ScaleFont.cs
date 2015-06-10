using UnityEngine;
using System.Collections;
[ExecuteInEditMode]


public class ScaleFont : MonoBehaviour {
	
	public Vector2 offset;
	
	public float ratio = 10;
	public float posx, posy;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = Camera.main.WorldToViewportPoint(Camera.main.transform.TransformPoint(new Vector3(posx, posy, 0)));
	}

	public void Awake() {
		//transform.position = Camera.main.WorldToViewportPoint(Camera.main.transform.TransformPoint(new Vector3(posx, posy, 0)));
		//transform.position = Camera.main.WorldToViewportPoint(transform.parent.position);
	}

	public void LateUpdate() {
		transform.position = Camera.main.WorldToViewportPoint(Camera.main.transform.TransformPoint(new Vector3(posx, posy, 0)));

	}
	void OnGUI(){
		float finalSize = (float)Screen.width/ratio;
		guiText.fontSize = (int)finalSize;
		guiText.pixelOffset = new Vector2( offset.x * Screen.width, offset.y * Screen.height);
	}
	
	
}