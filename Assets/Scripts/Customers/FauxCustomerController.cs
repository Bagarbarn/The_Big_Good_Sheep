using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxCustomerController : MovingObjects {

    public float p_decreaseTime;
    public int p_increasePoints;

    private string m_demandedColor;

    private ParticleManager particleScript;
    public GameObject iceCreamParticles;

    SpriteRenderer demandSprite;
    SoundScript soundManager;
    public AudioClip evilLaughter;

    ScreenShake ss;

    // Use this for initialization
    override public void Start()
    {
        base.Start();
        bool multicolor = gameManagerScript.gameObject.GetComponent<SpawnManager>().GetColorMode();
        m_demandedColor = gameManagerScript.gameObject.GetComponent<ColorManager>().GetRandomColor(multicolor);
        demandSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        demandSprite.color = gameManagerScript.gameObject.GetComponent<ColorManager>().GetColor(m_demandedColor);
        Destroy(this.gameObject, 25 / (gameManagerScript.m_currentSpeed + p_speed));
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();
        ss = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShake>();
        particleScript = gameManagerScript.gameObject.GetComponent<ParticleManager>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {

            //Spawn Particle
            string bullet_color = other.gameObject.GetComponent<BulletScript>().p_color;
            Color color_color = gameManagerScript.gameObject.GetComponent<ColorManager>().GetColor(bullet_color);
            particleScript.SpawnParticleSystem(iceCreamParticles, transform.position, color_color);


            if (other.gameObject.GetComponent<BulletScript>().p_color != "Rainbow")
            {
                gameManagerScript.gameObject.GetComponent<TimeManager>().AdjustTime(-p_decreaseTime);
                FloatTextController.CreateFloatingText("-" + p_decreaseTime.ToString() + "s", transform, false);

                //Cool smoke bomb animation
                soundManager.PlayAudio(evilLaughter);
            }
            Destroy(other.gameObject);
            Destroy(this.gameObject);

        } else if (other.tag == "Player")
        {
            //They can die
            gameManagerScript.AddScore(p_increasePoints);

            int multiplier = 1;
            if (gameManagerScript.m_multiplier >= 2)
                multiplier = (int)gameManagerScript.m_multiplier;

            FloatTextController.CreateFloatingText((multiplier * p_increasePoints).ToString()+"p", transform, true);
            ss.ShakeScreen();
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Customer")
        {
            if (other.gameObject.transform.position.x < transform.position.x) // This sheep is taking over
            {
                p_speed = 0.5f;
            }
            else
            {
                if (p_speed < 1f)
                    p_speed = 1f;
            }
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            if (other.transform.position.y > 2) // Top lane
            {
                StartCoroutine(MoveLane(false));
            }
            else if (other.transform.position.y > 0) // Middle lane
            {
                int i = Random.Range(0, 2);
                if (i == 0) StartCoroutine(MoveLane(true));
                else StartCoroutine(MoveLane(false));
            }
            else // Bottom lane
            {
                StartCoroutine(MoveLane(true));
            }
        }
    }

    IEnumerator MoveLane(bool up)
    {
        float speed = 6;
        Vector2 dir = new Vector2(0, 0);
        if (up) dir.y = 1;
        if (!up) dir.y = -1;

        float time = 2 / speed;
        float timer = time;

        while (timer > 0)
        {
            transform.Translate(dir * speed * Time.deltaTime);
            spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100f);
            timer -= Time.deltaTime;
            yield return null;
        }
    }
}
