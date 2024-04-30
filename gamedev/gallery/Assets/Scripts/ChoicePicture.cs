using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class ChoicePicture : MonoBehaviour
{
    public GameObject canvas;
    public GameObject showPicture;

    private Camera mainCam;
    private float timeHoldTouch;
    private float timeBegan, timeEnded;
    private Vector3 posTouch;
    private Image image;
    private bool activePicture = false;
    void Start()
    {
        mainCam = Camera.main;
        image = showPicture.transform.GetChild(0).GetComponent<Image>();
        showPicture.SetActive(false);
    }
    
    void Update()
    {
        TouchHoldTime();

        if(timeHoldTouch < 0.15f)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCam.ScreenPointToRay(posTouch), out hit, 20f))
            {
                if (hit.transform.gameObject.tag == "Picture" && !activePicture && !UIShowPictureCanvas.activeMenuFavorites)
                {
                    UIShowPictureCanvas.DisableRotateCamera();
                    GameObject picture = hit.transform.gameObject;
                    canvas.SetActive(false);
                    image.material = picture.GetComponent<MeshRenderer>().materials[0];
                    showPicture.SetActive(true);
                    activePicture = true;
                }
            }

            timeHoldTouch = 1f;
        }
    }

    public void ClosePicture()
    {
        StartCoroutine(closeTimerPicture());
    }
    
    void TouchHoldTime()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                timeBegan = Time.time;
                posTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                timeEnded = Time.time;
                timeHoldTouch = timeEnded - timeBegan;
            }
        }
    }

    IEnumerator closeTimerPicture()
    {
        yield return new WaitForSeconds(0.5f);
        activePicture = false;
    }
}
