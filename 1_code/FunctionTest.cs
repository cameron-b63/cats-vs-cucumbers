using Godot;
using System;

public partial class FunctionTest : Node2D
{
	public override void _Ready()
	{
		GD.Print("===== Running FunctionTest.cs =====");

		// Print all children and their types
		foreach (Node child in GetChildren())
		{
			GD.Print($"Child: {child.Name}, Type: {child.GetType().Name}");
		}

		// Try to get Player node (even if it's Node2D) and print script info
		Node playerNode = GetNodeOrNull("../Player");
		if (playerNode == null)
		{
			GD.PrintErr("❌ Player node not found.");
			return;
		}

		GD.Print($"✅ Found node named 'Player'. C# type: {playerNode.GetType().Name}");

		// Try casting to your actual Player script class
		if (playerNode is Player player)
		{
			GD.Print($"✔ Cast succeeded: Player.Speed = {player.Speed}");
		}
		else
		{
			GD.PrintErr("❌ Cast to Player class failed. Node might not be using Player.cs correctly.");
		}		
		if (GetTree().Root.HasNode("Main_World/Player"))
		{
			player = GetNode<Player>("/root/Main_World/Player");
			GD.Print("Enemy found player in main world!");
		}
		else
		{
			GD.Print("⚠ Enemy did not find Main_World/Player — skipping.");
		}
	}
}
