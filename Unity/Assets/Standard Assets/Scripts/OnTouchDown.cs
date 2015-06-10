//	OnTouchDown.cs
//	Allows "OnMouseDown()" events to work on the iPhone.
//	Attack to the main camera.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
public class OnTouchDown : MonoBehaviour
{
	private GameObject lastHitObject;

	void Update ()
	{
		RaycastHit hit = new RaycastHit();
		for (int i = 0; i < Input.touchCount; ++i)
		{
			// Construct a ray from the current touch coordinates
			Ray ray = camera.ScreenPointToRay(Input.GetTouch(i).position);
			if ( Physics.Raycast(ray, out hit) )
			{
				var hitObject = hit.transform.gameObject;
				if (Input.GetTouch(i).phase == TouchPhase.Began)
				{
					lastHitObject = hitObject;
					hitObject.SendMessage("OnPointerDown");
				}
				if (Input.GetTouch(i).phase == TouchPhase.Ended)
				{
					if (lastHitObject == hitObject)
					{
						hitObject.SendMessage("OnPointerUpAsButton");
					}
					hitObject.SendMessage("OnPointerUp");
					lastHitObject = null;
				}
			}
		}
	}
}*/

public class OnTouchDown : MonoBehaviour {
	void Update()
	{
		// Code for OnMouseDown in the iPhone. Unquote to test.
		RaycastHit hit = new RaycastHit();
		for (int i = 0; i < Input.touchCount; ++i)
		{
			if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
			{
				// Construct a ray from the current touch coordinates
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				if (Physics.Raycast(ray, out hit))
				{
					hit.transform.gameObject.SendMessage("OnMouseDown");
				}
			}
		}
	}
	
}