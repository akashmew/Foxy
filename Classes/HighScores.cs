using Godot;
using Godot.NativeInterop;
using System;
using System.Linq;

public partial class HighScores : Resource
{
    const int MAX_SCORES = 10;

    [Export] public Godot.Collections.Array<HighScore> Scores { get; set; } = new();


    private void SortScores()
    {
        var list = Scores.ToList();
        list.Sort((a, b) => b.score.CompareTo(a));
        Scores = new(list);
    }
    public void AddScores(int score)
    {
        HighScore highScore = new HighScore();
        highScore.score = score;
        Scores.Add(highScore);

        SortScores();

        if(Scores.Count> MAX_SCORES)
          Scores.Resize(MAX_SCORES);
    }
}
