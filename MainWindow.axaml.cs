using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace zad2;

public partial class MainWindow : Window {
  private Game GameLogic;
  private Bot PlayerAI;
  private Button[,] Buttons = new Button[3,3];
  private int MoveCount;
  private bool IsPVE = true, ReverseTurn = true;
  private uint ScoreX, ScoreO, Draws;

  public MainWindow() {
    InitializeComponent();
    System.Console.WriteLine($"BoardGrid null? {BoardGrid == null}");
    StartNewGame();
  }

  private void StartNewGame() {
    GameLogic = new Game(ReverseTurn);
    PlayerAI = new HeuristicBot(GameLogic);
    MoveCount = 0;
    BoardGrid.IsHitTestVisible = true;
    BoardGrid.Children.Clear();
    TurnText.Text = ReverseTurn ? "X" : "O";
    ReverseTurn = !ReverseTurn;
    ProceedGameMatrix();
  }
  private void ProceedGameMatrix() {
    //UniformGrid myBoard = this.FindControl<UniformGrid>("BoardGrid");
    for (int i = 0; i < 3; ++i) {
      for (int j = 0; j < 3; ++j) {
        Button btn = new Button();
        int r = i, c = j;
        btn.Content = "";
        btn.FontSize = 20;
        btn.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
        btn.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch;
        btn.Click += (sender, args) => OnCellClick(r, c);

        Buttons[r, c] = btn;
        BoardGrid.Children.Add(btn);
      }
    }
  }
  private void OnCellClick(int r, int c) {
    if (!GameLogic.IsEmpty(r, c)) { return; }
    
    //gracz
    (int, int[]) status = PlayMove(r, c);
    if (CheckEnd(status.Item1, status.Item2)) { WinPathShow(status.Item2); return; }
    
    //bot
    if (IsPVE) {
        int[] move = PlayerAI.GetMove();
        (int, int[]) botStatus = PlayMove(move[0], move[1]);
        if (CheckEnd(botStatus.Item1, botStatus.Item2)) { return; }
    }
    
    //wsk tury
    TurnText.Text = GameLogic.GetPlayerOne() ? "X" : "O";
  }

  private (int, int[]?) PlayMove(int r, int c) {
    bool isX = GameLogic.GetPlayerOne();
    (int, int[]) status = GameLogic.SetMove(r, c);

    Buttons[r, c].Content =isX ? "X" : "O";
    MoveCount++;
    return status;
  }
  private bool CheckEnd(int s, int[] winPath) {
    //var text = this.FindControl<TextBlock>("TurnText");
    if (s is -1 or 1) {
      TurnText.Text = GameLogic.GetPlayerOne() ? "wygral X" : "wygral O";
      if (s == -1) { ScoreO++; }
      else { ScoreX++; }
      WinPathShow(winPath); //BoardGrid.IsEnabled = false;
      BoardGrid.IsHitTestVisible = false;
    }
    else if (MoveCount == 9) {
      TurnText.Text = "remis";
      Draws++;
      BoardGrid.IsHitTestVisible = false;
    }
    else {return false;}
    ScoreText.Text = $"X: {ScoreX} | O: {ScoreO} | Remisy: {Draws}";
    //var grid = this.FindControl<UniformGrid>("BoardGrid");
    return true;
  }

  private void WinPathShow(int[]? winPath) {
    if (winPath == null) {
      return;
    }
    else {
      int r, c;
      for (int i = 0; i < winPath.Length; i += 2) {
        r = winPath[i]; c = winPath[i + 1];
        Buttons[r,c].Background = Brushes.Pink;
      }
    }
  }

  public void ResetScores_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
    ScoreX = 0; ScoreO = 0; Draws = 0;
    ScoreText.Text = $"X: {ScoreX} | O: {ScoreO} | Remis: {Draws}";
  }
  public void Restart_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
    BoardGrid.IsHitTestVisible = true;
    StartNewGame();
  }
  public void ToggleMode_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
    IsPVE = !IsPVE;
    if (sender is Button btn) {
        btn.Content = IsPVE ? "PvE" : "PvP";
    }
    BoardGrid.IsHitTestVisible = true;
    StartNewGame();
  }
}

