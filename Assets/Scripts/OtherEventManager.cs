using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OEventType
{
    FirstLight
}

public class OtherEventManager : MonoBehaviour
{
    public void FirstLight()
    {
        GameApp.ViewManager.Open(ViewType.StartView);
    }
}
