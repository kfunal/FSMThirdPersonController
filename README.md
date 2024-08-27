# Third Person Controller with Finite State Machine

This project is an example of a Third Person Controller created using Unity. A Finite State Machine (FSM) structure is implemented to manage the transitions between different states of the character, such as running, walking, and jumping. Additionally, there is an aiming feature that allows the character to aim in a certain direction, with the character's movement adjusting dynamically based on the aim direction. Future updates to the project will include shooting mechanics, weapon placement using IK (Inverse Kinematics) for hand positioning, the ability to drop weapons from the character's hands, interacting with objects, and incorporating sound effects.

# Features
-> **Finite State Machine (FSM):** Manages transitions between different states of the character.

-> **Third Person Controller:** Camera and movement controls provide a third-person gameplay experience.

-> **Jumping, Running, Walking:** Basic character movement mechanics.

-> **Aiming Feature:** Users can make the character aim in a specific direction, with the character rotating accordingly. The movement adjusts dynamically based on the aim direction.

-> **Shooting (Planned):** The character will be able to shoot using weapons.

-> **Weapon Placement and Dropping (Planned):** Weapons will be placed in the character's hands using IK (Inverse Kinematics) and can be dropped when needed.

-> **Object Interaction (Planned):** The character will be able to interact with various objects in the environment.

-> **Sound Effects (Planned):** Adding sound effects to enhance the gameplay experience.

-> **Modular and Extensible Structure:** Easily add new states or customize existing ones.

-> **Character and Animations from Mixamo:** The character model and animations used in this project are sourced from [Mixamo](https://www.mixamo.com).

## Usage
**Clone the repository:** 

```
git clone https://github.com/kfunal/FSMThirdPersonController.git
```

**Install Git LFS:** To handle large files managed by Git LFS, ensure you have Git LFS installed. You can install it using the following command:
git lfs install

**Open with Unity:** Open the project in the Unity Editor.

**Pull Large Files:** After cloning, Git LFS will automatically fetch the large files. If needed, you can manually pull them using:

```
git lfs pull
```

**Play:** Press the Play button to run the game.

## Installation
**Unity Version:** This project was created using Unity version 2022.3.13f1. It may be compatible with other versions, but using the specified version is recommended for the best experience.

**Required Packages:** Cinemachine, Input System.

# Contributing

If you want to contribute to the project, please open an issue first.

Pull requests are welcome.

# License

This project is licensed under the MIT License. See the LICENSE file for more information.

# GamePlay
WASD -> Walk // Space -> Jump // Shift -> Run // Right Mouse Click -> Aim

https://github.com/user-attachments/assets/32501c9b-7951-4a09-ac17-dfb3d314d6aa
