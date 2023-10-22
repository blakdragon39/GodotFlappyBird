using Godot;

public partial class Main : Node {
    
    private int score;
    
    private Player player;
    private HUD hud;
    private Level level;
    
    // private AudioStreamPlayer music;
    // private AudioStreamPlayer deathSound;
    
    public override void _Ready() {
        player = GetNode<Player>("Player");
        hud = GetNode<HUD>("HUD");
        level = GetNode<Level>("Level");
    }
    
    private void NewGame() {
        score = 0;
        player.Reset();
        
        hud.ShowMessage("");
        hud.UpdateScore(score);
        level.StartSpawning();
    }

    private void AddPoint() {
        score += 1;
        hud.UpdateScore(score);
    }
    
    // private void OnMobTimerTimeout() {
    //     Mob mob = MobScene.Instantiate<Mob>();
    //
    //     spawnPath.ProgressRatio = GD.Randf();
    //     
    //     float direction = spawnPath.Rotation + Mathf.Pi / 2;
    //     direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
    //
    //     var velocity = new Vector2((float) GD.RandRange(150.0, 250.0), 0);
    //
    //     mob.Position = spawnPath.Position;
    //     mob.Rotation = direction;
    //     mob.LinearVelocity = velocity.Rotated(direction);
    //
    //     AddChild(mob);
    // }
    
    private void GameOver() {
        hud.ShowMessage("Game Over!");
        hud.ShowStartButton();
        level.StopSpawning();
    }
}