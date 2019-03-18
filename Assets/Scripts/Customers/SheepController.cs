using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MovingObjects {
    
    private string m_demandedColor;

    SpriteRenderer demandSprite;

    private ParticleManager particleScript;
    public GameObject iceCreamParticles;

    private SoundScript soundManager;
    public AudioClip satisfiedBleat;
    public AudioClip satisfiedBleat2;

    public float p_timeValue;
    public int p_scoreValue;
    public int p_scoreFailure;
    public int p_timeWhenHit;



    public Vector2 speedValues;
    public Vector2 modifier_SpeedModifier_secondsToMax;

    private Animator m_animator;
    private BoxCollider2D m_collider;
    private float timeSpeedModifier;
    private float time;

    private ScreenShake ss;

    override public void Start()
    {
        base.Start();
        if (Time.timeSinceLevelLoad < modifier_SpeedModifier_secondsToMax.y)
            time = Time.timeSinceLevelLoad;
        else
            time = modifier_SpeedModifier_secondsToMax.y;

        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();
        m_animator = gameObject.GetComponent<Animator>();
        m_collider = gameObject.GetComponent<BoxCollider2D>();

        //Gets modifier for speed increase
        timeSpeedModifier = modifier_SpeedModifier_secondsToMax.x * (time / modifier_SpeedModifier_secondsToMax.y);
        p_speed = Random.Range(speedValues.x, speedValues.y) * timeSpeedModifier;

        //Gets demanded color
        bool multicolor = gameManagerScript.gameObject.GetComponent<SpawnManager>().GetColorMode();
        m_demandedColor = gameManagerScript.gameObject.GetComponent<ColorManager>().GetRandomColor(multicolor);
        demandSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        demandSprite.color = gameManagerScript.gameObject.GetComponent<ColorManager>().GetColor(m_demandedColor);

        //Get Camera script

        ss = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShake>();

        particleScript = gameManagerScript.gameObject.GetComponent<ParticleManager>();

        //Destroys sheep outside screen
        Destroy(this.gameObject, 25 / (gameManagerScript.m_currentSpeed + p_speed));
    }

    AudioClip RandomizeBleat()
    {
        int rand = Random.Range(1, 5);
        if (rand >= 3)
            return satisfiedBleat;
        else return satisfiedBleat2;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            string bullet_color = other.gameObject.GetComponent<BulletScript>().p_color;
            Color color_color = Color.black;
            if (bullet_color != "Rainbow")
            {
                color_color = gameManagerScript.gameObject.GetComponent<ColorManager>().GetColor(bullet_color);
            }
            particleScript.SpawnParticleSystem(iceCreamParticles, transform.position, color_color);

            if (bullet_color == m_demandedColor || bullet_color == "Rainbow")
            {
                gameManagerScript.gameObject.GetComponent<TimeManager>().AdjustTime(p_timeValue);
                gameManagerScript.AddScore(p_scoreValue);
                soundManager.PlayAudio(RandomizeBleat());

                FloatTextController.CreateFloatingText((gameManagerScript.m_multiplier*p_scoreValue).ToString() + "p", transform, true);
                FloatTextController.CreateFloatingText(p_timeValue.ToString() + "s", transform, true);

                m_animator.SetBool("isSatisfied", true);
                m_collider.enabled = false;
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
                Destroy(other.gameObject);
                Destroy(gameObject, 0.7f);
            }
            else
            {
                Destroy(other.gameObject);
                gameManagerScript.AddScore(-p_scoreFailure);
                FloatTextController.CreateFloatingText("-"+p_scoreFailure.ToString()+"p", transform, false);
                //Something when wrong color hits sheeps
            }
        } else if (other.gameObject.tag == "Player")
        {
            gameManagerScript.gameObject.GetComponent<TimeManager>().AdjustTime(-p_timeWhenHit);
            FloatTextController.CreateFloatingText("-"+p_timeWhenHit.ToString()+"s", transform, false);
            ss.ShakeScreen();
            //Debug.Log("Sheep Hit");
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
        Vector2 dir = new Vector2(0,0);
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


