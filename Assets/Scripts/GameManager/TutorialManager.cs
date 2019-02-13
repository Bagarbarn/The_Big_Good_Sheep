using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float tutorial_moveSpeed;
    public float tutorial_inwardMovement;

    public float spawnHeightDifference;

    public GameObject background;
    public GameObject sheepObject;
    public GameObject roadblockObject;

    private Vector2 topSpawn;
    private Vector2 bottomSpawn;

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

        topSpawn = new Vector2(spawnPoint.position.x, spawnPoint.position.y + spawnHeightDifference);
        bottomSpawn = new Vector2(spawnPoint.position.x, spawnPoint.position.y - spawnHeightDifference);

        StartCoroutine("EventOne");
    }


    //Movement
    IEnumerator EventOne()
    {
        Debug.Log("EventOne has started");

        bool movedUp = false;
        bool movedDown = false;




        //tutorialUI.ChangeActive(tutorialUI.keyBlue, true);
        //tutorialUI.BlinkObject(tutorialUI.keyBlue, 6f);

        tutorialUI.BlinkObject(tutorialUI.moveSprites, 2f, 2f);

        while (!movedDown || !movedUp)
        {
            if (Input.GetKeyDown(key_moveDown))
                movedDown = true;
            if (Input.GetKeyDown(key_moveUp))
                movedUp = true;


            yield return null;
        }

        //tutorialUI.ChangeActive(tutorialUI.moveSprites, false);
        StartCoroutine("EventTwo");
    }

    //Single colored sheep;
    IEnumerator EventTwo()
    {
        Debug.Log("EventTwo has started");

        GameObject currentSheep;

        //red sheep;
        currentSheep = Instantiate(sheepObject, spawnPoint.position, Quaternion.identity);

        currentSheep.GetComponent<Tutorial_SheepScript>().ObtainColor("red");

        tutorialUI.BlinkObject(tutorialUI.keyRed, 1f, 2f);

        while (currentSheep != null)
        {
            if (currentSheep.transform.position.x > tutorial_inwardMovement)
                currentSheep.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            yield return null;
        }

        //blue sheep
        currentSheep = Instantiate(sheepObject, spawnPoint.position, Quaternion.identity);

        currentSheep.GetComponent<Tutorial_SheepScript>().ObtainColor("blue");
        tutorialUI.BlinkObject(tutorialUI.keyBlue, 1f, 2f);

        while (currentSheep != null)
        {
            if (currentSheep.transform.position.x > tutorial_inwardMovement)
                currentSheep.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            yield return null;
        }

        //yellow sheep
        currentSheep = Instantiate(sheepObject, spawnPoint.position, Quaternion.identity);

        currentSheep.GetComponent<Tutorial_SheepScript>().ObtainColor("yellow");
        tutorialUI.BlinkObject(tutorialUI.keyYellow, 1f, 2f);

        while (currentSheep != null)
        {
            if (currentSheep.transform.position.x > tutorial_inwardMovement)
                currentSheep.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            yield return null;
        }

        StartCoroutine("EventThree");
    }


    //Multi colored sheep
    IEnumerator EventThree()
    {
        Debug.Log("EventThree has started");
        GameObject currentSheep;

        //Purple sheep;
        currentSheep = Instantiate(sheepObject, spawnPoint.position, Quaternion.identity);

        currentSheep.GetComponent<Tutorial_SheepScript>().ObtainColor("purple");
        

        while (currentSheep != null)
        {
            if (currentSheep.transform.position.x > tutorial_inwardMovement)
                currentSheep.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            yield return null;
        }

        //Orange sheep
        currentSheep = Instantiate(sheepObject, bottomSpawn, Quaternion.identity);

        currentSheep.GetComponent<Tutorial_SheepScript>().ObtainColor("orange");
        Debug.Log("EventTwo has started");
        while (currentSheep != null)
        {
            if (currentSheep.transform.position.x > tutorial_inwardMovement)
                currentSheep.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            yield return null;
        }

        //Green sheep
        currentSheep = Instantiate(sheepObject, topSpawn, Quaternion.identity);

        currentSheep.GetComponent<Tutorial_SheepScript>().ObtainColor("green");
        Debug.Log("EventTwo has started");
        while (currentSheep != null)
        {
            if (currentSheep.transform.position.x > tutorial_inwardMovement)
                currentSheep.transform.Translate(Vector2.left * tutorial_moveSpeed * Time.deltaTime);
            yield return null;
        }

    }

    //The roadblock
    IEnumerator EventFour()
    {
        GameObject roadblock;
        roadblock = Instantiate(roadblockObject, spawnPoint.position, Quaternion.identity);
        //while ()
        yield return null;
    }

}
