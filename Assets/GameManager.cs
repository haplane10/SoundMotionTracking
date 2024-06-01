using Mediapipe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnPos;
    public RectTransform faceMarker;
    public RectTransform screenCanvas;

    LocationData locationData;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            Instantiate(projectile, spawnPos);
            yield return new WaitForSeconds(5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (locationData == null) return;

        var _rbb = locationData.RelativeBoundingBox;


        var xPos = -screenCanvas.sizeDelta[0] * (_rbb.Xmin + (_rbb.Width * 0.5f));
        var yPos = -screenCanvas.sizeDelta[1] * (_rbb.Ymin + (_rbb.Height * 0.5f));

        faceMarker.anchoredPosition = new Vector2(xPos, yPos);
    }

    public void GetLocationData(LocationData _locationData)
    {
        locationData = _locationData;
    }
}
