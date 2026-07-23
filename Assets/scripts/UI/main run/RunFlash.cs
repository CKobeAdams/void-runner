using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunFlash : MonoBehaviour
{
    [SerializeField]
    private TMP_Text runText;

    private float riseDuration = 1f, flashDuration = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flash());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Flash()
    {
        float elapsedTime = 0;
        int flashCounter = 1;
        bool isFlashing = true;


        while(elapsedTime<flashDuration)
        {
            elapsedTime += Time.deltaTime;
           
            if(elapsedTime*10>=flashCounter)
            {
                isFlashing = !isFlashing;
                flashCounter += 1;
            }

            if(isFlashing)
            {
                runText.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                runText.color = new Color(1f, 1f, 1f, 0f);
            }

                yield return null;
        }

        runText.color = new Color(1f, 1f, 1f, 1f);

        StartCoroutine(Rise());
    }

    IEnumerator Rise()
    {
        float elapsedTime = 0;
        float riseSpeed = 10f;

        while(elapsedTime<riseDuration)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, pos.y + riseSpeed * Time.deltaTime, pos.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
