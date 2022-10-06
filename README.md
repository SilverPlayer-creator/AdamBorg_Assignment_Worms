# AdamBorg_Assignment_Worms
 
FGP 2022 – Assignment #1, Local Multiplayer Shooter__
By Adam Borg__
__________________________________
Basic Gameplay<br />
The game is a third person shooter that will be playable by 2(two) to 4(four) different players, each taking their turn on the same machine.<br />
Each player has their own turn to move, while a timer counts down how much longer their turn lasts.<br />
Firing their weapon will also decrease the amount of time the player has left.<br /> 
Reloading or throwing a grenade will end the current players turn.<br />
The game ends when only one player remains.<br />
__________________________________
Controls<br />
WASD – Moves the current player<br />
Q, E – Rotates the current player left and right, respectively<br />
Spacebar – Makes the current player jump<br />
Left Mouse Button – Fires the currently selected weapon (holding down makes it auto fire if the selected weapon is automatic)<br />
Right Mouse Button – Throws a grenade<br />
Mouse Wheel – Selects a different weapon (if one is available)<br />
R – Reloads the currently selected weapon<br />
T-Ends the current turn<br />
__________________________________
Added Features and Desired Grade<br />

G Features<br />
•Turn Based Game -Two players can play using the same input device (keyboard) taking turns.<br />
•Terrain - Basic terrain using ProBuilder<br />
•Player - A player controls one worm using the character controller, and has hit points.<br />
•Camera - The camera follows the active player and follows when the player rotates.<br />
•Weapon System - Using mouse one fires the main weapon and using mouse two makes the player throw a grenade (if it has one). Both use add force.<br />
Besides all the G level features, the following VG features have been added:<br />
•	(VG, Small) Add main menu (start) scene and game over scene<br />
-A main menu screen where you can select how many players there are and a screen when the game is over where you can select to play again, go to the main menu or quit the game.<br />
-The amount of players is set with PlayerPrefs which the player manager references in the next scene to know how many players there are.<br />
•	(VG, Large) Support up to 4 players (using the same input device taking turns)<br />
-All the movement is done with one manager that handles all the players and switches the active player when a turn is finished.<br />
-The manager uses Unity's new input system and references the active player's character controller to move.<br />
•	(VG, Small) A worm can only move a certain range<br />
-There is a timer that counts down when a turn starts. When the timer is finished, the current player’s turn ends.<br />
-The timer also decreases when a player fires a weapon. Weapons have a variable for how much to decrease the time with which is passed through to the turn manager when a weapon fires.<br />
-After a certain amount of time has passed, the length of a turn decreases by one second, making the players have to think faster.<br />
•	(VG, Small) A weapon can have ammo and needs to reload<br />
-Different weapons have different amounts of ammo. If a weapon is reloaded the current player's turn ends.<br />
•	(VG, Medium) The two types of weapons/attacks must function differently.<br />
-The player can pick up a new weapon, switch between them with the mouse wheel and fire it with the left mouse button.<br />
-The default weapon fires automatically and the projectiles only damage on impact with a player. The rocket you can pick up damages on impact and also in a radius around the impact.<br />
-The two weapons have different ammo, fire types and damage. Both "spawn" projectiles using an object pooler to save resources.<br />
•	(VG, Medium) Pickups<br />
-Pickups can randomly spawn at the end of a player’s turn. Picking them up can either restore a player’s health, increase their damage, give them more grenades or a new weapon.<br />
-A pickup manager holds a dictionary of vector3's and booleans. If a position spawns a pickup, it's value is set to false until the item is picked up.<br />
I am aiming for VG (Väl godkänt) for this assignment.
