namespace GodotFlappyBird.scripts; 

public enum CollisionLayers {
    PLAYER = 1 << 0,
    OBSTACLES = 1 << 1,
    POINTS = 1 << 2,
    POWERUPS = 1 << 3,
}