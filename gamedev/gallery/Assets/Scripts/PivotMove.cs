using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotMove : MonoBehaviour
{
    [HideInInspector]
    public float speed = 1.5f;

    [Header("Максимальные позиции смещения по Х, Z")]
    public float PosXmin; 
    public float PosXmax;
    public float PosZmin; 
    public float PosZmax;

    [SerializeField][Header("Чувствительность к касанию пальца")]
    private float sens = 5f;

    private float horizontal, vertical;
    private float X, Z;
    private bool tmp;

    void Start()
    {
        GameObject player = GameObject.Find("Student");
        transform.position = new Vector3(player.transform.position.x - 10f, transform.position.y, player.transform.position.z + 6f);
        tmp = false;
    }

#if UNITY_EDITOR_WIN
    //Для осмотра карты на компьютере
    private void Update()
    {
        if (Input.GetMouseButton(0) && Input.touchCount == 0)
        {
            horizontal += Input.GetAxis("Mouse X") * 7f;
            vertical += Input.GetAxis("Mouse Y") * 7f;

            Vector3 direction = new Vector3(horizontal, 0f, vertical);
            transform.Translate(direction * Time.deltaTime * speed);
            //Ограничение координат
            X = transform.position.x;
            Z = transform.position.z;
            X = Mathf.Clamp(X, PosXmin, PosXmax);
            Z = Mathf.Clamp(Z, PosZmin, PosZmax);
            transform.position = new Vector3(X, transform.position.y, Z);
        }
    }
#endif

    void FixedUpdate()
    {
        if (Input.touchCount == 2)
        {
            tmp = false;
        }

        if (Input.touchCount == 1)
        {
            StartCoroutine(Timer());

            if (Input.touches[0].deltaPosition.magnitude > sens && tmp)
            {
                horizontal = Mathf.Lerp(horizontal, -Input.touches[0].deltaPosition.x, 0.1f);
                vertical = Mathf.Lerp(vertical, -Input.touches[0].deltaPosition.y, 0.1f);
            }
            else
            {
                horizontal = Mathf.Lerp(horizontal, 0f, 0.1f);
                vertical = Mathf.Lerp(vertical, 0f, 0.1f);
            }
        }
        else
        {
            horizontal = Mathf.Lerp(horizontal, 0f, 0.1f);
            vertical = Mathf.Lerp(vertical, 0f, 0.1f);
        }


        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        transform.Translate(direction * Time.deltaTime * speed);
        //Ограничение координат
        X = transform.position.x;
        Z = transform.position.z;
        X = Mathf.Clamp(X, PosXmin, PosXmax);
        Z = Mathf.Clamp(Z, PosZmin, PosZmax);
        transform.position = new Vector3(X, transform.position.y, Z);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.1f);
        tmp = true;
    }
}
