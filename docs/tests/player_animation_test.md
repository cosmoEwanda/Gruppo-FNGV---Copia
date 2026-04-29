# Manual Test Plan: Player Animations

This document outlines the manual verification steps to ensure the **Player** animation systems are correctly integrated, visually functional, and robust against manual or external changes.

## Warrior Attack Animation
### 1. Setup & Dependency Verification
**Goal:** Ensure all Attack Animations Used Are Working and all scene references are correctly linked before testing logic.

* **Component Check**:`Animator` is Attached to the object `character-human` or any variation of it
* **References**: `Animator` has the `Controller` value linked to `Player Attack` or equivalent `Controller`
* **State machine**: open `Player Attack` or equivalent `Controller` via the `Animator` tab and verify that it is not empty

### 2. Warrior Attack animation
**Goal:** Confirm that the warrior attack animations are working as intended in play mode

* **start the game:** select the `Assets/Scenes/TestScenes/AttackEntity` and start it with the *play* button, upon start no attack animation should play
* **idle attack:** with the game still running open the `Animator` tab. Upon pressing the attack button (and/or keeping it pressed) and standing still, you should be able to see the signals in the state machine move randomly to all possible attack states, starting always with `attack-melee-right`. The animation changes should feel seamless.
* **on-move attack:** with the game still running open the `Animator` tab. Upon pressing the attack button (and/or keeping it pressed) and moving, you should be able to see the signals in the state machine move  starting always with `attack-melee-right` to `attack-melee-left` (or `Empty` if you are not fast enough).The animation changes should feel seamless.
