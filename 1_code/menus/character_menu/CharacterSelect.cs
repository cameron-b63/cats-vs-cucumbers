using Godot;
using System;

public partial class CharacterSelect : Control
{
	// Character buttons	
	private Button _swordsmanButton;
	private Button _berserkerButton;
	private Button _mageButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Links the buttons
		_swordsmanButton = GetNode<Button>("SwordsmanButton");
		_berserkerButton = GetNode<Button>("BerserkerButton");
		_mageButton = GetNode<Button>("MageButton");
		
		_swordsmanButton.Pressed += OnSwordsmanPressed;
		_berserkerButton.Pressed += OnBerserkerPressed;
		_mageButton.Pressed += OnMagePressed;
	}
	
	// Changes the character according to the button pressed
	private void OnSwordsmanPressed()
	{
		Global.SelectedCharacter = "Swordsman";
		Global.Instance.MainScene.ShowMainMenu();
	}
	
	private void OnBerserkerPressed()
	{
		Global.SelectedCharacter = "Berserker";
		Global.Instance.MainScene.ShowMainMenu();
	}
	
	private void OnMagePressed()
	{
		Global.SelectedCharacter = "Mage";
		Global.Instance.MainScene.ShowMainMenu();
	}
}
