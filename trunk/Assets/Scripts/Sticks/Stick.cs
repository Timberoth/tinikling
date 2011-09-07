using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
public abstract class Stick : MonoBehaviour {
		
	// Public
	public string stickName;
	
	// Track whether the stick should be active/moving or not.
	// This flag can be flipped by the GameManager to stop the
	// game from progressing.
	public bool stickActive = false;
	
	// Protected
	protected AudioSource groundHit;
	protected AudioSource stickHit;
	
	// Privates	
	// Track up/down cycles to begin "in" motion
	private int upDownCounter = 0;
	
	// Track in/out cycles to begin "up" motion
	private int inOutCounter = 0;
	
	
	// Abstract Functions
	public abstract void Reset();	
	public abstract void StartIn();	
	public abstract void StartOut();	
	
	// Unity Functions
	
	// Use this for initialization
	void Start () {			
		// Set up audio references
		InitAudio();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	// Stick Functions
	
	public void StartMoving()
	{
		stickActive = true;
		StartUp();
	}
	
	
	public void StopMoving()
	{
		// Flip the flag so the sticks don't start another cycle.
		stickActive = false;
		
		// Wait for signal to start again
	}
		
	
	protected void InitAudio()
	{
		// Create references to the attached audio sources.
		foreach( AudioSource source in this.GetComponents<AudioSource>() )
		{
			if( source.clip.name == "GroundHit" )
			{
				groundHit = source;				
			}
			else if( source.clip.name == "StickHit" )
			{
				stickHit = source;				
			}
		}		
	}
		
	protected void StartDown()
	{
		iTween.MoveBy(gameObject, iTween.Hash("z", GameManager.instance.beat.upDownDist,
		                                      "easeType", GameManager.instance.beat.easeTypeDown,
		                                      "speed", GameManager.instance.beat.upDownSpeed,
		                                      "oncomplete", "DownComplete"));
	}
	
	protected void DownComplete()
	{
		upDownCounter++;
		
		// Play sound
		groundHit.Play();
		
		// TODO Pull this value from the current BeatPattern.
		if( upDownCounter >= GameManager.instance.beat.upDownHits )
		{
			// Reset up down counter
			upDownCounter = 0;
			
			// Go Inward
			StartIn();			
		}
		else
		{
			// Go Up again
			StartUp();
		}
	}
	
	protected void StartUp()
	{
		iTween.MoveBy(gameObject, iTween.Hash("z", -GameManager.instance.beat.upDownDist, 
		                                      "speed", GameManager.instance.beat.upDownSpeed,
		                                      "easeType", GameManager.instance.beat.easeTypeUp, 		                                      
		                                      "oncomplete", "UpComplete"));
	}
	
	protected void UpComplete()
	{
		// Start Down
		StartDown();
	}	
	
	
	public void InComplete()
	{
		// Play sound FX
		stickHit.Play();
		
		// Play particles
		
		StartOut();
	}
	
	
	public void OutComplete()
	{
		// Play sound FX
		
		// Play particles
		
		inOutCounter++;
		
		// TODO Pull this value from the current BeatPattern.
		if( inOutCounter >= GameManager.instance.beat.inOutHits )
		{
			inOutCounter = 0;
			
			if( stickActive )
			{
				// Start the next cycle
				StartUp();	
			}		
		}
		else
		{
			if( stickActive )
			{
				// Start the next cycle
				StartIn();	
			}	
		}
		
		// Update tempo if necessary		
	}	
}
