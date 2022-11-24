public class ScoreManager
{
    private int score;

    public void Reset()
    {
        score = 0;
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public int GetScore()
    {
        return score;
    }
}
