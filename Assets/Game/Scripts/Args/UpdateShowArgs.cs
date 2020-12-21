using System.Collections;

public class UpdateShowArgs
{
    //lv target score
    public int Level { get; set; }
    public int Target { get; set; }
    public int Score { get; set; }
    public float Time { get; set; }

    public UpdateShowArgs(int lv, int target, int score)
    {
        this.Level = lv;
        this.Target = target;
        this.Score = score;
    }
}
