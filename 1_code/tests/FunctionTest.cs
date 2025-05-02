using Godot;
using System;

// written by: Gabe
// debugged by: Cameron

public partial class FunctionTest : Node2D
{
	// All these tests are run when this Node is loaded.
	public override void _Ready()
	{
		GD.Print("===== Running FunctionTest.cs =====");
		// Make sure MainScene is available
		MainScene mainScene = Global.Instance.MainScene;
		if (mainScene == null)
		{
			GD.PrintErr("Main scene not set in Global singleton.");
			return;
		}
		
		// Try to get Player node and print script info
		CharacterBody2D playerNode = Global.Instance.PlayerNode;
		if (playerNode == null)
		{
			GD.PrintErr("Player node not set in Global singleton.");
			return;
		}
		
		// Make sure the correct level is loaded
		Node2D currentLevel = Global.Instance.CurrentLevel;
		if (currentLevel == null)
		{
			GD.PrintErr("Current Level not set in Global singleton.");
			return;	
		}
		
		GD.Print($"Global singleton is in order. Level {currentLevel.Name} properly loaded.");
	}
}
