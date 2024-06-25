using Godot;
using System;
using Data;

//The base class for Abilities. The input & deck system will rely on these.
//They can be customised in editor, although different behaviour will need to be defined in script.
public partial class Ability : Resource
{

    //Name & Desc to appear when the deck is viewed and on the currently accessible card.
    [Export] public string Name { get; set; }
    [Export] public string Description { get; set; }

    //The input sequence is used by the input system to activate the ability.
    [Export] public Godot.Collections.Array<DirectionInputs> InputSequence { get; set; }
    
    //Tags will be used by items to apply effects.
    //"Apply poison on ranged hit"
    //"Increase Melee Damage" etc.
    [Export] public Godot.Collections.Array<string> Tags { get; set; }
    public virtual void Activate() {} //This should feed in the player probably. Although location can be accessed without it.
    

}
