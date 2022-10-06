# AdamBorg_Assignment_Worms
 
FGP 2022 – Assignment #1, Local Multiplayer Shooter__
By Adam Borg__
__________________________________
Basic Gameplay<br />
The game is a third person shooter that will be playable by 2(two) to 4(four) different players, each taking their turn on the same machine.<br />
Each player has their own turn to move, while a timer counts down how much longer their turn lasts.__
Firing their weapon will also decrease the amount of time the player has left.__ 
Reloading or throwing a grenade will end the current players turn.__
The game ends when only one player remains.__
__________________________________
Controls__
WASD – Moves the current player__
Q, E – Rotates the current player left and right, respectively__
Spacebar – Makes the current player jump__
Left Mouse Button – Fires the currently selected weapon (holding down makes it auto fire if the selected weapon is automatic)__
Right Mouse Button – Throws a grenade__
Mouse Wheel – Selects a different weapon (if one is available)__
R – Reloads the currently selected weapon__
T-Ends the current turn__
__________________________________
Added Features and Desired Grade__

G Features__
•Turn Based Game -Two players can play using the same input device (keyboard) taking turns.__
•Terrain - Basic terrain using ProBuilder__
•Player - A player controls one worm using the character controller, and has hit points.__
•Camera - The camera follows the active player and follows when the player rotates.__
•Weapon System - Using mouse one fires the main weapon and using mouse two makes the player throw a grenade (if it has one). Both use add force.
Besides all the G level features, the following VG features have been added:__
•	(VG, Small) Add main menu (start) scene and game over scene__
-A main menu screen where you can select how many players there are and a screen when the game is over where you can select to play again, go to the main menu or quit the game.__
-The amount of players is set with PlayerPrefs which the player manager references in the next scene to know how many players there are.__
•	(VG, Large) Support up to 4 players (using the same input device taking turns)__
-All the movement is done with one manager that handles all the players and switches the active player when a turn is finished.__ 
-The manager uses Unity's new input system and references the active player's character controller to move.
•	(VG, Small) A worm can only move a certain range__
-There is a timer that counts down when a turn starts. When the timer is finished, the current player’s turn ends.__ 
-The timer also decreases when a player fires a weapon. Weapons have a variable for how much to decrease the time with which is passed through to the turn manager when a weapon fires.__
-After a certain amount of time has passed, the length of a turn decreases by one second, making the players have to think faster.__
•	(VG, Small) A weapon can have ammo and needs to reload__
-Different weapons have different amounts of ammo. If a weapon is reloaded the current player's turn ends.__
•	(VG, Medium) The two types of weapons/attacks must function differently.__
-The player can pick up a new weapon, switch between them with the mouse wheel and fire it with the left mouse button.__
-The default weapon fires automatically and the projectiles only damage on impact with a player. The rocket you can pick up damages on impact and also in a radius around the impact.__
-The two weapons have different ammo, fire types and damage. Both "spawn" projectiles using an object pooler to save resources.__
•	(VG, Medium) Pickups__
-Pickups can randomly spawn at the end of a player’s turn. Picking them up can either restore a player’s health, increase their damage, give them more grenades or a new weapon.__
-A pickup manager holds a dictionary of vector3's and booleans. If a position spawns a pickup, it's value is set to false until the item is picked up.__
I am aiming for VG (Väl godkänt) for this assignment.
