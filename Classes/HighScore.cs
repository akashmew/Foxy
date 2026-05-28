using Godot;
using System;

public partial class HighScore : Resource
{
    [Export] public int score { get; set; } = 0;
    [Export] public string DateScore { get; set; } = GetFormattedDate();

    static string GetFormattedDate()
    {
        var d = Time.GetDateDictFromSystem();
        int day = (int)d["day"];
        int month = (int)d["month"];
        int year = (int)d["year"];

        return $"{day:D2}-{month:D2}-{year:D2}";
    }
}
