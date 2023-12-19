using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPiece : MonoBehaviour
{
    public CanvasManager canvasManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasManager.OnWin();
            Destroy(gameObject);
        }
    }
}

