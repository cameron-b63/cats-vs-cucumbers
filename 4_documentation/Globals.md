# Globals
Global state is managed using the `Global` singleton, defined in `globals/Global.cs`. It is auto-loaded into Godot when the game is run, and allows universal access to certain objects and functionality.

## Main Scene
The `MainScene` containing all layers.

## CurrentLevelScene
The `Node2D` corresponding to the root of the current level.

## CurrentLevelSource
The `PackedScene` corresponding to the current level.

## NextLevelSource
The `PackedScene` corresponding to the next level to be loaded upon completion of the current level.

## Attributes
 - `ShouldLoadNextLevel`: Represents whether the next level should be loaded - if not, a death screen or win screen can be loaded instead.