using Godot;
using System;

public partial class MainMenu : Control
{
	// export the PackedScene
	[Export]
	public PackedScene Level1Scene;
	
	private Button _playButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playButton = GetNode<Button>("PlayButton");
		// Put our OnPlayPressed function in the Button's action
		_playButton.Pressed += OnPlayPressed;
		
		GD.Print("Main menu is ready.");
	}
	
	private void OnPlayPressed()
	{
		// use the exported level 1 scene
		if (Level1Scene == null)
		{
			GD.PrintErr("Level1Scene not available");
			return;
		}
		
		// grab MainScene from the Global singleton (that's why we put it there)
		MainScene main = Global.Instance.MainScene;
		if (main == null)
		{
			GD.PrintErr("MainScene not registered on Global. Check MainScene's .tscn.");
			return;
		}
		
		// Start level 1
		main.StartLevel(Level1Scene);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
