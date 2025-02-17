### 1. **Controller.cs** (ParticleController)
This script manages the behavior and movement of individual particles. Key functionalities include:
- **Physics-based movement** using force calculations.
- **Separation, Alignment, and Cohesion behaviors** to simulate flocking.
- **Boundary constraints** to keep particles within a specified region.
- **Animator integration** for visual effects.
- **Sound playback logic** with probabilistic triggers.

### 2. **Generator.cs** (ParticleGenerator)
This script initializes and manages a collection of particles. Key functionalities include:
- **Instantiating particles** in random positions within defined limits.
- **Assigning random initial velocities** to the particles.
- **Maintaining a list of all generated particles** for reference by the `ParticleController` script.

### Features
- **Flocking Behavior**: Particles move dynamically based on their neighbors.
- **Boundary Handling**: Particles experience forces when approaching boundaries to keep them within the simulation area.
- **Realistic Motion**: Movement uses Euler integration and steering forces.
- **Animation & Sound**: Particle movement is complemented with animations and sound triggers.
