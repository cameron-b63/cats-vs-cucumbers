using Godot;
using System;

public partial class Level1Scene : Node2D
{
	[Export]
	private PackedScene _next_level_scene;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Load Level 2 into the Global singleton
		if (_next_level_scene == null)
		{
			GD.PrintErr("Level 2 not linked to level 1.");
			return;
		}
		
		Global.Instance.NextLevelSource = _next_level_scene;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
