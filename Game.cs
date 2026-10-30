using System;

namespace zad2;
public class Game {
    private Matrix GameBoard;
    private bool PlayerOne;
    private int[] RowSum = new int[3], ColsSum = new int[3];
    private int Diag1 = 0, Diag2 = 0;// MoveCount = 0;
    public Game(bool TurnPlayer = false) {
        GameBoard = new Matrix();
        PlayerOne = TurnPlayer;
    }

    public (int, int[]?) SetMove(int r, int c) {
        if (GameBoard.IsZero(r, c)) {
            int val = PlayerOne ? 1 : -1;
            GameBoard.SetValIntoMatrix(r, c, val);
            RowSum[r] += val;
            ColsSum[c] += val;
            if (r == c) { Diag1 += val; }

            if (r + c == 2) { Diag2 += val; }

            if (Math.Abs(RowSum[r]) == 3) {
                return (PlayerOne ? 1 : -1, new int[6] {r,0 ,r,1, r,2}); //wygrana po kodzie gracza
            }
            else if (Math.Abs(ColsSum[c]) == 3) {
                return (PlayerOne ? 1 : -1, new int[6] { 0, c, 1, c, 2, c });
            }
            else if (Math.Abs(Diag1) == 3) {
                return (PlayerOne ? 1 : -1, new int[6] { 0, 0, 1, 1, 2, 2 });
            }
            else if (Math.Abs(Diag2) == 3) {
                return (PlayerOne ? 1 : -1, new int[6] { 2, 0, 1, 1, 0, 2 });
            }
            PlayerOne = !PlayerOne;
            //MoveCount++;
        }

        /*if (MoveCount == 9) {
            return (-2, null);
        }*/

        return (0, null); //kontynuacja
    }

    public bool IsEmpty(int r, int c) => 
        GameBoard.IsZero(r, c);
    public bool GetPlayerOne() {
        return PlayerOne;
    }

    public int GetVal(int r, int c) {
        return GameBoard.GetValMatrixIdx(r, c);
    }
}