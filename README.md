# AdamBorg_Assignment_Worms
 
FGP 2022 – Assignment #1, Local Multiplayer Shooter

By Adam Borg

__________________________________
Basic Gameplay

The game is a third person shooter that will be playable by 2(two) to 4(four) different players, each taking their turn on the same machine. 

Each player has their own turn to move, while a timer counts down how much longer their turn lasts. 

Firing their weapon will also decrease the amount of time the player has left. 

Reloading or throwing a grenade will end the current players turn. 

The game ends when only one player remains. 
__________________________________
Controls

WASD – Moves the current player

Q, E – Rotates the current player left and right, respectively

Spacebar – Makes the current player jump

Left Mouse Button – Fires the currently selected weapon (holding down makes it auto fire if the selected weapon is automatic)

Right Mouse Button – Throws a grenade

Mouse Wheel – Selects a different weapon (if one is available)

R – Reloads the currently selected weapon

T-Ends the current turn

__________________________________
Added Features and Desired Grade

Besides all the G level features, the following VG features have been added:

•	(VG, Small) Add main menu (start) scene and game over scene

-A main menu screen where you can select how many players there are and a screen when the game is over where you can select to play again, go to the main menu or quit the game.

-The amount of players is set with PlayerPrefs.

•	(VG, Large) Support up to 4 players (using the same input device taking turns)

-All the movement is done with one manager that handles all the players and switches the active player when a turn is finished.

•	(VG, Small) A worm can only move a certain range

-There is a timer that counts down when a turn starts. When the timer is finished, the current player’s turn ends. 

•	(VG, Small) A weapon can have ammo and needs to reload

-Different weapons have different amounts of ammo. If a weapon is reloaded the current player's turn ends.

•	(VG, Medium) The two types of weapons/attacks must function differently. 

-The player can pick up a new weapon, switch between them with the mouse wheel and fire it with the left mouse button.

•	(VG, Medium) Pickups 

-Pickups can randomly spawn at the end of a player’s turn. Picking them up can either restore a player’s health, increase their damage, give them more grenades or a new weapon.

I am aiming for VG (Väl godkänt) for this assignment.
