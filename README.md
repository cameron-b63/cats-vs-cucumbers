# Dependencies
Cats vs. Cucumbers is written as a .NET Godot project using C#.

# Player
The `Player` node contains a `CollisionShape2D` and an `AnimationSprite2D` to manage collisions with enemies/projectiles and animation respectively. It uses standard left-right movement, along with a jump action.

# Enemy
The `Enemy` node contains a `CollisionShape2D` and a `Sprite2D` to represent the enemy and manage collisions with the player. Enemies operate in two modes: **patrol** and **chase**, determined by the distance between the enemy and the player.

# Power-Ups
Power-ups are nodes with a `CollisionShape2D` and a `Sprite2D` to represent the power-up. When a player collects a power-up, they temporarily gain a specific effect (e.g., speed boost, jump boost), which reverts after a defined duration.

# Next Level
The **next level marker** is an indicator object that shows the location the player must reach after defeating the level boss. Interacting with the marker routes the player to the next level in the game.
