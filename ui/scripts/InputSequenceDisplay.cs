using Godot;
using System;
using Data;

//The class for managing the Input Sequence UI.
public partial class InputSequenceDisplay : Control
{
	private int m_currentArrowIndex = 0;


	//These are arrays of textures rather than just one rotated.
	// I chose this in case we implement rebindable controls and want to change the sprites to account for other bindings.
	[Export] private Godot.Collections.Array<Texture2D> m_arrowTextures;
	[Export] private Godot.Collections.Array<Texture2D> m_arrowSuccessTextures;

	//Fail textures might not be necessary given it resets progress anyway. Perhaps have some effect trigger instead.
	[Export] private Godot.Collections.Array<Texture2D> m_arrowFailTextures;
	[Export] private HBoxContainer m_hBoxContainer;
	[Export] private PanelContainer m_panelContainer;
	[Export] private AnimationPlayer m_animationPlayer;
	private PackedScene m_arrowScene = ResourceLoader.Load<PackedScene>("res://ui/input_arrow.tscn");
	private InputArrow[] m_arrowNodes;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Connecting Signals to the PlayerInputComponent
		GameManager.Instance.playerAttackComponentTEMP.OnInputSequenceReady += InputSequenceReady;
		GameManager.Instance.playerAttackComponentTEMP.OnInputSequenceCorrect += InputSequenceCorrect;
		GameManager.Instance.playerAttackComponentTEMP.OnInputSequenceFailed += InputSequenceFailed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	private void InputSequenceReady(Godot.Collections.Array<DirectionInputs> InputSequence)
	{


		//Object pooling is probably a smarter approach to this.
		//Clear each child whenever a new sequence is loaded.
		Godot.Collections.Array<Node> children = m_hBoxContainer.GetChildren();
		
		
		if (children.Count > 0)
		{
			foreach (Control child in children){
				child.QueueFree();
			}
		}

		m_currentArrowIndex = 0;

		//Upon receiving an Input Sequence from the PlayerInputComponent
		m_arrowNodes = new InputArrow[InputSequence.Count];

		for (int i = 0; i < InputSequence.Count; i++){
			//Spawn an arrow
			InputArrow arrowNode = m_arrowScene.Instantiate() as InputArrow;
			arrowNode.direction = InputSequence[i];
			//Set texture based on its direction
			arrowNode.textureRect.Texture = m_arrowTextures[(int)arrowNode.direction];
		
			m_arrowNodes[i] = arrowNode;
			
			//Add to the UI
			m_hBoxContainer.AddChild(arrowNode);
		}
	}

	//If correct, highlight the arrow.
	private void InputSequenceCorrect()
	{
		m_arrowNodes[m_currentArrowIndex].textureRect.Texture = m_arrowSuccessTextures[(int)m_arrowNodes[m_currentArrowIndex].direction];
		if (m_currentArrowIndex < m_arrowNodes.Length - 1) m_currentArrowIndex++;
	}

	//If failed, reset arrows and shake it a bit.
	private void InputSequenceFailed()
	{
		while (m_currentArrowIndex >= 0)
		{
			m_arrowNodes[m_currentArrowIndex].textureRect.Texture = m_arrowTextures[(int)m_arrowNodes[m_currentArrowIndex].direction];
			if (m_currentArrowIndex == 0) break;
			m_currentArrowIndex--;
		}

		m_animationPlayer.Play("InputSequnceUI_FailShake");

	}
}
