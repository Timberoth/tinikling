using UnityEngine;
using System.Collections;
using System;

public abstract class Level : MonoBehaviour {
	
	// Level consists of:
	// 1) Music Track
	// 2) List of times with associated LevelEvents that can do things like
	// change the tempo, change the beat pattern, display text, fire particles, etc.
	
	
	// Music Track Reference
	protected AudioSource music = null;
	
	// List of times with associated LevelEvents that can do things like
	// change the tempo, change the beat pattern, display text, fire particles, etc.
	protected Queue levelEvents;
	
	
	private float levelTimer = 0.0f;
	
	/// <summary>
	/// Abstract Functions
	/// </summary>
	
	// Fill up the array of events that will be triggered during this level.
	public abstract void CreateLevelEvents();
	
	// Use this for initialization
	void Start () {
		
		// Create event queue
		levelEvents = new Queue();
		
		// Create the actual level events
		CreateLevelEvents();
		
		// Initialize audio track and start it.
		InitAudio();
	}
	
	// Update is called once per frame
	void Update () {
			
		levelTimer += Time.deltaTime;
		
		bool doneFiringEvents = false;
		
		while( !doneFiringEvents )
		{
			// Check if there are events to check in the active queue
			if( levelEvents.Count > 0 )
			{
				// Check if the event at the top should be fired
				LevelEvent levelEvent = levelEvents.Peek() as LevelEvent;
				if( Math.Abs( levelEvent.triggerTime - levelTimer ) <= 0.05 || (levelTimer >= levelEvent.triggerTime ) )
				{
					// Dequeue the LevelEvents queue.
					levelEvents.Dequeue();
					
					// Apply valid stick tempo speeds
					if( levelEvent.stickTempoSpeed > 0 )
					{
						// Change the stick tempo through the stick manager.						
						StickManager.Instance.ChangeSpeed( levelEvent.stickTempoSpeed );
						print( "Changing stick tempo speed: " + levelEvent.stickTempoSpeed );
					}
					
					// Apply valid foot pattern speeds
					if( levelEvent.footPatternSpeed > 0 )
					{
						// Change the foot pattern speed through the game manager							
						GameObject patternObject = GameObject.FindGameObjectWithTag("FootPattern") as GameObject;
						FootPattern pattern = patternObject.GetComponent<FootPattern>();
						pattern.currentSpeed = levelEvent.footPatternSpeed;
						print( "Changing foot pattern speed: " + levelEvent.footPatternSpeed );
					}
					
					// Fire messages
					if( levelEvent.callbackObject != null )
					{
						levelEvent.callbackObject.SendMessage( levelEvent.callbackMessage, 
						                                      levelEvent.callbackData, 	
						                                      SendMessageOptions.RequireReceiver);
					}
				}
				
				// The top event is not ready to be fired so we're done
				else
				{
					doneFiringEvents = true;	
				}
			}
			
			
			// If there are no events left, then we're done.
			else
			{				
				doneFiringEvents = true;	
			}
		}
	}
	
	void InitAudio()
	{
		// Create references to the attached audio sources.
		foreach( AudioSource source in this.GetComponents<AudioSource>() )
		{			
			if( source.clip.name.Contains( "Music" ) )
			{
				music = source;				
				break;
			}			
		}		
				
		if( music )
			music.Play();
	}	
}
