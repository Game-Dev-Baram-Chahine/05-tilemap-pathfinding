# tilemap pathfinding and cave generation

## [Link To The Game On 'itch.io'](https://gamedevbc.itch.io/tiles-and-cavegen)

## You can choose between two scenes:
* State: we changed the state machine so it will have a hider function, so it will hide the enemy when he is far.
* Cave Generator: we implemented a BFS search algorythm that checks if the given map is valid for playing, and the user can change some of the parameters that generates the map.

![exampleGif](https://github.com/Game-Dev-Baram-Chahine/05-tilemap-pathfinding/blob/main/example.gif)

Scripts:
* [BFS](https://github.com/Game-Dev-Baram-Chahine/05-tilemap-pathfinding/blob/37ffb6019b90f1b16878c504873050c9ff1d92bf/Assets/Scripts/0-bfs/BFS.cs#LL55C23-L55C41)
* [Tilemap Cave Generator](https://github.com/Game-Dev-Baram-Chahine/05-tilemap-pathfinding/blob/main/Assets/Scripts/4-generation/TilemapCaveGenerator.cs)
* [Scene Selector](https://github.com/Game-Dev-Baram-Chahine/05-tilemap-pathfinding/blob/main/Assets/Scripts/5-gameMan/SceneSelector.cs)
* [Enemy Controller](https://github.com/Game-Dev-Baram-Chahine/05-tilemap-pathfinding/blob/main/Assets/Scripts/3-enemies/EnemyController3.cs)
