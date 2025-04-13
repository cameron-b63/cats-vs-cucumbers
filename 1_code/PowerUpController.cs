using Godot;
using System;

public partial class PowerUpController : Node2D
{
	private PackedScene powerUpScene = GD.Load<PackedScene>("res://PowerUp.tscn");

	public override void _Ready()
	{
		// Example spawn position
		SpawnPowerUp(new Vector2(300, 500), powerUpType.Speed);
		SpawnPowerUp(new Vector2(500, 500), powerUpType.Jump);
	}
	
	public void SpawnPowerUp(Vector2 position, powerUpType type)
	{
		// creates power up according to specified location and type
		PowerUp powerUp = (PowerUp)powerUpScene.Instantiate();
		powerUp.Type = type;
		powerUp.Position = position;
		AddChild(powerUp);
	}
}
