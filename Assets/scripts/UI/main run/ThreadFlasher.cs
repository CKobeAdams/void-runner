using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThreadFlasher : MonoBehaviour
{

    private float riseRate = 10f, durationCounter = 0f,riseDuration =1f;

    [SerializeField]
    private TMP_Text threadText;


    // Update is called once per frame
    void Update()
    {
        durationCounter += Time.deltaTime;
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y + riseRate * Time.deltaTime, pos.z);
        if(durationCounter>=riseDuration)
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeText(int value)
    {
        threadText.text = value + "T";
    }


}
