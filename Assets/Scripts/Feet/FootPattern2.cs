using UnityEngine;
using System;
using System.Collections;

public class FootPattern2 : FootPattern {	
	
	public override void CreateSteps()
	{
		// Create all the FootPatternEvents.  
		// TODO have this info come from an XML file
		
		// DEBUG TIME
		float DEBUGTIME = 0.0f;
		
		// Left foot down
		FootPatternEvent footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol3";
		footEvent.time = 0.0f;
		footEvent.foot = FootSymbol.Foot.Left;
		footEvent.state = FootSymbol.FootState.Down;		
		activeQueue.Enqueue( footEvent );
		
		// Right foot down
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol4";
		footEvent.time = 0.0f;
		footEvent.foot = FootSymbol.Foot.Right;
		footEvent.state = FootSymbol.FootState.Down;		
		activeQueue.Enqueue( footEvent );
		
		++DEBUGTIME;
		
		// Left foot inactive
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol3";
		footEvent.time = DEBUGTIME;
		footEvent.foot = FootSymbol.Foot.Left;
		footEvent.state = FootSymbol.FootState.Up;		
		activeQueue.Enqueue( footEvent );
		
		// Right foot inactive
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol4";
		footEvent.time = DEBUGTIME;
		footEvent.foot = FootSymbol.Foot.Right;
		footEvent.state = FootSymbol.FootState.Up;		
		activeQueue.Enqueue( footEvent );
		
		++DEBUGTIME;
						
		// Left foot down
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol3";
		footEvent.time = DEBUGTIME;
		footEvent.foot = FootSymbol.Foot.Left;
		footEvent.state = FootSymbol.FootState.Down;		
		activeQueue.Enqueue( footEvent );
		
		// Right foot down
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol4";
		footEvent.time = DEBUGTIME;
		footEvent.foot = FootSymbol.Foot.Right;
		footEvent.state = FootSymbol.FootState.Down;		
		activeQueue.Enqueue( footEvent );
		
		++DEBUGTIME;
		
		// Left foot inactive
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol3";
		footEvent.time = DEBUGTIME;
		footEvent.foot = FootSymbol.Foot.Left;
		footEvent.state = FootSymbol.FootState.Up;
		activeQueue.Enqueue( footEvent );
		
		// Right foot inactive
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol4";
		footEvent.time = DEBUGTIME;
		footEvent.foot = FootSymbol.Foot.Right;
		footEvent.state = FootSymbol.FootState.Up;		
		activeQueue.Enqueue( footEvent );
				
		++DEBUGTIME;			
		
		// Left foot down
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol3";
		footEvent.time = DEBUGTIME;
		footEvent.foot = FootSymbol.Foot.Left;
		footEvent.state = FootSymbol.FootState.Down;
		activeQueue.Enqueue( footEvent );
		
		// Right foot down
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol4";
		footEvent.time = DEBUGTIME;
		footEvent.foot = FootSymbol.Foot.Right;
		footEvent.state = FootSymbol.FootState.Down;
		activeQueue.Enqueue( footEvent );
		
		++DEBUGTIME;			
		
		// Left foot inactive
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol3";
		footEvent.time = DEBUGTIME;
		footEvent.foot = FootSymbol.Foot.Left;
		footEvent.state = FootSymbol.FootState.Inactive;
		activeQueue.Enqueue( footEvent );
		
		// Right foot inactive
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol4";
		footEvent.time = DEBUGTIME;
		footEvent.foot = FootSymbol.Foot.Right;
		footEvent.state = FootSymbol.FootState.Inactive;
		activeQueue.Enqueue( footEvent );	
		
		// Left foot down
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol2";
		footEvent.time = DEBUGTIME;
		footEvent.foot = FootSymbol.Foot.Left;
		footEvent.state = FootSymbol.FootState.Down;
		activeQueue.Enqueue( footEvent );
		
		// Right foot down
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol5";
		footEvent.time = DEBUGTIME;
		footEvent.foot = FootSymbol.Foot.Right;
		footEvent.state = FootSymbol.FootState.Down;
		activeQueue.Enqueue( footEvent );	
		
		++DEBUGTIME;
		
		// Left foot inactive
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol2";
		footEvent.time = DEBUGTIME;
		footEvent.foot = FootSymbol.Foot.Left;
		footEvent.state = FootSymbol.FootState.Inactive;
		activeQueue.Enqueue( footEvent );
		
		// Right foot inactive
		footEvent = new FootPatternEvent();
		footEvent.symbolName = "FootSymbol5";
		footEvent.time = DEBUGTIME;
		footEvent.foot = FootSymbol.Foot.Right;
		footEvent.state = FootSymbol.FootState.Inactive;
		activeQueue.Enqueue( footEvent );	
				
		// It all repeats from here.	
	}
}
