using Godot;
using System;

public partial class MageAttack : Area2D
{
	// Variables defining speed and direction
	public Vector2 Direction { get; set; } = Vector2.Right;

	[Export]
	public int Speed { get; set; } = 400;

	public override void _PhysicsProcess(double delta)
	{
		// Updates the position
		Position += Direction * Speed * (float)delta;
	}

	// If a cucumber hits the projectile, take damage
	private void _on_body_entered(Node body)
	{
		if (body is Cucumber1 cucumber)
		{
			GD.Print("Takes Damage");
			cucumber.CucumberTakeDamage(1);
			QueueFree(); // Destroy the projectile on hit
		}
	}
	
	// If the projectile leaves the screen, delete the projectile
	private void _on_visible_on_screen_enabler_2d_screen_exited()
	{
		QueueFree();
	}
}
