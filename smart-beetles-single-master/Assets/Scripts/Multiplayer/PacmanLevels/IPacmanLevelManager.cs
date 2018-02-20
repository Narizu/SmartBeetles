using UnityEngine;

public interface IPacmanLevelManager {

    bool addProgress();
    int objectiveProgress();
    void resetProgress();

}
