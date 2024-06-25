using Godot;
using System;
[GlobalClass]
public partial class playercontroller : Node
{
    CharacterBody2D _player_cb2d;
    // input 
    Vector2 _playerInput = Vector2.Zero;

    //constants
    const float BaseSpeed = 250f;

    public override void _Ready()
    {
        _player_cb2d = this.GetParent() as CharacterBody2D;
    }
    public override void _Process(double delta)
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        //get input
        _playerInput = GetDirection();
        //set velocity
        _player_cb2d.Velocity = _playerInput * BaseSpeed ;
        //move and slide
        _player_cb2d.MoveAndSlide();

    }
    Vector2 GetDirection()
    {
        Vector2 input = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        input = new Vector2(Math.Sign(input.X), Math.Sign(input.Y)); 
        return input.Normalized();
    }
}

