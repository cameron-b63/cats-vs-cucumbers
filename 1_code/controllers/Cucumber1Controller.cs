using Godot;
using System;

// written by: Gabe
// debugged by: Cameron

public partial class Cucumber1Controller : Node2D
{
	[Export]
	private PackedScene cucumber1Scene;
	private Random rng = new Random();
	
	public override void _Ready()
	{
		// randomized enemy count
		int enemyCount = rng.Next(2, 4);
		float startX = 800f;              // where spawning begins (left)
		float endX = 2000f;               // where spawning ends (right)
		
		float spacing = (endX - startX) / (enemyCount - 1);
		
		// spawns enemies in spaced out locations
		for (int i = 0; i < enemyCount; i++)
		{
			// even base spacing
			float baseX = startX + i * spacing;
			
			// offset for more randomization
			float jitter = (float)rng.Next(-20, 21); // +/- 20px max wiggle

			float finalX = baseX + jitter;
	
			SpawnCucumber1(new Vector2(finalX, 540));
		}
	}
	
	public void SpawnCucumber1(Vector2 position)
	{
		// creates the enemy 
		Cucumber1 enemy = (Cucumber1)cucumber1Scene.Instantiate();
		enemy.Position = position;
		
		AddChild(enemy);
	}
}
