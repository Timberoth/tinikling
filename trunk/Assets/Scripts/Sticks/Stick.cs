using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
public abstract class Stick : MonoBehaviour {
	
	// Constants
	protected const string easeTypeDown = "easeInBack"; 
	protected const string easeTypeUp = "easeOutQuint"; 	
	protected const string easeTypeIn = "easeInBack"; 
	protected const string easeTypeOut = "easeOutQuint"; 
	
	protected const float upDownDist = 3.0f;
	protected const float inOutDist = 5.0f;
	
	protected float speed = 20.0f;
	
	// Public
	public string stickName;
	
	// Protected
	protected AudioSource groundHit;
	protected AudioSource stickHit;
	
	// Privates
	
	// Track up/down cycles to begin "in" motion
	private int upDownCounter = 0;
	
	
	// Abstract Functions
	public abstract void Reset();	
	public abstract void StartIn();	
	public abstract void StartOut();	
	
	// Normal Functions
	
	// Use this for initialization
	void Start () {	
		
		// Set up audio references
		InitAudio();
		
		// Get the sticks moving
		StartUp();	
	}
	
	// Update is called once per frame
	void Update () {
	
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
		iTween.MoveBy(gameObject, iTween.Hash("z", upDownDist,
		                                      "easeType", easeTypeDown,
		                                      "speed", speed,
		                                      "oncomplete", "DownComplete"));
	}
	
	protected void DownComplete()
	{
		upDownCounter++;
		
		// Play sound
		groundHit.Play();
		
		if( upDownCounter >= 2 )
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
		iTween.MoveBy(gameObject, iTween.Hash("z", -upDownDist, 
		                                      "speed", speed,
		                                      "easeType", easeTypeUp, 		                                      
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
		// Update tempo if necessary
		
		// Play sound FX
		
		// Play particles
		
		// For now go up
		StartUp();
	}
	
}
