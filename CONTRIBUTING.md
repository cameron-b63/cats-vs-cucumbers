# NOTE
This is a student project. It is not open for contribution from anybody except the assigned group members until after the due date for the project has passed. Please do not attempt to contribute before this note has been removed unless you are one of the assigned group members.

# Contribution workflow
Create or find an issue to fix, and clone the repository. Create a new branch with the name `<your-github-name>/<description-of-work-on-branch>` to make ownership clear. Once work is completed, open a pull request to bring your work into `main` and assigning the other group members. It should be reviewed by all other team members before it is closed, so assign each of the other group members as **Reviewers** from inside GitHub.

For example: `supergithubguy32453426/fix-collision-bug` (be specific enough that we know what you're working on, but brief)

# Project Structure
Currently, all project files should simply go into root. However, eventually, each node should have its own folder with associated child nodes and C# scripts. Art should be in its own `art/` folder following the same tree structure.

# Godot Conventions
Godot is based on a set of scenes and nodes which can be combined in various ways. See [here](https://docs.godotengine.org/en/stable/getting_started/introduction/godot_design_philosophy.html) for more details.

## Naming
 - **Variables** should be named using camelCase.
 - **Classes and Members** should be named using PascalCase.
 - **private fields** should be named using camelCase, prefixed with an underscore (_camelCase).

More details can be found [here](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_style_guide.html).