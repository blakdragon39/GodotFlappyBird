using Godot;

public partial class HUD : CanvasLayer {

    [Signal] public delegate void StartGameEventHandler();

    private Label messageLabel;
    private Label scoreLabel;
    private Button startButton;
    
    public override void _Ready() {
        messageLabel = GetNode<Label>("Message");
        scoreLabel = GetNode<Label>("ScoreLabel");
        startButton = GetNode<Button>("StartButton");
    }

    public void UpdateScore(int score) {
        scoreLabel.Text = score.ToString();
    }
    
    public void ShowMessage(string text, bool timeout = false) {
        messageLabel.Text = text;
        messageLabel.Show();
    }

    public void ShowStartButton() {
        startButton.Show();
    }
    
    // public async void ShowGameOver() {
    //     ShowMessage("Game Over", true);
    //
    //     await ToSignal(messageTimer, Timer.SignalName.Timeout);
    //     ShowMessage("Dodge the\nCreeps");
    //
    //     await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
    //     startButton.Show();
    // }
    

    private void OnStartButtonPressed() {
        startButton.Hide();
        EmitSignal(SignalName.StartGame);
    }
}