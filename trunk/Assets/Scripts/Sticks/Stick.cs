using UnityEngine;
using System.Collections;


/*
 * TODO This has to be broken down in a different way since the stick pattern
 * consist of 3 movements instead of the two which I initially thought.
 * In - sticks clack together
 * In-Down - stick clack on touching ground after arcing over from in
 * Down - stick clack after going up and then down
 */

[RequireComponent (typeof (Collider))]
public abstract class Stick : MonoBehaviour {
	
	// Dust particle that plays when the two sticks hit.
	// Only one of the sticks will need the particle reference.
	public GameObject dustParticles = null;
	
	// Track whether the stick should be active/moving or not.
	// This flag can be flipped by the GameManager to stop the
	// game from progressing.
	protected bool stickActive = false;
		
	protected AudioSource groundHit;
	protected AudioSource stickHit;
	
	// Overall movement speed
	protected float speed = 1.0f;
		
	// Track up/down cycles to begin "in" motion
	private int upDownCounter = 0;
	
	// Track in/out cycles to begin "up" motion
	private int inOutCounter = 0;
	
	
	// Abstract Functions
	public abstract void Reset();	
	public abstract void DownToIn();	
	public abstract void InToDown();	
	
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
		DownToIn();
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
	
	protected void DownToUp()
	{
		iTween.MoveBy(gameObject, iTween.Hash("z", -GameManager.instance.beat.upDownDist, 
		                                      "speed", GameManager.instance.beat.upDownSpeed * speed,
		                                      "easeType", GameManager.instance.beat.easeTypeUp, 		                                      
		                                      "oncomplete", "UpToDown"));
	}
		
	protected void UpToDown()
	{
		iTween.MoveBy(gameObject, iTween.Hash("z", GameManager.instance.beat.upDownDist,
		                                      "easeType", GameManager.instance.beat.easeTypeDown,
		                                      "speed", GameManager.instance.beat.upDownSpeed * speed,
		                                      "oncomplete", "UpToDownComplete"));
	}
	
	// NOTE: DownToIn and InToDown are defined in the child classes				
	
	public void DownToInComplete()
	{
		// Play sound FX
		// TODO ENABLE SOUND
		stickHit.Play();
		
		// Play particles
		if( dustParticles )
		{
			// Spawn the dustParticle object and it will autodestruct on it's own.
			GameObject.Instantiate( dustParticles, Vector3.zero, Quaternion.identity );			
		}
		
		InToDown();
	}
	
	public void InToDownComplete()
	{		
		upDownCounter++;
		
		// Play sound
		// TODO ENABLE SOUND
		groundHit.Play();
		
		DownToUp();	
	}
	
	
	public void UpToDownComplete()
	{		
		upDownCounter++;
		
		// Play sound
		// TODO ENABLE SOUND
		groundHit.Play();
				
		// TODO Pull this value from the current BeatPattern.
		if( upDownCounter >= GameManager.instance.beat.upDownHits )
		{
			// Reset up down counter
			upDownCounter = 0;
			
			// Go Inward
			DownToIn();			
		}
		else
		{
			// Go Up again
			DownToUp();
		}		
	}
		
	// Change the stick speed.
	public void ChangeSpeed( float newSpeed )
	{
		speed = newSpeed;	
	}
}
