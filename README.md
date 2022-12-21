# KeyStrokes
KeyStrokes an auto-runner, fantasy themed game. 

## Game Play
The player will type out a word that will appear in their view (left/right/forward) to control the character's direction of movement. The character will then continuously move in the new direction. The key elements are the main character which includes hit points (health), a name, and statistics. There will also be obstacles such as blockades or walls that cause the character's hit points to decrease upon collision. Enemies will also be generated to track the character's movement and attempt to hit them. Items will also appear that is centered around abilities like restoring health, limited invincibility, faster speed, and a locking mechanism that prevents the character from choosing a direction. The in-game goal is to get as many points as possible before failure.  

## Educational Component 
There is an educational component, with the target audience being anyone wanting to improve their typing speed/skills. Outside the game, typing and vocabulary improvement is desired. As mentioned before, words will appear that the player must type to control the character. 

## Engine
The Unity Engine will be used for this project.

# Contributions

## Donald Hurld
All Environment design: Floor, walls, shields, flags (and physics), torches (particle emitters), smoke wall (hides propogation), tutorial sign, all lighting.
Sounds for objects, background music.
Non-Enemy Obstacles: Columns 1 and 2, fire, barrel, chest (and all associated sounds).
Menu: Titlecard video
All of MapPropogate.cs, LevelBoundry.cs, and TutorialSign.cs
runner.cs:77-80(also used:98-101,122-125),83-88,(92/102, 116/126 boundry checks), all from line 180 until the end.

## Eric Capri
Character: All 6 player models, their walk and run animations.
Menu: Everything minus the titlecard
NPCs: Skeleton, Person, Dragon (models, animations).

## Hongxiang Wang
Character UI
All of AI.cs, TyperForward.cs, TyperLeft.cs, and TyperRight.cs, type to control mechanic.
runner.cs:56-76,93-97,108-112,117-121,129-134, 163-175.