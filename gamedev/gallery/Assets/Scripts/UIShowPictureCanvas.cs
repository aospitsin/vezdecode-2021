using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShowPictureCanvas : MonoBehaviour
{
    private ChoicePicture choicePicture;
    private float timeHoldTouch;
    private float timeBegan, timeEnded;

    [SerializeField]
    private GameObject menuWithFavorites;

    public static bool activeMenuFavorites;

    void Start()
    {
        choicePicture = GetComponent<ChoicePicture>();
        menuWithFavorites.SetActive(false);
        activeMenuFavorites = false;
    }

    public void Close()
    {
        if (!AdderInFavorites.IsFavorite)
        {
            choicePicture.showPicture.SetActive(false);
            choicePicture.canvas.SetActive(true);
            choicePicture.ClosePicture();
            AdderInFavorites.RefreshPictureFavorite();
            EnableRotateCamera();
        }
        else
            AdderInFavorites.IsFavorite = false;
    }

    public static void DisableRotateCamera()
    {
        CameraRotate.cameraRotate.enabled = false;
    }

    public static void EnableRotateCamera()
    {
        CameraRotate.cameraRotate.enabled = true;
    }
    
    public void ShowMenuWithFavorites()
    {
        menuWithFavorites.SetActive(true);
        activeMenuFavorites = true;
        DisableRotateCamera();
    }

    public void CloseMenuWithFavorites()
    {
        menuWithFavorites.SetActive(false);
        activeMenuFavorites = false;
        EnableRotateCamera();
    }
}
