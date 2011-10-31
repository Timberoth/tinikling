using UnityEngine;
using System.Collections;


public class LevelEvent {
	
	// When events should be trigger.
	public float triggerTime = 0.0f;
	
	// How fast the feet symbols are moving
	public float footPatternSpeed = -1.0f;
	
	// How fast the sticks are moving
	public float stickTempoSpeed = -1.0f;
	
	// Object that will make the callback
	public GameObject callbackObject = null;
	
	// Callback message
	public string callbackMessage = "";
	
	// Callback data
	public string callbackData = "";
	
}
