using UnityEngine;
using Photon;

public class CheckpointRPC : PunBehaviour {

    private SmallGarbage smallGarbageScript;

    [PunRPC]
    public void setSmallGarbageScript()
    {
        smallGarbageScript = GameObject.Find("Minimap").GetComponent<SmallGarbage>();
    }

    [PunRPC]
    private void disableGarbage()
    {
        smallGarbageScript.disableGarbageImage(gameObject.name);
        gameObject.SetActive(false);
    }

    [PunRPC]
    private void enableGarbage()
    {
        gameObject.SetActive(true);
        smallGarbageScript.enableGarbageImage(gameObject.name);
    }

}
