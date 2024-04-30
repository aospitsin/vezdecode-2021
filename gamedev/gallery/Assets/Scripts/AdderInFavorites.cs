using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdderInFavorites : MonoBehaviour
{
    private static GameObject pictureFavorite;
    private float timeHoldTouch;
    private static float timeBegan;
    private static bool oneAddFavorite = false;

    private static HashSet<Material> imagePictures = new HashSet<Material>();

    public static bool IsFavorite = false;
    //public static Material favoritePictureImage;
    private void Start()
    {
        pictureFavorite = transform.GetChild(1).gameObject;
        RefreshPictureFavorite();
    }

    void Update()
    {
        TouchHoldTime();
    }
    
    void TouchHoldTime()
    {
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Stationary)
            {
                timeBegan += Time.deltaTime;
                if (timeBegan > 0.5f && !IsFavorite && !oneAddFavorite)
                {
                    Debug.Log(GetComponent<Image>().material);
                    imagePictures.Add(GetComponent<Image>().material);

                    pictureFavorite.SetActive(true);
                    oneAddFavorite = true;
                    IsFavorite = true;
                }
            }
        }
    }

    public static HashSet<Material> GetFavoritePictures()
    {
        return imagePictures;
    }
    
    public static void RefreshPictureFavorite()
    {
        oneAddFavorite = false;
        timeBegan = 0f;
        pictureFavorite.SetActive(false);
    }
}
