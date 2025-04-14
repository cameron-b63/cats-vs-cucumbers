# Function Testing

This module verifies basic Player mechanics for the Cats vs. Cucumbers game. It includes a Godot test scene and script to validate script attachment, node detection, and simple behavior logic.

## How to Run
1. Open the project in Godot.
2. Press **F5** to run the scene.
3. Open the **Output** tab at the bottom of the editor to view test results.

## Expected Output
===== Running FunctionTest.cs ===== \
Child: Player, Type: Player \
✅ Found node named 'Player'. C# type: Player \
✔ Cast succeeded: Player.Speed = 300 \
Enemy did not find Main_World/Player — skipping \
===== FunctionTest complete =====

If any lines are missing or display errors, the node path or script reference may be incorrect.

## Included Files
- `FunctionTest.tscn`: Scene used to run the test.
- `FunctionTest.cs`: C# script that prints test results.
- `README3.txt`: Instructions for running and validating the test.