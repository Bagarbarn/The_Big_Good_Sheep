using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    public float max_delta;
    public float duration_total;
    public float duration_delay;
    public float speed;


	public void ShakeScreen()
    {
        Debug.Log("ShakeScreen");
        StartCoroutine("ShakingScreen");
    }

    Vector3 GetRandomDir()
    {
        float x = Random.Range(max_delta / 10, max_delta);
        float y = Random.Range(max_delta / 10, max_delta);
        if (x > 0) x = -x;
        if (y > 0) y = -y;

        return new Vector3(x, y, transform.position.z);

    }

    IEnumerator ShakingScreen()
    {
        Vector3 originalPos = transform.position;
        Vector3 newPos = new Vector3(0, 0, transform.position.z);
        float timer = duration_total;
        float timer_delay = 0;
        while (timer > 0)
        {
            if (timer_delay <= 0)
            {
                float x = Random.Range(-1f, 1f) * max_delta;
                float y = Random.Range(-1f, 1f) * max_delta;
                newPos = new Vector3(x, y, transform.position.z);
                timer_delay = duration_delay;
            }
            else timer_delay -= Time.deltaTime;


            transform.position = newPos;
            timer -= Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
    }
}
