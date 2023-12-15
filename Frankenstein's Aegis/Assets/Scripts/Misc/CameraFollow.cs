using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float minXClamp;
    public float maxXClamp;
    public float minYClamp;
    public float maxYClamp;

    private void LateUpdate()
    {
        if (GameManager.Instance.playerInstance == null) return;

        Vector3 cameraPos;

        cameraPos = transform.position;
        cameraPos.x = Mathf.Clamp(GameManager.Instance.playerInstance.transform.position.x, minXClamp, maxXClamp);
        cameraPos.y = Mathf.Clamp(GameManager.Instance.playerInstance.transform.position.y, minYClamp, maxYClamp);

        transform.position = cameraPos;
    }
}
