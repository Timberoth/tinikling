using UnityEngine;
using System.Collections;

// Beat Data Container
// Beat is composed of a number of up/down hits followed by a number of in/out hits.

public class BeatPattern : MonoBehaviour {
	
	// Constants
	public string easeTypeDown = "easeInBack"; 
	public string easeTypeUp = "easeOutQuint"; 	
	public string easeTypeIn = "easeInBack"; 
	public string easeTypeOut = "easeOutQuint"; 
	
	// Speed and Distance Variables
	public float upDownDist = 3.0f;
	public float inOutDist = 5.0f;
			
	public float upDownSpeed = 20.0f;
	public float inOutSpeed = 20.0f;
	
	// Track up/down cycles to begin "in" motion
	public int upDownHits = 2;
	
	// Track in/out cycles to begin "up" motion
	public int inOutHits = 1;
	
	// Use this for initialization
	void Start () {
	
	}
}
