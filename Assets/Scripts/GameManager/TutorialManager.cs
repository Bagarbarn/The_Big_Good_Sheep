using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    KeyCode key_moveUp;
    KeyCode key_moveDown;
    KeyCode key_shoot;
    KeyCode key_colorOne;
    KeyCode key_colorTwo;
    KeyCode key_colorThree;
    KeyCode key_cancelColor;

    GameObject playerObject;
    PlayerController playerController;
    TutorialUIScript tutorialUI;

    Transform spawnPoint;
    Transform offScreenPoint;

    public int t_startEvent;

    public float transitionTime;
    private float transitionTimer;

    public float tutorial_moveSpeed;
    public float tutorial_inwardMovement;

    public float spawnHeightDifference;

    public GameObject background;
    public GameObject sheepObject;
    public GameObject foxObject;
    public GameObject roadblockObject;

    public GameObject overdriveObject;

    public float tutorial_waitTime;

    public float endTimer;

    private Vector2 topSpawn;
    private Vector2 bottomSpawn;
    public GameObject[] backgrounds;

    public void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerController = playerObject.GetComponent<PlayerController>();
        tutorialUI = this.GetComponent<TutorialUIScript>();
        this.key_moveUp = playerController.key_moveUp;
        this.key_moveDown = playerController.key_moveDown;
        this.key_colorOne = playerController.key_colorOne;
        this.key_colorTwo = playerController.key_colorTwo;
        this.key_colorThree = playerController.key_colorThree;
        this.key_shoot = playerController.key_shoot;
        this.key_cancelColor = playerController.key_cancelColor;

        spawnPoint = GameObject.FindGameObjectWithTag("ObjectSpawner").transform;
        offScreenPoint = GameObject.FindGameObjectWithTag("Offscreen").transform;
        backgrounds = GameObject.FindGameObjectsWithTag("Background");

        topSpawn = new Vector2(spawnPoint.position.x, spawnPoint.position.y + spawnHeightDifference);
        bottomSpawn = new Vector2(spawnPoint.position.x, spawnPoint.position.y - spawnHeightDifference);

        StartCoroutine("EventMinusOne");
    }


    void MoveBackgrounds()
    {
        for (int i = 0; i < backgrounds.Length; i++)
            backgrounds[i].transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("yeet");
        SceneManager.LoadScene("MainMenu");
    }

    void StartRoutine()
    {
        switch (t_startEvent)
        {
            case 0: StartCoroutine("EventZero"); break;
            case 1: StartCoroutine("EventOne"); break;
            case 2: StartCoroutine("EventTwo"); break;
            case 3: StartCoroutine("EventThree"); break;
            case 4: StartCoroutine("EventFour"); break;
            case 5: StartCoroutine("EventFive"); break;
            case 6: StartCoroutine("EventSix"); break;
            default: StartCoroutine("EventZero"); break;
        }
    }

    IEnumerator EventMinusOne()
    {
        tutorialUI.infoText.text = "Welcome to the tutorial, press Space to start";
        tutorialUI.ChangeActive(tutorialUI.infoText, true);
        bool pressed = false;

        while (!pressed)
        {
            if (Input.GetKeyDown(key_shoot))
                pressed = true;
            yield return null;
        }

        tutorialUI.ChangeActive(tutorialUI.infoText, false);
        StartRoutine();
    }

    IEnumerator EventZero()
    {
        t_startEvent++;
        transitionTimer = transitionTime;
        while (transitionTimer > 0)
        {
            transitionTimer -= Time.deltaTime;
            MoveBackgrounds();
            yield return null;
        }
        StartRoutine();
    }


    //Movement
    IEnumerator EventOne()
    {
        Debug.Log("EventOne has started");
        t_startEvent = 1;
        bool movedUp = false;
        bool movedDown = false;

        tutorialUI.ChangeActive(tutorialUI.moveSprites, true);
        tutorialUI.infoText.text = "Move up and down with the displayed keys";
        tutorialUI.ChangeActive(tutorialUI.infoText, true);
        
        while (!movedDown || !movedUp)
        {
            if (Input.GetKeyDown(key_moveDown))
                movedDown = true;
            if (Input.GetKeyDown(key_moveUp))
                movedUp = true;


            yield return null;
        }

        tutorialUI.ChangeActive(tutorialUI.infoText, false);
        tutorialUI.ChangeActive(tutorialUI.moveSprites, false);

        //tutorialUI.ChangeActive(tutorialUI.moveSprites, false);
        StartCoroutine("EventZero");
    }

    //Single colored sheep;
    IEnumerator EventTwo()
    {
        Debug.Log("EventTwo has started");

        GameObject currentSheep;

        //red sheep;
        currentSheep = Instantiate(sheepObject, spawnPoint.position, Quaternion.identity);

        currentSheep.GetComponent<Tutorial_SheepScript>().ObtainColor("red");

        tutorialUI.ChangeActive(tutorialUI.keyRed, true);

        tutorialUI.infoText.text = "Use the J, K and L button to select base colors";
        tutorialUI.ChangeActive(tutorialUI.infoText, true);

        tutorialUI.ChangeActive(tutorialUI.shootSprite, true);

        while (currentSheep != null)
        {
            //if (Input.GetKeyDown(key_colorOne))
            //    tutorialUI.ChangeActive(tutorialUI.keyRed, false);
            /*else*/ if (Input.GetKeyDown(key_colorTwo) || Input.GetKeyDown(key_colorThree))
                tutorialUI.infoText.text = "Press C to cancel your color selection";
            else if (Input.GetKeyDown(key_cancelColor))
                tutorialUI.infoText.text = "Use the J, K and L button to select base colors";
            else if (Input.GetKeyDown(key_shoot))
                tutorialUI.ChangeActive(tutorialUI.shootSprite, false);


            if (currentSheep.transform.position.x > tutorial_inwardMovement)
                currentSheep.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            yield return null;
        }

        tutorialUI.ChangeActive(tutorialUI.keyRed, false);

        //blue sheep
        currentSheep = Instantiate(sheepObject, spawnPoint.position, Quaternion.identity);

        currentSheep.GetComponent<Tutorial_SheepScript>().ObtainColor("blue");

        tutorialUI.ChangeActive(tutorialUI.keyBlue, true);
        tutorialUI.infoText.text = "Use the J, K and L button to select base colors";

        while (currentSheep != null)
        {
            //if (Input.GetKeyDown(key_colorTwo))
            //    tutorialUI.ChangeActive(tutorialUI.keyBlue, false);
            /*else*/ if (Input.GetKeyDown(key_colorOne) || Input.GetKeyDown(key_colorThree))
                tutorialUI.infoText.text = "Press C to cancel your color selection";
            else if (Input.GetKeyDown(key_cancelColor))
                tutorialUI.infoText.text = "Use the J, K and L button to select base colors";


            if (currentSheep.transform.position.x > tutorial_inwardMovement)
                currentSheep.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            yield return null;
        }

        tutorialUI.ChangeActive(tutorialUI.keyBlue, false);

        tutorialUI.ChangeActive(tutorialUI.keyBlue, false);
        //yellow sheep
        currentSheep = Instantiate(sheepObject, spawnPoint.position, Quaternion.identity);

        currentSheep.GetComponent<Tutorial_SheepScript>().ObtainColor("yellow");

        tutorialUI.infoText.text = "Use the J, K and L button to select base colors";
        tutorialUI.ChangeActive(tutorialUI.keyYellow, true);

        while (currentSheep != null)
        {
            //if (Input.GetKeyDown(key_colorThree))
            //    tutorialUI.ChangeActive(tutorialUI.keyYellow, false);
            /*else*/ if (Input.GetKeyDown(key_colorTwo) || Input.GetKeyDown(key_colorOne))
                tutorialUI.infoText.text = "Press C to cancel your color selection";
            else if (Input.GetKeyDown(key_cancelColor))
                tutorialUI.infoText.text = "Use the J, K and L button to select base colors";

            if (currentSheep.transform.position.x > tutorial_inwardMovement)
                currentSheep.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            yield return null;
        }

        tutorialUI.ChangeActive(tutorialUI.keyYellow, false);

        tutorialUI.ChangeActive(tutorialUI.infoText, false);

        StartCoroutine("EventZero");
    }


    //Multi colored sheep
    IEnumerator EventThree()
    {
        Debug.Log("EventThree has started");
        GameObject currentSheep;

        tutorialUI.ChangeActive(tutorialUI.infoText, true);
        tutorialUI.infoText.text = "You can mix two base colors to make a new color";

        //Purple sheep;
        currentSheep = Instantiate(sheepObject, spawnPoint.position, Quaternion.identity);

        currentSheep.GetComponent<Tutorial_SheepScript>().ObtainColor("purple");

        tutorialUI.ChangeActive(tutorialUI.keyRed, true);
        tutorialUI.ChangeActive(tutorialUI.keyBlue, true);


        while (currentSheep != null)
        {
            //if (Input.GetKeyDown(key_colorOne))
            //    tutorialUI.ChangeActive(tutorialUI.keyRed, false);
            //else if (Input.GetKeyDown(key_colorTwo))
            //    tutorialUI.ChangeActive(tutorialUI.keyBlue, false);
            /*else*/ if (Input.GetKeyDown(key_colorThree))
                tutorialUI.infoText.text = "Press C to cancel your color selection";
            else if (Input.GetKeyDown(key_cancelColor))
                tutorialUI.infoText.text = "You can mix two base colors to make a new color";


            if (currentSheep.transform.position.x > tutorial_inwardMovement)
                currentSheep.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            yield return null;
        }

        tutorialUI.ChangeActive(tutorialUI.keyRed, false);
        tutorialUI.ChangeActive(tutorialUI.keyBlue, false);

        //Orange sheep
        currentSheep = Instantiate(sheepObject, bottomSpawn, Quaternion.identity);

        tutorialUI.ChangeActive(tutorialUI.keyRed, true);
        tutorialUI.ChangeActive(tutorialUI.keyYellow, true);


        currentSheep.GetComponent<Tutorial_SheepScript>().ObtainColor("orange");
        while (currentSheep != null)
        {
            //if (Input.GetKeyDown(key_colorOne))
            //    tutorialUI.ChangeActive(tutorialUI.keyRed, false);
            //else if (Input.GetKeyDown(key_colorThree))
            //    tutorialUI.ChangeActive(tutorialUI.keyYellow, false);
            /*else*/ if (Input.GetKeyDown(key_colorTwo))
                tutorialUI.infoText.text = "Press C to cancel your color selection";
            else if (Input.GetKeyDown(key_cancelColor))
                tutorialUI.infoText.text = "You can mix two base colors to make a new color";


            if (currentSheep.transform.position.x > tutorial_inwardMovement)
                currentSheep.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            yield return null;
        }


        tutorialUI.ChangeActive(tutorialUI.keyRed, false);
        tutorialUI.ChangeActive(tutorialUI.keyYellow, false);

        //Green sheep
        currentSheep = Instantiate(sheepObject, topSpawn, Quaternion.identity);

        currentSheep.GetComponent<Tutorial_SheepScript>().ObtainColor("green");

        tutorialUI.ChangeActive(tutorialUI.keyBlue, true);
        tutorialUI.ChangeActive(tutorialUI.keyYellow, true);

        while (currentSheep != null)
        {
            //if (Input.GetKeyDown(key_colorTwo))
            //    tutorialUI.ChangeActive(tutorialUI.keyBlue, false);
            //else if (Input.GetKeyDown(key_colorThree))
            //    tutorialUI.ChangeActive(tutorialUI.keyYellow, false);
            /*else*/ if (Input.GetKeyDown(key_colorOne))
                tutorialUI.infoText.text = "Press C to cancel your color selection";
            else if (Input.GetKeyDown(key_cancelColor))
                tutorialUI.infoText.text = "You can mix two base colors to make a new color";


            if (currentSheep.transform.position.x > tutorial_inwardMovement)
                currentSheep.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            yield return null;
        }

        tutorialUI.ChangeActive(tutorialUI.keyBlue, false);
        tutorialUI.ChangeActive(tutorialUI.keyYellow, false);
        tutorialUI.ChangeActive(tutorialUI.infoText, false);

        StartCoroutine("EventZero");
    }

    //The roadblock
    IEnumerator EventFour()
    {
        GameObject roadblock;
        roadblock = Instantiate(roadblockObject, spawnPoint.position, Quaternion.identity);
        //Get roadblock on the screen
        while (roadblock.transform.position.x > tutorial_inwardMovement)
        {
            roadblock.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            MoveBackgrounds();
            yield return null;
        }
        float time = tutorial_waitTime;
        tutorialUI.ChangeActive(tutorialUI.infoText, true);
        tutorialUI.infoText.text = "A roadblock! Avoid it";
        Vector2 lastPosrb = roadblock.transform.position;
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        while (roadblock.transform.position.x > playerObject.transform.position.x)
        {
            if (roadblock.GetComponent<Tutorial_RoadblockScript>().playerHit)
            {
                roadblock.GetComponent<Tutorial_RoadblockScript>().playerHit = false;
                Debug.Log("Roadblock hit");
                tutorialUI.infoText.text = "You hit the roadblock, please try again";
                roadblock.transform.position = lastPosrb;
                float retime = tutorial_waitTime;
                while (retime > 0)
                {
                    retime -= Time.deltaTime;
                    yield return null;
                }
            }
            roadblock.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            MoveBackgrounds();
            yield return null;
        }
        tutorialUI.infoText.text = "Well done!";
        while (roadblock.transform.position.x > offScreenPoint.position.x)
        {
            roadblock.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            MoveBackgrounds();
            yield return null;
        }
        Destroy(roadblock);
        tutorialUI.ChangeActive(tutorialUI.infoText, false);

        StartCoroutine("EventZero");
    }

    //Overdrive Pickup
    IEnumerator EventFive()
    {
        GameObject pickup;
        pickup = Instantiate(overdriveObject, topSpawn, Quaternion.identity);
        while (pickup.transform.position.x > tutorial_inwardMovement)
        {
            pickup.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            MoveBackgrounds();
            yield return null;
        }

        tutorialUI.ChangeActive(tutorialUI.infoText, true);
        tutorialUI.infoText.text = "A pickup! try running it over";

        float time = tutorial_waitTime;

        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        bool pickedUp = false;

        Vector2 lastPospu = pickup.transform.position;


        while (!pickedUp)
        {
            if (pickup == null)
            {
                pickedUp = true;
                tutorialUI.infoText.text = "Nice! Now serve the customers";
            }
            else
            {
                pickup.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
                MoveBackgrounds();
                if (pickup.transform.position.x <= offScreenPoint.position.x)
                {
                    pickup.transform.position = lastPospu;
                    tutorialUI.infoText.text = "You missed the pickup! Please run it over";
                    float retime = tutorial_waitTime;
                    while (retime > 0)
                    {
                        retime -= Time.deltaTime;
                        yield return null;
                    }
                }

            }
            yield return null;
        }

        GameObject[] sheepArray = new GameObject[3];
        sheepArray[0] = Instantiate(sheepObject, spawnPoint.position, Quaternion.identity);
        sheepArray[0].GetComponent<Tutorial_SheepScript>().ObtainColor("orange");
        sheepArray[1] = Instantiate(sheepObject, topSpawn, Quaternion.identity);
        sheepArray[1].GetComponent<Tutorial_SheepScript>().ObtainColor("red");
        sheepArray[2] = Instantiate(sheepObject, bottomSpawn, Quaternion.identity);
        sheepArray[2].GetComponent<Tutorial_SheepScript>().ObtainColor("green");

        while (sheepArray[0] != null || sheepArray[1] != null || sheepArray[2] != null)
        {
            for ( int i = 0; i < sheepArray.Length; i++)
            {
                if (sheepArray[i] != null)
                {
                    sheepArray[i].transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
                    if (sheepArray[i].transform.position.x <= offScreenPoint.position.x)
                        Destroy(sheepArray[i]);
                }
            }
            MoveBackgrounds();
            yield return null;
        }

        tutorialUI.ChangeActive(tutorialUI.infoText, false);
        StartCoroutine("EventZero");
    }

    //End of tutorial
    IEnumerator EventSix()
    {
        tutorialUI.ChangeActive(tutorialUI.timeText, true);
        while (endTimer > 0)
        {
            tutorialUI.timeText.text = endTimer.ToString("F1");
            MoveBackgrounds();
            endTimer -= Time.deltaTime;
            yield return null;
        }
        endTimer = 0;
        tutorialUI.timeText.text = endTimer.ToString("F1");
        tutorialUI.ChangeActive(tutorialUI.infoText, true);
        tutorialUI.infoText.text = "Well done! You completed the tutorial!";
        tutorialUI.ChangeActive(tutorialUI.mainMenuButton, true);
    }
}
