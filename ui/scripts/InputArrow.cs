using Godot;
using System;
using Data;


//Class for the UI arrow elements.
public partial class InputArrow : Control
{
	[Export] public TextureRect textureRect;
	public DirectionInputs direction;
}
