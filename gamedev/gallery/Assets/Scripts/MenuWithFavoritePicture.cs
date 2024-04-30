using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuWithFavoritePicture : MonoBehaviour
{
    public GameObject image;
    
    private GameObject content;
    private HashSet<Material> pictures;
    private List<GameObject> obj = new List<GameObject>();
    private int i;
    void Start()
    {
        content = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        i = 0;
        pictures = AdderInFavorites.GetFavoritePictures();
        Debug.Log(pictures.Count);
        foreach (var pctr in pictures)
        {
            obj.Add(Instantiate(image, content.transform));
            pctr.shader = Shader.Find("UI/Default");
            obj[i].GetComponent<Image>().material = pctr;
            i++;
        }
    }

    private void OnDisable()
    {
        i = 0;
        foreach (var pctr in pictures)
        {
            Destroy(obj[i].gameObject);
            i++;
        }
        obj.Clear();
    }
}
