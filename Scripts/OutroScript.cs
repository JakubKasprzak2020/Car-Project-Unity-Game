using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OutroScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textElement;
    [SerializeField] GameObject driver;
    [SerializeField] GameObject farmer;
    [SerializeField] GameObject car;
    [SerializeField] GameObject finishButton;
    private Animator driverAnimator;
    private Vector3 driverBeginingPosition = new Vector3(5.27f, 0, -16.4f);
    float carSpeed = 2f;
    float driverSpeed = 1f;
    float driverAnimatorSpeed = 0.3f;
    float carXborder = 5.5f;
    float driverZborder = -12.8f;
    float displayTime = 0.15f;
    float startDelay = 1f;
    bool isCarMoveFinished = false;
    bool isDriverMoveFinished = false;
    string fullText1 = "-Hi! I came to see Abigail.";
    string partialText1 = "";
    string fullText2 = "-I'm sorry my son. She's not home. I hope that your road was good.";
    string partialText2 = "";
    string fullText3 = "-Yeah, I saw a tank...";
    string partialText3 = "";

    // Start is called before the first frame update
    void Start()
    {
        driver.SetActive(false);
        driverAnimator = driver.GetComponent<Animator>();
        driver.transform.position = driverBeginingPosition;
        InvokeRepeating("DoDialogue", startDelay, displayTime);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCar();
        StopCarOnBorder();
        MoveDriver();
        StopDriverOnBorder();
        ShowFinishButtonOnMouseClick();
    }

    void MoveCar()
    {
        if (!isCarMoveFinished)
        {
            Vector3 carVec = car.transform.position;
            carVec.x -= Time.deltaTime * carSpeed;
            car.transform.position = carVec;
        }
    }

    void StopCarOnBorder()
    {
        if (car.transform.position.x < carXborder && !isCarMoveFinished)
        {
            isCarMoveFinished = true;
        }
    }

    void MoveDriver()
    {
        if (isCarMoveFinished && !driver.activeSelf)
        {
            driver.SetActive(true);
        } else if (driver.activeSelf && !isDriverMoveFinished)
        {
            driverAnimator.SetFloat("Speed_f", driverAnimatorSpeed);
            Vector3 driverVec = driver.transform.position;
            driverVec.z += Time.deltaTime * driverSpeed;
            driver.transform.position = driverVec;
        }
    }

    void StopDriverOnBorder()
    {
        if (driver.transform.position.z > driverZborder && !isDriverMoveFinished)
        {
            isDriverMoveFinished = true;
            driverAnimator.SetFloat("Speed_f", 0);
        }
    }

    void DoDialogue()
    {
        if (isDriverMoveFinished && fullText1.Length > partialText1.Length)
        {
            partialText1 = BuildText(fullText1, partialText1);
        }
        else if (isDriverMoveFinished && fullText2.Length > partialText2.Length)
        {
            partialText2 = BuildText(fullText2, partialText2);
        } else if (isDriverMoveFinished && fullText3.Length > partialText3.Length)
        {
            partialText3 = BuildText(fullText3, partialText3);
        } else if (fullText3.Length == partialText3.Length)
        {
            ShowFinishButton();
            CancelInvoke();
        }
    }

    private string BuildText(string fullText, string partialText)
    {
        partialText = fullText.Substring(0, partialText.Length + 1);
        textElement.text = partialText;
        return partialText;
    }

    private void ShowFinishButton()
    {
        finishButton.SetActive(true);
    }

    public void EndGame()
    {
        Debug.Log("Click!");
        SceneManager.LoadScene("Menu");
    }

    private void ShowFinishButtonOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShowFinishButton();
        }
    }


}
