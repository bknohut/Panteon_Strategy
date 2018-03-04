Strategy Game

The game is developed with Unity 2017.3.1f1, using Visual Studio 2017.

The game board is a grid of height 15, width 15. The grid is generated at the beginning of the game.
The production menu is an infinite scrollbar. On the scrollbar there are two items in each row.
	The items are draggable on the grid.
	If dragged anywhere out of the grid or another object, game warns the player.
	If the dragged place is available, an object is created on the grid part shown.
The information menu is consists of four parts.
	A text object is at the top and shows the clicked objects name.
	There is the image to show the image of the object.
	There is a spawn button to spawn unit when clicked.
	Lastly, the status text is to notify the user when a unit levels up.

A barracks can spawn units as long as it has an empty tile exactly one tile around it.
	The barracks will spawn a unit, on each button click.
	If all the tiles are occupied with buildings, the barracks cannot spawn units.
	If all the tiles are occupied, but there at least exists one unit around the barracks, that units level is increased by one.

Tanks are able to move around the grid using the unoccupied tiles.
	If the right clicked tile has a tank on it, the two tanks are merged into one. The tank on the destination is leveled up by the level of the source tank.

Considering the different aspect ratios, I tried my best to fit the game into different aspect ratios.

Thank you.