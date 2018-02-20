using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankingObservableObject {

    private List<RankingUser> usersInRanking = new List<RankingUser>();
    private List<IRankingObserver> subscribers = new List<IRankingObserver>();

    public void addUser(RankingUser newUser)
    {
        usersInRanking.Add(newUser);
    }

    public List<RankingUser> getListOfUsers()
    {
        return usersInRanking;
    }

    public void filled()
    {
        foreach (IRankingObserver observer in subscribers)
        {
            observer.dataFilled();
        }
    }

    public override string ToString()
    {
        string cadena = "";
        foreach(RankingUser user in usersInRanking)
        {
            cadena += "Name: " + user.getUser() + " Score: " + user.getScore() + "\n";
        }
        return cadena;
    }

    public int length()
    {
        return usersInRanking.Count;
    }

    public void subscribe(IRankingObserver newSubscriber)
    {
        subscribers.Add(newSubscriber);

    }
	
}
