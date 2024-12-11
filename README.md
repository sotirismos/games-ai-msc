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

