using Godot;
using System;

public partial class Level2 : Node2D
{
	[Export]
	private PackedScene _next_level_scene;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Same level advance logic as level 1
		// Load Level 2 into the Global singleton
		if (_next_level_scene == null)
		{
			GD.PrintErr("Level 3 not linked to level 2.");
			return;
		}
		
		Global.Instance.NextLevelSource = _next_level_scene;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
