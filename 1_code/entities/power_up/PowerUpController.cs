using Godot;
using System;

// written by: Gabe
// refactored by: Cameron

public partial class PowerUpController : Node2D
{
	[Export]
	private PackedScene _powerUpScene;
	
	public override void _Ready()
	{
		// Example spawn position
		SpawnPowerUp(new Vector2(300, 500), powerUpType.Speed);
		SpawnPowerUp(new Vector2(500, 500), powerUpType.Jump);
	}
	
	public void SpawnPowerUp(Vector2 position, powerUpType type)
	{
		// creates power up according to specified location and type
		PowerUp powerUp = _powerUpScene.Instantiate<PowerUp>();
		powerUp.Type = type;
		powerUp.Position = position;
		AddChild(powerUp);
	}
}
