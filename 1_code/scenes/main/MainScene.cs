using Godot;
using System;

// written by: Cameron

public partial class MainScene : Node
{
	[Export]
	public PackedScene PlayerScene;		// Store the player's scene
	
	[Export]
	public PackedScene MainMenuScene;	// Store the main menu scene
	
	[Export]
	public PackedScene WinScreen;	// Store the win screen
	
	[Export]
	public PackedScene HUDScene;	// Store the HUD here
	
	[Export]
	public PackedScene CharacterSelectScene; // Store the character select scene
	
	private Control _menuInstance;	// Store the instantiated menu scene
	private Node2D _levelInstance;	// Instantiated current level
	private Control _hudInstance;	// Instantiated HUD
	private Control _characterSelectInstance; // Instantiated character select
	
	// Make the child nodes of the main scene publicly available
	[Export]
	public Node MenuLayer;
	
	[Export]
	public Node LevelLayer;
	
	[Export]
	public Node HUDLayer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Register with the Global singleton
		Global.Instance.MainScene = this;
		Global.Instance.ShouldLoadNextLevel = false;
		
		// The second the game is loaded, we want to show the main menu (for obvious reasons).
		ShowMainMenu();
	}
	
	// As the name suggests, responsible for showing the main menu
	public void ShowMainMenu()
	{
		ClearAll();
		// Instantiate the menu	
		_menuInstance = MainMenuScene.Instantiate<Control>();
		
		// Put menu instance into the menu layer
		MenuLayer.AddChild(_menuInstance);
		
		// Let game know it's ok to load the next level
		Global.Instance.ShouldLoadNextLevel = true;
	}
	
	public void ShowWinScreen()
	{
		ClearAll();
		
		// Instantiate the win screen
		_menuInstance = WinScreen.Instantiate<Control>();
		
		// Load the win screen into the MenuLayer
		MenuLayer.AddChild(_menuInstance);
	}
	
	// As the name suggests, responsible for starting a level.
	// The level to start is passed as a PackedScene.
	public void StartLevel(PackedScene levelScene)
	{
		ClearAll();
		Global.Instance.CurrentLevelSource = levelScene;
		// Instantiate the level
		_levelInstance = levelScene.Instantiate<Node2D>();
		// Register the level with the Global singleton
		Global.Instance.CurrentLevel = _levelInstance;
		
		// Instantiate the player to play in the level
		CharacterBody2D player = PlayerScene.Instantiate<CharacterBody2D>();
		
		// Position player at the level's spawnpoint if available
		if (_levelInstance.HasNode("SpawnPoint"))
		{
			player.Position = _levelInstance.GetNode<Node2D>("SpawnPoint").GlobalPosition;
		}
		
		// Put the player on the level's tree
		_levelInstance.AddChild(player);
		// Register the player with the Global singleton
		Global.Instance.PlayerNode = player;
		
		// Add the level to the tree
		LevelLayer.AddChild(_levelInstance);
		
		// Instantiate the HUD
		_hudInstance = HUDScene.Instantiate<Control>();
		HUDLayer.AddChild(_hudInstance);
	}
	
	public void GetCharacterSelect()
	{
		ClearAll();
		
		// Instantiate the menu	
		_characterSelectInstance = CharacterSelectScene.Instantiate<Control>();
		
		// Put character selection instance into the menu layer
		MenuLayer.AddChild(_characterSelectInstance);
	}
	
	// Clear all layers.
	private void ClearAll()
	{
		FreeChildren(MenuLayer);
		FreeChildren(LevelLayer);
		FreeChildren(HUDLayer);
	}
	
	// Helper that queues each child of the passed layer for freeing (clearing)
	private void FreeChildren(Node layer)
	{
		if(layer == null) return;
		
		// Walk backwards since child count changes as we go
		for (int i = layer.GetChildCount() - 1; i >= 0; i--)
		{
			var child = layer.GetChild(i);
			child.QueueFree();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
