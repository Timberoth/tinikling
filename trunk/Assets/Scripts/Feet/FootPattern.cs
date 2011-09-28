using UnityEngine;
using System;
using System.Collections;

public class FootPattern : MonoBehaviour {
	
	// Publics
	public float startingSpeed = 1.0f;
		
	// Privates
	public float currentSpeed = 1.0f;
		
	// One queue is the active one while the other stores
	// the events being popped out of the active one.
	protected Queue footEventQueue1;
	protected Queue footEventQueue2;
	
	// Pointer to the active queue
	protected Queue activeQueue;
	
	// Pointer to inactive queue
	protected Queue inactiveQueue;
	
	// Timer	
	protected float patternTimer = 0.0f;
	
	// Use this for initialization
	void Start () {
	
		currentSpeed = startingSpeed;
		
		// Events will be transferred between the two queues
		footEventQueue1 = new Queue();
		footEventQueue2 = new Queue();
		
		// Start with Queue1
		activeQueue = footEventQueue1;
		inactiveQueue = footEventQueue2;
		
		CreateSteps();				
	}
	
	// Create all the steps in the pattern.
	public virtual void CreateSteps()
	{
		// This is filled out in the individual patterns objects.
	}
	
	// Update is called once per frame
	protected void Update () {
		patternTimer += ( Time.deltaTime * currentSpeed );
		
		bool doneFiringEvents = false;
		
		while( !doneFiringEvents )
		{
			// Check if there are events to check in the active queue
			if( activeQueue.Count > 0 )
			{
				// Check if the event at the top should be fired
				FootPatternEvent footEvent = activeQueue.Peek() as FootPatternEvent;
				if( Math.Abs( footEvent.time - patternTimer ) <= 0.05 || (patternTimer >= footEvent.time) )
				{
					// Pop the top off the active queue and push it on the inactive queue
					// if it's not a oneshot event
					footEvent = activeQueue.Dequeue() as FootPatternEvent;
					
					if( !footEvent.oneShot )
						inactiveQueue.Enqueue( footEvent );
					
					// Now do the actual stuff of the event.
					GameObject footSymbolObject = GameObject.Find( footEvent.symbolName );
					if( footSymbolObject != null )
					{
						FootSymbol footSymbol = footSymbolObject.GetComponent<FootSymbol>();
						
						if( footSymbol != null )
						{
							footSymbol.UpdateFoot( footEvent.foot, footEvent.state, footEvent.flipped );
						}
					}				
				}
				
				// The top event is ready to be fired so we're done
				else
				{
					doneFiringEvents = true;	
				}
			}
			
			// If the active queue is empty then we're done processing and 
			// need to flip the active/inactive queues.
			else
			{				
				Queue temp = activeQueue;
				activeQueue	= inactiveQueue;
				inactiveQueue = temp;
						
				// Reset the pattern timer
				patternTimer = 0.0f;
				
				doneFiringEvents = true;				
			}			
		}		
	}
}
