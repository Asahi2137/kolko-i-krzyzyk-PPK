namespace zad2;
public class Matrix {
    private uint Rows, Cols;
    private int[,] Tab;
    
    public Matrix() {
        Rows = 3;
        Cols = 3;
        Tab = new int[3, 3] {
            {0, 0, 0},
            {0, 0, 0},
            {0, 0, 0}
        };
    }

    public bool IsZero(int r, int c) {
        if (r < 3 && r >= 0 && c < 3 && c >= 0) {
            return Tab[r, c] == 0;
        }
        return false;
    }
    
    public void SetValIntoMatrix(int r, int c, int val) {
        if (r < 3 && r >= 0 && c < 3 && c >= 0) {
            Tab[r, c] = val;
        }
    }
    public int GetValMatrixIdx(int r, int c) => Tab[r, c];
}