using Godot;
using System;
using Data; //RENAME

public partial class PlayerAttackComponent : Node //Perhaps have inherit from Node2D instead to access World2D etc.
{
	private const int BUFFER_SIZE = 10;
	private const int BUFFER_WAIT = 10; // Likely Unecessary

	private DirectionInputs[] m_inputBuffer = new DirectionInputs[BUFFER_SIZE];

	[Export] private Ability m_currentAbility; // Figure this out more thoroughly. Should be loaded in by the deck.
	private int m_inputSequenceIndex = 0;

	[Signal] public delegate void OnInputSequenceReadyEventHandler(Godot.Collections.Array<DirectionInputs> InputSequence);	
	//Consider merging these bottom 3.
	[Signal] public delegate void OnInputSequenceFailedEventHandler();
	[Signal] public delegate void OnInputSequenceCorrectEventHandler();
	[Signal] public delegate void OnInputSequenceCompleteEventHandler();	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("direction_Up")) AddToBuffer(DirectionInputs.Up);
		if (Input.IsActionJustPressed("direction_Down")) AddToBuffer(DirectionInputs.Down);
		if (Input.IsActionJustPressed("direction_Left")) AddToBuffer(DirectionInputs.Left);
		if (Input.IsActionJustPressed("direction_Right")) AddToBuffer(DirectionInputs.Right);
	}

	private void AddToBuffer(DirectionInputs a_value){
		
		//If the pressed key matches with the current direction in the input sequence, move to the next in line and emit a signal for the UI.
		if (a_value == m_currentAbility.InputSequence[m_inputSequenceIndex])
		{
			m_inputSequenceIndex++;
			EmitSignal(SignalName.OnInputSequenceCorrect);
		}
		//If the key was incorrect, reset progress and emit a different signal for UI. Potentially combine these two signals later.
		else 
		{
			GD.Print("Attack Failed");
			m_inputSequenceIndex = 0;
			
			EmitSignal(SignalName.OnInputSequenceFailed);
		}

		// If the input sequence has been completed, activate the current ability and emit a signal for the Deck to load the next attack. Or access it from here.
		if (m_inputSequenceIndex == m_currentAbility.InputSequence.Count) {
			GD.Print("Attack Success");
			m_inputSequenceIndex = 0;

			EmitSignal(SignalName.OnInputSequenceComplete);

			m_currentAbility.Activate(); //Perhaps emit a signal as well. So that deck moves to next card etc. Maybe make it one script. I'm not a software engineer.

			//The deck should probably call Load Input sequence.
			EmitSignal(SignalName.OnInputSequenceReady, m_currentAbility.InputSequence);
		}
	}

	//Call this to prepare the next ability.
	public void LoadInputSequence(Ability a_ability)
	{
		GD.Print(a_ability.Name);
		m_currentAbility = a_ability;
		EmitSignal(SignalName.OnInputSequenceReady, m_currentAbility.InputSequence);
	}
}
