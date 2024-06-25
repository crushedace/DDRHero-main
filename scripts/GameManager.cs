using Godot;
using System;


//GameManager runs the main game logic and handles game state.
public partial class GameManager : Node2D
{

//Temporary Reference to the Attack Component. This will be gotten through a reference to the current Player.
	[Export] public PlayerAttackComponent playerAttackComponentTEMP;

//Singleton Pattern so that the current GameManager is available everywhere.
    private static GameManager _instance;
	public static GameManager Instance
	{
		get {
			if (_instance is null){
				GD.PrintErr("GameManager is NULL");		
			}
			return _instance;
		}
	}

//_instance is set the moment GameManager is created and does not wait for other nodes.
    public override void _EnterTree()
    {
        _instance = this;
    }
    public override void _Ready()
    {
		
    }

}
