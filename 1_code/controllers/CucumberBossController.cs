using Godot;
using System;

public partial class CucumberBossController : Node
{
	[Export]
	private PackedScene cucumberBossScene;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SpawnCucumberBoss(new Vector2(2700, 510));
	}

	public void SpawnCucumberBoss(Vector2 position)
	{
		CucumberBoss boss = (CucumberBoss)cucumberBossScene.Instantiate();
		boss.Position = position;
		boss.BossDefeated += OnBossDefeated;
		AddChild(boss);
	}
	
	private void OnBossDefeated()
	{
		var nextlevelController = GetParent().GetNode<NextLevelController>("NextLevelController");
		nextlevelController.SpawnLevelMarker(new Vector2(2700, 500));
	}
}
