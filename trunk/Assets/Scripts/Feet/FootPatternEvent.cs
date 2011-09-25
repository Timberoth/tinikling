using System.Collections;

public class FootPatternEvent {
	
	// Name of FootSymbol object
	public string symbolName = "";
	
	// Time of event
	public float time = 0.0f;
	
	// Foot State - Up, Down, Inactive
	public FootSymbol.FootState state = FootSymbol.FootState.Inactive;
	
	// Foot - Left, Right
	public FootSymbol.Foot foot = FootSymbol.Foot.Left;
	
	// Is foot direction reversed
	public bool flipped = false;
	
	// True if this event should only be fired once and not repeated
	// in the second cyle.  
	// This is mainly used for the starting feet positions.
	public bool oneShot = false;
}
