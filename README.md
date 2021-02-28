# Button-Masher-3d-Alien-Game
Tapping button masher demo game, 50s Alien Retro, Unity 3D

Code: Roy Massaad
Design Aart : Nareg Kalenderian, Helen Sawma
Date: 2016
License : GPL

Code: Unity3d C# Scripts

**Description:**

This small demo game was created in a couple of days, art and code, for a University Workshop for last year design students. 

The game demo is name 'They came from Behind' in homage to old black and white science fiction movies.

We went over with the students with the basics of putting a game level together in Unity 3d with scripting, and modelling elements in Maya 3d to import as Prefabs.

The gameplay itself is button masher type, click/tap on the incoming aliens from all sides to destroy them with your turret.

All art style is retro inspired.

C# script code is applied to a couple of objects (turret, alien bots, spawner objects, menu)

The basic logic works the following, we detect where the mouse click/tap happens and convert it from 2d to 3d space.

Then we rotate animate the turret to that location, once the turret points correctly, it fires a beam/trace and detects if it hit a bot or wall.

If it it a wall it plays a particle effect, if it hit a bot the bot will play a hit animation.

The bots have life and speed value, they can be tweaked, by default they can take a few hits before de-spawning.

The alien bots come from a couple of predefined spawners that create them placed around the sceen from all angles towards the play, when spawned the move in the direction of the player.

If the come close enough to the player without being shot, they play the open fire animation.

The player doesnt lose if hit, we didnt program that part for the workshop demo.

The C# code isnt documented but its straightforward enough to understand without nonetheless.

The alien bots themselves were modelled rigged and skinned and animated by us to give the students a reference template also. 

This was original created on Unity 3d 5.6, but i updated it and tested on Unity 2020 before uploading to github



