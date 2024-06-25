using Godot;
using System;

//This holds the players current ability deck. 
public partial class AbilityDeck : Node
{



// A player inventory component will hold the true list of abilities while this list is used to hold the current arrangement.
// This means that abilities that effect the first card in a hand, or guarantee one is drawn first can call alternate shuffles.
// Which is it's own problem but hey.

//Abilities are added to/removed from this list.
	[Export] private Godot.Collections.Array<Ability> m_abilities; 
	public Godot.Collections.Array<Ability> Abilities {get {return m_abilities;}}
//This is the selected ability to load when shuffling. By default this is AbilityShuffle. Perhaps items could remove this necessity or change it to another ability.
	[Export] private Ability m_shuffleAbility;
	private int m_currentAbiltyIndex = 0;
	

	[Signal] public delegate void OnDeckEndReachedEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Connect to the PlayerInputSequence signals
		GameManager.Instance.playerAttackComponentTEMP.OnInputSequenceComplete += InputSequenceComplete;
		
		//Shuffle by default and load the first ability. Make this triggered by gamestart rather than _Ready().
		m_abilities.Shuffle();
		LoadAbility(0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	//When an input is complete, increase ability index. If it is at the end of the sequence, trigger DeckEndReached()
	//Otherwise, LoadNextAbility(), if the index is past the sequence length, start again as it will have passed by shuffle already.
	//This is probably a weird roundabout solution and might require some changes later but it works for the moment.
	public void InputSequenceComplete()
	{
		m_currentAbiltyIndex++;
		if (m_currentAbiltyIndex == m_abilities.Count) 
		{
			DeckEndReached();
		}
		else 
		{
			LoadNextAbility();
		}
	}

	public void LoadNextAbility()
	{
		if (m_currentAbiltyIndex >= m_abilities.Count) 
		{
			m_currentAbiltyIndex = 0;
		}
		
		LoadAbility(m_currentAbiltyIndex);
	}
	public void LoadAbility(int a_abilityIndex)
	{
		if (a_abilityIndex >= m_abilities.Count) 
		{
			GD.PrintErr("Index Out of Bounds");
			return;
		}
		GameManager.Instance.playerAttackComponentTEMP.LoadInputSequence(m_abilities[a_abilityIndex]);
	}

	public void LoadAbility(Ability a_ability){
		GameManager.Instance.playerAttackComponentTEMP.LoadInputSequence(a_ability);
	}

//When the deck reaches its end emit a signal, load in the shuffle card and shuffle the deck.
//This is probably going to require change to work with UI animations etc.
	public void DeckEndReached()
	{
		EmitSignal(SignalName.OnDeckEndReached);
		LoadAbility(m_shuffleAbility);
		ShuffleDeck();
	}

//Use the built in shuffle & randomize it each time. Randomisation probably unnecessary.
	private void ShuffleDeck()
	{
		GD.Randomize();
		m_abilities.Shuffle();
		GD.Print(m_abilities);
	}
}
