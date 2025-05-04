using Godot;
using System;

public partial class Global : Node
{
	public static Global Instance { get; private set; }
	
	// Public variables to keep in the singleton
	public MainScene MainScene { get; set; }
	public CharacterBody2D PlayerNode { get; set; }
	public Node2D CurrentLevel { get; set; }
	public PackedScene CurrentLevelSource { get; set; }
	public PackedScene NextLevelSource { get; set; }
	
	// Default character
	public static string SelectedCharacter = "Mage";
	
	// Public primitives
	public bool ShouldLoadNextLevel { get; set; }	// On final level, set to false -> will go to win screen
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		GD.Print("Global singleton is ready to go.");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
