using Godot;
using System;


// This may need to be massively overhauled to enable reordering the deck etc. 
// Which will probably mean making some custom container to move things about and do small animations
// We'll see.
public partial class DeckDisplay : Control
{

	//This is currently connected to a temporary Node. This will access the inventory through GM & player. 
	[Export] private AbilityDeck m_abilityDeck;
	[Export] private PackedScene m_AbilityCardScene = ResourceLoader.Load<PackedScene>("res://ui/ability_card.tscn");

	[Export] private HFlowContainer m_cardContainer;

	// Either use this if the inventory screen is created/destroyed each load. Or have it always hidden. That descision will come later.
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0; i < m_abilityDeck.Abilities.Count; i++){
			AbilityCard abilityCard = m_AbilityCardScene.Instantiate() as AbilityCard;
			abilityCard.SetAbility(m_abilityDeck.Abilities[i]);
			m_cardContainer.AddChild(abilityCard);
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
