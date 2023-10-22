using Godot;
using GodotFlappyBird.scripts;

public partial class Player : RigidBody2D {

    [Signal] public delegate void CollidePipeEventHandler();
    [Signal] public delegate void GainPointEventHandler();

    public bool InputEnabled { get; set; }
    
    [Export] private float fallingGravityScale;
    [Export] private Vector2 startPos;
    [Export] private Material rainbowMaterial;
    
    private AnimatedSprite2D animatedSprite2D;

    private bool shouldReset;

    public override void _Ready() {
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite2D.Material = rainbowMaterial; // todo remove
    }
    
    public override void _Process(double delta) {
        if (!InputEnabled) return;
        
        if (Input.IsActionJustPressed("fly")) {
            animatedSprite2D.Play();
            GravityScale = 0;
        } else if (Input.IsActionJustReleased("fly")) {
            animatedSprite2D.Stop();
            GravityScale = fallingGravityScale;
        }

        if (Input.IsActionPressed("fly")) {
            ApplyForce(new Vector2(0, -200f));
        }
    }

    public override void _IntegrateForces(PhysicsDirectBodyState2D state) {
        base._IntegrateForces(state);
        if (shouldReset) {
            state.Transform = new Transform2D(0, startPos);
            // Position = startPos;
        }

        shouldReset = false;
    }

    public void Reset() {
        LinearVelocity = Vector2.Zero;
        GravityScale = fallingGravityScale;
        // GlobalPosition = startPos;
        // Position = startPos;
        // Transform = new Transform2D(0, startPos);
        InputEnabled = true;
        
        shouldReset = true;
    }

    private void OnAreaEntered(Area2D area) {
        switch (area.CollisionLayer) {
            case (uint) CollisionLayers.OBSTACLES:
                KillPlayer();
                break;
            case (uint) CollisionLayers.POINTS:
                AddPoint();
                break;
            case (uint) CollisionLayers.POWERUPS:
                CollectPowerup();
                break;
        }
    }

    private void AddPoint() {
        EmitSignal(SignalName.GainPoint);
    }
    
    private void KillPlayer() {
        InputEnabled = false;
        GravityScale = fallingGravityScale;
        ApplyImpulse(new Vector2(-300f, 0));

        EmitSignal(SignalName.CollidePipe);
    }

    private void CollectPowerup() {
        animatedSprite2D.Material = rainbowMaterial;
    }
}