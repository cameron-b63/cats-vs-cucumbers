# Project Structure
Currently, all project files should simply go into root. However, eventually, each node should have its own folder with associated child nodes and C# scripts. Art should be in its own `art/` folder following the same tree structure.

# Godot Conventions
Godot is based on a set of scenes and nodes which can be combined in various ways. See [here](https://docs.godotengine.org/en/stable/getting_started/introduction/godot_design_philosophy.html) for more details.

## Naming
 - **Variables** should be named using camelCase.
 - **Classes and Members** should be named using PascalCase.
 - **private fields** should be named using camelCase, prefixed with an underscore (_camelCase).

More details can be found [here](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_style_guide.html).