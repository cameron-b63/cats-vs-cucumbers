using Godot;
using System;

public partial class CucumberBossController : Node2D
{
	[Export]
	private PackedScene cucumberBossScene;
		
	public override void _Ready()
	{
		SpawnCucumberBoss(new Vector2(2700, 445));
	}
	
	public void SpawnCucumberBoss(Vector2 position)
	{
		// creates the enemy 
		CucumberBoss enemy = (CucumberBoss)cucumberBossScene.Instantiate();
		enemy.Position = position;
		enemy.BossDefeated += OnBossDefeated;
		AddChild(enemy);
	}
	
	private void OnBossDefeated()
	{
		GD.Print("Spawning next level marker because boss is defeated.");
		var nextLevelController = GetParent().GetNode<NextLevelController>("NextLevelController");
		nextLevelController.SpawnLevelMarker(new Vector2(2700, 520));
	}
}
