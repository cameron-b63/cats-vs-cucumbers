using Godot;
using System;

public partial class Level3 : Node2D
{
	[Export]
	private PackedScene _next_level_scene;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// There is no next level here.
		// Tell the game there aren't any more levels after this.
		Global.Instance.ShouldLoadNextLevel = false;
		Global.Instance.NextLevelSource = null;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
