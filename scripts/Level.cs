using System;
using Godot;
using GodotFlappyBird.scripts;

public partial class Level : Node {

    [Export] private Timer spawnTimer;
    [Export] private PackedScene pipeObstacle;
    [Export] private PackedScene powerup;
    [Export] private float scrollSpeed;
    
    private int obstaclesSinceLastPowerup;
    private float _scrollSpeed;
    
    public override void _Process(double delta) {
        ForChildren(node => {
            var node2D = node as Node2D;
            if (node2D == null) return;
            
            if (HasLeftScreen(node2D)) {
                node2D.QueueFree();
                return;
            }
        
            node2D.Position = new Vector2(node2D.Position.X - (float) delta * _scrollSpeed, node2D.Position.Y);
        });
    }

    public void StartSpawning() {
        ForChildren(node => node.QueueFree());
        _scrollSpeed = scrollSpeed;
        SpawnPipe();
        spawnTimer.Start();
    }

    public void StopSpawning() {
        spawnTimer.Stop();
        _scrollSpeed = 0;
    }

    private void OnSpawnTimerTimeout() {
        SpawnPipe();
    }

    private void SpawnPipe() {
        var (width, height) = GetViewport().GetVisibleRect().Size;
        var halfHeight = height / 2;
        var openingRange = halfHeight - 70;
        var opening = halfHeight + GD.RandRange(-openingRange, openingRange);

        var obstacle = pipeObstacle.Instantiate<Node2D>();
        obstacle.Position = new Vector2(width + 64, (float) opening);

        AddChild(obstacle);
        
        obstaclesSinceLastPowerup += 1;
        
        TrySpawnPowerup(obstacle.Position);
    }

    private void TrySpawnPowerup(Vector2 obstaclePosition) {
        // if (obstaclesSinceLastPowerup < 3) return;

        // var spawnPowerup = GD.Randi() % 3 == 0;
        // if (!spawnPowerup) return;

        SpawnPowerup(obstaclePosition);
    }

    private void SpawnPowerup(Vector2 obstaclePosition) {
        var newPowerup = powerup.Instantiate<Powerup>();
        newPowerup.Position = obstaclePosition; // todo possibility to move forward, up, down
        AddChild(newPowerup);
        obstaclesSinceLastPowerup = 0;
    }
    
    private bool HasLeftScreen(Node2D node) {
        return node.Position.X < -64;
    }

    private void ForChildren(Action<Node> action) {
        foreach (var child in GetChildren()) {
            action.Invoke(child);
        }
    }
}
