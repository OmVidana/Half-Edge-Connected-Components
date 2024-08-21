# Half-Edge Connected Components Mesh Renderer in Unity

Final Project for **_Computation Geometry_** course.

## About

This project contains two scenes:

* **Main Menu:** Continue or exit menu.
* **Connected Components:** Main playing scene. Here the user can open .obj via file selection, count its faces and vertex and then render it and count how many connected components does it has.

This project implements Half-Edge data structure in order to iterate faster between meshes loaded from an .obj.

The main playing scene has the player as a movable camera to view the Mesh and a interactive menu for loading and viewing the mesh data. The user cannot move while the menu is active. 

There is information about the player inputs in the scene.

**Unity Version:** _2022.3.36f1_

### Disclaimer

* For files over 75MB wait a few seconds in order to properly load them. This program can manage large .obj sizes but it requieres a few seconds more.

### Credits

This project utilizes the StandaloneFileBrowser unitypackage from **Gökhan Gökçe**:

https://github.com/gkngkc/UnityStandaloneFileBrowser/tree/master/Assets/StandaloneFileBrowser

As a tool for loading files from the system.