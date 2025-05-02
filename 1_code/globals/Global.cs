using Godot;
using System;

public partial class Global : Node
{
	public PackedScene main_scene { get; set; }
	public CharacterBody2D player { get; set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Globals are ready to go.");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
