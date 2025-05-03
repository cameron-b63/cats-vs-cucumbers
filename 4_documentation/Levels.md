# Levels
Levels are stored as Godot scenes. They consist of a `Node2D` root node, a `ParallaxBackground`, a `TileMapLayer`, a `Node2D` SpawnPoint, a `PowerUpController`, an `EnemyController`, a `NextLevelController`, and any other needed nodes.

## Map Layout
Map layout is non-random and stored as a Tilemap layout.

## Enemies
Enemies are spawned randomly using the `EnemyController`.

## Powerups
Powerups are spawned according to the `PowerUpController` implementation.

## NextLevel
The scratching post at the end of each level, when touched by the player, will start whatever `PackedScene` is loaded into the `Global` singleton's `NextLevelSource`.

## Level Loading
Level loading is managed through the use of a `LevelLayer` in the `MainScene`.