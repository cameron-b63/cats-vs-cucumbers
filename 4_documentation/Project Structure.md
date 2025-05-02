# Project Structure
Cats Vs. Cucumbers is developed in accordance with Godot's [development guidelines](https://docs.godotengine.org/en/stable/tutorials/best_practices/project_organization.html).

## Directory Structure
This directory structure is adapted from a developer's suggestion [here](https://www.reddit.com/r/godot/comments/7786ee/what_the_best_folder_structure_for_developement/?rdt=47072)

 - `1_code`: The Godot project itself 
   - `entities`
     - `player`
       - `player.tscn`
       - `player.cs`
     - `enemies`
   - `menus`
   - `ui`
     - `theme_default`
       - `assets`
       - `theme_default.tres`
     - `font_uidefault.tres`
     - `cool_font.tff`
   - `scenes`
     - `common`
       - `assets`
     - `main`
       - `assets`
       - main.tscn
     - `level#`
       - `assets`
       - `level#.tscn`
 - `2_data_collection`: Information about data collection in the context of CS 3354 (the Software Engineering course this project was produced for)
 - `3_basic_function_testing`: Information about how to perform basic function testing
 - `4_documentation`: Documentation on various parts of the software package. You are here.