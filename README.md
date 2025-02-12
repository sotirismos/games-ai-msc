# Unity Particle System & Steering Behaviors

## Overview
This repository contains Unity C# scripts that demonstrate various particle system behaviors and steering mechanisms. The project is structured into two main sections: **Particles** and **Flocking**, each containing different simulations that illustrate fundamental concepts in physics-based movement and AI steering behaviors.

## Table of Contents
- [Requirements](#requirements)
- [Installation](#installation)
- [Project Structure](#project-structure)
  - [Particles](#particles)
    - [Random Particle System with Gravity, Wind, and Bouncing in a Cube](#random-particle-system-with-gravity-wind-and-bouncing-in-a-cube)
    - [Seek & Pursue Steering Behaviors in 2D](#seek--pursue-steering-behaviors-in-2d)
    - [Arrive Steering Behavior in 2D](#arrive-steering-behavior-in-2d)
    - [Repulsive Forces in a Sphere](#repulsive-forces-in-a-sphere)
  - [Flocking](#flocking)
    - [Bird Flocking](#bird-flocking)
- [Usage](#usage)

## Requirements
- Unity **2021.3** or later (earlier versions may work but are not tested)
- Basic understanding of Unity’s Particle System and AI Steering Behaviors
- C# scripting knowledge for Unity development

## Installation
1. Clone this repository to your local machine:
   ```sh
   git clone https://github.com/yourusername/Unity-Particle-Steering-Behaviors.git
   ```
2. Open Unity and load the project folder.
3. Ensure all required dependencies are installed in Unity.
4. Run the Unity scene to view the simulations in action.

## Project Structure
The project is divided into two main folders: `Particles` and `Flocking`, each containing specific behaviors and simulations.

### Particles
This section contains various particle system behaviors and physics-based interactions.

#### Random Particle System with Gravity, Wind, and Bouncing in a Cube
- This simulation demonstrates how particles move under the influence of **gravity**, **wind forces**, and **collision interactions** within a confined cubic space.
- The particles react dynamically to forces and bounce off the walls when they collide.
- Useful for understanding **force-based particle interactions**.

#### Seek & Pursue Steering Behaviors in 2D
- Implements **Seek** and **Pursue** steering behaviors, commonly used in AI movement systems.
- `Seek` behavior directs an agent toward a target’s current position.
- `Pursue` behavior predicts the target’s future position based on its velocity and moves accordingly.
- Ideal for AI enemy behaviors in 2D games.

#### Arrive Steering Behavior in 2D
- Implements an **Arrive** behavior, where an agent moves toward a target but slows down as it nears.
- Prevents overshooting and creates smooth deceleration.
- Useful for NPC navigation and autonomous movement systems.

#### Repulsive Forces in a Sphere
- Particles experience repulsion based on a force field, making them move away from a central object or defined area.
- Simulates effects like electric/magnetic repulsion or obstacle avoidance.
- Can be used for force field simulations and avoidance behaviors.

### Flocking
This section contains flocking behavior, which mimics how groups of entities (e.g., birds, fish, or crowds) move together in a natural way.

#### Bird Flocking
- Implements **Boid Algorithm** for simulating realistic bird flocking behavior.
- Three core rules:
  1. **Separation** - Avoid crowding neighbors.
  2. **Alignment** - Match velocity with nearby flock members.
  3. **Cohesion** - Move towards the average position of nearby flock members.
- Demonstrates how AI agents can move in realistic swarming formations.

## Usage
1. Open any of the Unity scenes corresponding to the behaviors.
2. Press **Play** to see the simulation in action.
3. Adjust parameters in the Inspector to observe changes in behavior (e.g., wind strength, pursuit speed, flocking separation distance, etc.).
