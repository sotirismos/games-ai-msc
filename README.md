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
