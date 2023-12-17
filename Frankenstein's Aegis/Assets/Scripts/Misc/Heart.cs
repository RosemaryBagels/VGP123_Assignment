using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Sprite heartFull, heartHalf, heartEmpty;

    Image heartImage;

    private void Awake()
    {
        heartImage = GetComponent<Image>();

        if (!heartImage) Debug.Log("Heart is missing image component");
    }

    public void SetHeartImage(HeartStatus status)
    {
        switch(status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = heartEmpty;
                break;
            case HeartStatus.Half:
                heartImage.sprite = heartHalf;
                break;
            case HeartStatus.Full:
                heartImage.sprite = heartFull;
                break;
        }
    }

}
