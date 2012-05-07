using UnityEngine;
using System.Collections;

public class Level1 : Level {

	public override void CreateLevelEvents()
	{		
		LevelEvent levelEvent = new LevelEvent();
		levelEvent.triggerTime = 2.0f;
		levelEvent.footPatternSpeed = 1.5f;
		levelEvent.stickTempoSpeed = 1.5f;
		levelEvent.callbackObject = this.gameObject;
		levelEvent.callbackMessage = "Level1SpecificFunction";
		levelEvent.callbackData = "Callback is working for Level 1";
		//levelEvents.Enqueue( levelEvent );
		
		
		levelEvent = new LevelEvent();
		levelEvent.triggerTime = 8.0f;
		levelEvent.footPatternSpeed = 3.0f;
		levelEvent.stickTempoSpeed = 3.0f;
		//levelEvents.Enqueue( levelEvent );
	}
	
	public void Level1SpecificFunction( string data )
	{
		print( data );	
	}
}
