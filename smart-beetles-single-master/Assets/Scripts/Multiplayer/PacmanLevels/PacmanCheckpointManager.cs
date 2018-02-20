using UnityEngine;
using System.Collections;

public class PacmanCheckpointManager : MonoBehaviour, IPacmanLevelManager {

    private int targetProgress = 4;
    private int currentProgress;

	public bool addProgress()
    {
        currentProgress++;
        return currentProgress == targetProgress;
    }

    public int objectiveProgress()
    {
        return targetProgress;
    }

    public void resetProgress()
    {
        currentProgress = 0;
    }

}
