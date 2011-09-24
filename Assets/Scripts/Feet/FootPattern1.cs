using UnityEngine;
using System;
using System.Collections;

public class FootPattern1 : MonoBehaviour {
	
	// Publics
	public float startingSpeed = 1.0f;
		
	// Privates
	private float currentSpeed = 1.0f;
		
	// One queue is the active one while the other stores
	// the events being popped out of the active one.
	private Queue footEventQueue1;
	private Queue footEventQueue2;
	
	// Pointer to the active queue
	private Queue activeQueue;
	
	// Pointer to inactive queue
	private Queue inactiveQueue;
	
	// Timer	
	private float patternTimer = 0.0f;
	
	// Use this for initialization
	void Start () {
	
		// Events will be transferred between the two queues
		footEventQueue1 = new Queue();
		footEventQueue2 = new Queue();
		
		// Start with Queue1
		activeQueue = footEventQueue1;
		inactiveQueue = footEventQueue2;
		
		// Create all the FootPatternEvents.  
		// TODO have this info come from an XML file
		
		// Left foot down
		FootPatternEvent footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol1";
		footEvent.time = 0.0f;
		footEvent.foot = FootSymbol.Foot.Left;
		footEvent.state = FootSymbol.FootState.Down;
		footEvent.flipped = false;
		activeQueue.Enqueue( footEvent );
		
		// Right foot down
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol2";
		footEvent.time = 0.0f;
		footEvent.foot = FootSymbol.Foot.Right;
		footEvent.state = FootSymbol.FootState.Down;
		footEvent.flipped = false;
		activeQueue.Enqueue( footEvent );
		
		// Right foot up
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol2";
		footEvent.time = 1.0f;
		footEvent.foot = FootSymbol.Foot.Right;
		footEvent.state = FootSymbol.FootState.Inactive;
		footEvent.flipped = false;
		activeQueue.Enqueue( footEvent );
		
		// Right foot down
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol4";
		footEvent.time = 1.2f;
		footEvent.foot = FootSymbol.Foot.Right;
		footEvent.state = FootSymbol.FootState.Down;
		footEvent.flipped = false;
		activeQueue.Enqueue( footEvent );
		
		// Left foot up
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol4";
		footEvent.time = 1.3f;
		footEvent.foot = FootSymbol.Foot.Left;
		footEvent.state = FootSymbol.FootState.Up;
		footEvent.flipped = false;
		activeQueue.Enqueue( footEvent );
		
		// Left foot down
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol3";
		footEvent.time = 1.5f;
		footEvent.foot = FootSymbol.Foot.Left;
		footEvent.state = FootSymbol.FootState.Down;
		footEvent.flipped = false;
		activeQueue.Enqueue( footEvent );
	}
	
	// Update is called once per frame
	void Update () {
		patternTimer += Time.deltaTime;
		
		print(patternTimer);
		
		// Check if the event at the top should be fired
		FootPatternEvent footEvent = activeQueue.Peek() as FootPatternEvent;
		if( Math.Abs( footEvent.time - patternTimer ) <= 0.05 )
		{
			// Pop the top off the active queue and push it on the inactive queue
			footEvent = activeQueue.Dequeue() as FootPatternEvent;
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
			
			// If that was the last event we need to swap active/inactive queues
			if( activeQueue.Count <= 0 )
			{
				Queue temp = activeQueue;
				activeQueue	= inactiveQueue;
				inactiveQueue = temp;
				
				// Reset the pattern timer
				patternTimer = 0.0f;
			}
		}
	}
}
