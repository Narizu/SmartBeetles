using UnityEngine;
using System.Collections;
using System;

public class RankingUser : IComparable
{

    private string user;
    private string score;

    public void setUser(string u)
    {
        user = u;
    }

    public void setScore(string s)
    {
        score = s;
    }

    public string getUser()
    {
        return user;
    }

    public string getScore()
    {
        return score;
    }

    public int CompareTo(object obj)
    {
        RankingUser otherUser = (RankingUser) obj;
        var myScore = Convert.ToInt32(this.score);
        var otherScore = Convert.ToInt32(otherUser.score);
        return otherScore.CompareTo(myScore);
    }
}
