using UnityEngine;
using System.Collections;


public sealed class StickManager {

	/*
	 * Singleton Code
	 */	
	private static readonly StickManager instance = new StickManager();
	
	private StickManager(){}
	
	public static StickManager Instance{ get { return instance; } }
		
		
	// Stick Component refs
	private Stick []sticks;
	
	// Stick Names
	
	
	public void Initialize()
	{
		// Sticks
		sticks = new Stick[]{null,null};
		string[] stickNames = {"LeftStick", "RightStick"};
		for( int i = 0; i < stickNames.Length; i++ )
		{
			GameObject stickObject = GameObject.Find(stickNames[i]);
			if( stickObject )
			{
				Stick stick = stickObject.GetComponent<Stick>();
				if( stick )
				{
					sticks[i] = stick;
				}							
			}	
			// THIS SHOULD NEVER HAPPEN
			else
			{
				Debug.Break();
			}
		}
		
		// Get the game going now
		StartSticks();
	}
	
	
	// Get the sticks moving again.
	public void StartSticks()
	{				
		foreach( Stick stick in sticks )
		{			
			stick.StartMoving();		
		}			
	}
	
	
	// This won't stop the sticks immediately but will prevent them
	// from starting another cycle.
	public void StopSticks()
	{	
		foreach( Stick stick in sticks )
		{			
			stick.StopMoving();		
		}	
	}
	
	public void ChangeSpeed( float newSpeed )
	{
		foreach( Stick stick in sticks )
		{			
			stick.ChangeSpeed( newSpeed );		
		}	
	}	
}
