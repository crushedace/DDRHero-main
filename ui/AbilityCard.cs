using Godot;
using System;

//This handles the UI of all the active ability cards.
public partial class AbilityCard : Panel
{

	[Export] private AnimationPlayer m_animationPlayer;
	private Ability m_ability;
	[Export] private TextureRect m_icon;
	[Export] private Label m_name;
	[Export] private RichTextLabel m_description;

	private bool m_isBeingDragged = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetAbility(Ability ability)
	{
		m_ability = ability;
		//m_icon.Texture = m_ability.Icon;
		m_name.Text = m_ability.Name;
		m_description.Text = m_ability.Description;
	}

//Mouse controls
	public void OnMouseEntered()
	{
		GrabFocus(); 
		
	}

	public void OnMouseExited()
	{
		ReleaseFocus();
		
	}

//Non mouse
	public void OnFocusEntered()
	{
		m_animationPlayer.Play("AbilityCardUI_Grow");
		ZIndex = 1;
	}

	public void OnFocusExited()
	{
		m_animationPlayer.PlayBackwards("AbilityCardUI_Grow");
		ZIndex = 0;
	}

	public override void _GuiInput(InputEvent @event){

		if (@event is InputEventMouseButton)
		{
			if (@event.IsPressed() && HasFocus())
			{
				m_isBeingDragged = true;
			}
			else 
			{
				m_isBeingDragged = false;
			}
		}

		else if (@event is InputEventMouseMotion)
		{
			if (m_isBeingDragged)
			{

			}
		}

	}

	
	
}
