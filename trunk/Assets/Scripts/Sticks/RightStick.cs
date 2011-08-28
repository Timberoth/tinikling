using UnityEngine;
using System.Collections;

public class RightStick : Stick {
	
	// Variables
	
	
	
	// Abstract Functions
	public override void Reset()
	{
		// Put the stick in the starting down position.	
	}
	
	public override void StartIn()
	{
		iTween.MoveBy(gameObject, iTween.Hash("x", -inOutDist, 
		                                      "easeType", easeTypeIn, 		                                      
		                                      "speed", speed,
		                                      "oncomplete", "InComplete"));
	}
		
	public override void StartOut()
	{
		iTween.MoveBy(gameObject, iTween.Hash("x", inOutDist, 
		                                      "easeType", easeTypeOut, 		                                     
		                                      "speed", speed,
		                                      "oncomplete", "OutComplete"));
	}
		
	
	// Normal Functions
	
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
