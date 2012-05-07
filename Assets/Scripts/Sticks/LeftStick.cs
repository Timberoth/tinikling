using UnityEngine;
using System.Collections;


public class LeftStick : Stick {
	
	// Variables
	
	
	// Abstract Functions
	public override void Reset()
	{
		// Put the stick in the starting down position.	
	}
	
	public override void DownToIn()
	{
		iTween.MoveBy(gameObject, iTween.Hash("x", GameManager.instance.beat.inOutDist, 
		                                      "easeType", GameManager.instance.beat.easeTypeIn, 		                                      
		                                      "speed", GameManager.instance.beat.inOutSpeed * speed,
		                                      "oncomplete", "DownToInComplete"));
	}
	
	public override void InToDown()
	{		
		iTween.MoveBy(gameObject, iTween.Hash("x", -GameManager.instance.beat.inOutDist, 
		                                      "easeType", GameManager.instance.beat.easeTypeOut, 		                                     
		                                      "speed", GameManager.instance.beat.inOutSpeed * speed,
		                                      "oncomplete", "InToDownComplete"));
	}	
}
