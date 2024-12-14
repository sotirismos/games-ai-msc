### **Particle Movement**
#### `ParticleGenerator.cs`
Unity component designed to generate particles dynamically during gameplay. It assigns random velocities to the generated particles.

##### Features
- **Dynamic Particle Generation**: Spawns particles every frame.
- **Randomized Velocities**: Assigns random velocities to particles within a specified range (`minVelocity` to `maxVelocity`).
- **Error Handling**: Logs an error if `minVelocity` is greater than `maxVelocity` to ensure valid configuration.

##### How It Works
1. **Validation**: The `Start` method checks that `minVelocity` is less than `maxVelocity` and logs an error if the condition is not met.
2. **Continuous Particle Spawning**: The `Update` method calls `SpawnParticle()` to create a new particle each frame.
3. **Random Velocity Assignment**: The `SpawnParticle()` method assigns random velocities to each particle, ensuring unique motion for every instance.

##### Usage
1. Attach the `ParticleGenerator` script to a GameObject in the scene.
2. Assign a prefab to the `particle` field in the Inspector. Ensure the prefab has a script (e.g., `ParticleController`) with a `velocity` property.
3. Set values for `minVelocity` and `maxVelocity` to define the range of possible velocities.
4. Run the scene to see particles spawn with randomized motion.

#### `ParticleDestroyer.cs`
Unity component designed to automatically destroy the GameObject it is attached to after a specified duration. This is particularly useful for temporary objects like particle effects or other transient elements in a scene.

##### Features
- **Customizable Lifetime**: Set the lifetime of the GameObject in seconds using the `lifetime` field in the Unity Inspector.
- **Automatic Cleanup**: Ensures that objects are destroyed automatically to free resources and maintain scene performance.

##### How It Works
- The `Destroy` function is called with a delay (`lifetime`) to schedule the destruction of the GameObject.
- The destruction is initialized in the `Start` method, ensuring it is only executed once when the object is created.

##### Usage
1. Attach the `ParticleDestroyer` script to a GameObject.
2. Set the `lifetime` value in the Inspector (e.g., 3.0 seconds).
3. The GameObject will automatically be destroyed after the specified time.

#### `ParticleController.cs`
Unity Component designed to simulate particle motion with collision detection and resolution in a 3D bounded environment. It accounts for gravity, drag, restitution, and the particle's interactions with the walls of a cube.

This script demonstrates foundational principles in physics-based animation, including the detection of collisions with planes, velocity corrections, and position adjustments.

##### Key Variables

- **`velocity`**: The current velocity of the particle.
- **`mass`**: The mass of the particle, influencing gravitational and drag forces.
- **`dragCoeff`**: The drag coefficient, which affects resistance to motion.
- **`restitutionCoeff`**: The coefficient of restitution, defining elasticity in collisions.
- **`cubeMinPosition` / `cubeMaxPosition`**: Boundaries of the cube within which the particle is constrained.

##### Core Methods

1. **`CalcNewState()`**:
   - Calculates the new position and velocity of the particle based on external forces.

2. **Collision Handling**:
   - **`CheckForCollisionWithPlane()`**: Detects and calculates collisions with the cube's planes.
   - **`DetectAndResolveCollision()`**: Iteratively resolves collisions to correct the particle's state.
   - **`ResolveCollisions()`**: Finalizes the particle's position and velocity post-collision.

3. **Utility Methods**:
   - **`GetFacetNormal()`**: Returns the normal vector of a cube's face based on its index.
   - **`GetFacetPoint()`**: Provides a reference point on a cube's face.
   - **`FindCollisionPosition()`**: Computes the intersection point of the particle's trajectory with a plane.
     
### **2D Seek & Pursue**
#### `TargetMovement.cs` 
Unity component designed to control target movement in a 2D plane (X-Z space) with fixed Y-axis positioning. The script moves a target dynamically over time with customizable velocity and duration.

##### Features

1. **Customizable Movement:**
   - Allows setting movement direction and speed via the `velocity` parameter (`Vector2`).
   - Duration of movement can be adjusted using the `duration` parameter (in seconds).

2. **Fixed Y-Position:**
   - Ensures the target remains at a constant height (Y-axis), maintaining motion only in the X-Z plane.

3. **Timed Movement:**
   - Movement stops automatically after the specified `duration`.

##### How It Works

1. **Initialization:**
   - The `Start` method stores the target's initial Y-position (`fixedY`) to keep it constant throughout the movement.

2. **Dynamic Movement:**
   - The `Update` method checks if the elapsed time is less than the `duration`:
     - Updates the target's position in the X-Z plane based on the velocity and elapsed time.
     - Keeps the Y-position constant (`fixedY`).
     - Increments the elapsed time.

3. **Automatic Stop:**
   - Once the elapsed time exceeds the `duration`, the movement ceases.

#### `SeekBehavior.cs`
Unity component designed to enable a GameObject to seek a target in the X-Z plane, maintaining a fixed Y-axis position. The script moves a seeker towards a specified target dynamically over a configurable duration and speed.

##### Features

1. **Target Seeking:**
   - The `target` parameter allows specifying the destination Transform that the seeker will move towards.

2. **Adjustable Speed and Duration:**
   - Movement speed can be customized using the `speed` parameter.
   - The seeker moves for a specified `duration` (in seconds) before stopping.

3. **Fixed Y-Position:**
   - Ensures the seeker remains at a constant height (Y-axis), focusing movement only in the X-Z plane.

##### How It Works

1. **Initialization:**
   - The `Start` method stores the seeker's initial Y-position (`fixedY`) to ensure it remains constant during movement.

2. **Seeking the Target:**
   - The `Update` method performs the following:
     - Checks if the elapsed time is less than the `duration`.
     - Calculates the direction from the seeker to the `target` in the X-Z plane, normalizing it to ensure consistent movement.
     - Moves the seeker towards the target using the `speed` parameter and `Time.deltaTime` to maintain frame-rate independence.
     - Keeps the Y-position constant (`fixedY`).
     - Updates the elapsed time.

3. **Automatic Stop:**
   - Movement ceases once the elapsed time exceeds the specified `duration`.

#### PursueBehavior.cs
Unity component designed to enable a GameObject to pursue a moving target by predicting its future position in the X-Z plane, maintaining a fixed Y-axis position. The script dynamically adjusts the pursuer's movement to intercept the target based on its velocity.

##### Features

1. **Predictive Pursuit:**
   - The `targetVelocity` parameter specifies the target's velocity, allowing the script to predict its future position for more accurate pursuit.

2. **Adjustable Speed and Duration:**
   - The pursuer's movement speed can be customized using the `speed` parameter.
   - The pursuer moves for a specified `duration` (in seconds) before stopping.

3. **Fixed Y-Position:**
   - Ensures the pursuer remains at a constant height (Y-axis), focusing movement only in the X-Z plane.

##### How It Works

1. **Initialization:**
   - The `Start` method stores the pursuer's initial Y-position (`fixedY`) to ensure it remains constant during movement.

2. **Pursuit with Prediction:**
   - The `Update` method performs the following:
     - Checks if the elapsed time is less than the `duration`.
     - Predicts the target's future position in the X-Z plane based on its current position and `targetVelocity`.
     - Calculates the direction from the pursuer to the predicted position, normalizing it to ensure consistent movement.
     - Moves the pursuer towards the predicted position using the `speed` parameter and `Time.deltaTime` to maintain frame-rate independence.
     - Keeps the Y-position constant (`fixedY`).
     - Updates the elapsed time.

3. **Automatic Stop:**
   - Movement ceases once the elapsed time exceeds the specified `duration`.

   - Set the `speed` parameter to control the pursuer's movement speed.
   - Specify the `duration` to define how long the pursuer should move towards the target.

