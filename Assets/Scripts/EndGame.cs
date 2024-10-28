using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private LaserLight LaserLight;

    public Camera Camera;

    private bool canMoveCamera = false;

    public Vector3 endPosition;

    public GameObject theLight;

    public GameObject theLevelEntry;

    private void Awake()
    {
        LaserLight = transform.Find("Light").GetComponent<LaserLight>();
    }


    private void Update()
    {
        if (LaserLight.isPowerOn && GameScene.gameData.isFirstPlay)
        {
            GameScene.gameData.isFirstPlay = false;
            canMoveCamera = true;
            theLight.gameObject.SetActive(true);
            theLevelEntry.gameObject.SetActive(false);
            StartCoroutine(Counter2ChangeScene());
        }

        MoveCamera2theLight();

        if (LaserLight.isPowerOn && (Camera.transform.position - endPosition).magnitude > 0.5f)
            canMoveCamera = true;
    }

    private void MoveCamera2theLight()
    {
        if(canMoveCamera)
        {
            Camera.transform.DOMove(endPosition,1.5f);
            canMoveCamera=false;
        }
    }


    IEnumerator Counter2ChangeScene()
    {
        yield return new WaitForSeconds(4f);

        LevelLoader.Instance.LoadNextLevel("EndScene");
        GameApp.ViewManager.Open(ViewType.EnddingView);
    }


}
