# witcher

I created this project to see if I can build basic mechanics of Witcher 3 game. 
Of course there are tons of things missing! The goal of this project was to create gameplay systems that can be flexible and easily extendable to allow easy implementation of missing features. Obviously, because of that the graphics side is not really impressive.
What have I done?
- resources system (health, energy)
  - characters dies once health is at 0
- system for skill creation
  - all 5 signs - Aard, Axii, Igni, Quen and Yrden are implementented
  - melee and ranged attacks are also implemented using that system
  - skill usage is interruptible
- effects system - allows for easy creation of effects that can be applied to objects in game, like burn, stun, damage/healing over time etc
- simple combat detection and handling events on combat enter / exit (like reducing regen, going back to spawn point)
- NPC AI System 
  - separates target selection from action selection, this allows us to create characters using any combination of target selection and target action that we have implemented. Should this be not enough for implementing more complicated behaviours, it should be easy to split logic into more than just "target selection" and "acting" to allow that.
  - there are 5 NPCs in game, each created by just attaching 2 or 3 scripts to each, which are reused between them
    - attacking us when we are close, but stopping once we run away (range based target detection, attack action with 1 skill)
    - same as above, but running back to spawn point after we run away (range based target detection, attack action with 1 skill, after combat going back)
    - following us no matter what once we get in his attack range, not regenerating in combat (1st seen target detection, attack action with 1 skill, in combat regeneration removal)
    - running away from us (range based target detection, run away action)
    - shooting at us until we get into melee range and then attacking us with a sword (range based target detection, attack action with 2 skills)  
- targeting system
- UI system for existing mechanics
- movement system, which allows objects to travel in selected direction, towards certain point, follow certain object

# What to see in game?
- There is a hanging skeleton at start, use Aard on it and try to make https://youtu.be/iA_Y-R9dbHA?t=1m39s happen
- Get onto uneven ground. Try using Yrden. Yay! It works as expected (doesn't appear mid air, under ground etc)
- Fight skeletons, try all the signs against them, have fun!
