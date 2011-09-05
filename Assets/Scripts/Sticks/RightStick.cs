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
		iTween.MoveBy(gameObject, iTween.Hash("x", -GameManager.instance.inOutDist, 
		                                      "easeType", easeTypeIn, 		                                      
		                                      "speed", GameManager.instance.inOutSpeed,
		                                      "oncomplete", "InComplete"));
	}
		
	public override void StartOut()
	{
		iTween.MoveBy(gameObject, iTween.Hash("x", GameManager.instance.inOutDist, 
		                                      "easeType", easeTypeOut, 		                                     
		                                      "speed", GameManager.instance.inOutSpeed,
		                                      "oncomplete", "OutComplete"));
	}
		
	
	// Normal Functions
	
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
