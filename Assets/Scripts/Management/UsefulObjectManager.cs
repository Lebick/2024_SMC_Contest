using UnityEngine;

public class UsefulObjectManager : Singleton<UsefulObjectManager>
{
    public Transform cameraCanvasTr;
    public Transform overlayCanvasTr;
    public Transform worldCanvasTr;

    public GameObject damageText;

    public PlayerController player;
}
