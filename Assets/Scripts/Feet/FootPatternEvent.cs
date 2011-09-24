using System.Collections;

public class FootPatternEvent {
	
	// Name of FootSymbol object
	public string symbolName;
	
	// Time of event
	public float time;
	
	// Foot State - Up, Down, Inactive
	public FootSymbol.FootState state;
	
	// Foot - Left, Right
	public FootSymbol.Foot foot;
	
	public bool flipped;
}
