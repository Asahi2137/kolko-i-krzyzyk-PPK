using System;

namespace zad2;
//using System.Random;
public abstract class Bot {
    protected Game GameRef;
    public Bot(Game game) => GameRef = game;
    public abstract int[] GetMove();
}
public class HeuristicBot : Bot {
    public HeuristicBot(Game game) : base(game) {}
    public override int[] GetMove() {
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                if (GameRef.IsEmpty(i, j)) {
                    int rowSum = GameRef.GetVal(i, 0) +
                                 GameRef.GetVal(i, 1) +
                                 GameRef.GetVal(i, 2);
                    int colSum = GameRef.GetVal(0, j) +
                                 GameRef.GetVal(1, j) +
                                 GameRef.GetVal(2, j);
                    int diag1 = (i == j) ? (GameRef.GetVal(0,0) + 
                                            GameRef.GetVal(1,1) + 
                                            GameRef.GetVal(2,2)) : 0;
                    int diag2 = (i == j)
                        ? GameRef.GetVal(2, 0) +
                          GameRef.GetVal(1, 1) +
                          GameRef.GetVal(0, 2) : 0;
                    if (Math.Abs(rowSum) == 2 ||
                        Math.Abs(colSum) == 2 ||
                        Math.Abs(diag1) == 2 ||
                        Math.Abs(diag2) == 2) {
                        return new[] { i, j };
                    }
                }
            }
        }
        int r, c;
        while (true) {
            r = Random.Shared.Next(0, 3);
            c = Random.Shared.Next(0, 3);
            if (GameRef.IsEmpty(r, c)) {
                break;
            }
        }
        return new[] { r, c };
    }
    
}