using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textElement;
    [SerializeField] GameObject driver;
    [SerializeField] GameObject car;
    private Animator driverAnimator;
    private Vector3 driverBeginingPosition = new Vector3(1.6f, 0, -4.6f);
    bool textIsCompleted = false;
    string fullText = "Hi! My name is Rick and this is my car. Help me get to my girlfriend. But be careful, because the road is dangerous. Use arrow keys to steer the car.";
    string partialText = "";
    float displayTime = 0.15f;
    float startDelay = 1f;
    float driverAnimatorSpeed = 0.3f;
    float driverSpeed = 0.5f;
    float driverRotationSpeed = 40f;
    float carRotationSpeed = 40f;
    float carSpeed = 5f;
    float borderToHideDriver = -0.5f;
    float carBorderToStartGame = 15f;


    // Start is called before the first frame update
    void Start()
    {
        driver.SetActive(true);
        driver.transform.position = driverBeginingPosition;
        driverAnimator = driver.GetComponent<Animator>();
        InvokeRepeating("BuildText", startDelay, displayTime);
    }

    // Update is called once per frame
    void Update()
    {
        SkipIntroOnMouseButton();
        MoveDriver();
        HideDriverOnBorder();
        MoveCar();
        StartGameAfterIntro();
    }

    private void BuildText()
    {

        if (fullText.Length > partialText.Length)
        {
            partialText = fullText.Substring(0, partialText.Length + 1);
            textElement.text = partialText;
        } else if (!textIsCompleted)
        {
            textIsCompleted = true;
            CancelInvoke();
        }
    }

    private void MoveDriver()
    {
        if (textIsCompleted)
        {
            MoveDriverLeft();
            TurnDriverLeft();
        }
    }
    
    private void MoveDriverLeft()
    {
        driverAnimator.SetFloat("Speed_f", driverAnimatorSpeed);
        Vector3 driverVec = driver.transform.position;
        driverVec.x += Time.deltaTime * -(driverSpeed);
        driver.transform.position = driverVec;
    }

    private void TurnDriverLeft()
    {
        if (driver.transform.rotation.eulerAngles.y < 270)
        {
            driver.transform.RotateAround(driver.transform.position, Vector3.up, Time.deltaTime * driverRotationSpeed);
        }
    }

    private void HideDriverOnBorder()
    {
        if (driver.transform.position.x < borderToHideDriver)
        {
            driver.SetActive(false);
        }
    }

    private void StartGameAfterIntro()
    {
        if (car.transform.position.z > carBorderToStartGame)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("With Graphic");
    }


    private void MoveCar()
    {
        if (!driver.activeInHierarchy)
        {
            CarRotateOrMoveForeward();
        }
    }

    private void CarRotateOrMoveForeward()
    {
        if (car.transform.rotation.y < 0)
        {
            car.transform.RotateAround(car.transform.position, Vector3.up, Time.deltaTime * carRotationSpeed);
        }
        else
        {
            Vector3 carVec = car.transform.position;
            carVec.z += Time.deltaTime * carSpeed;
            car.transform.position = carVec;
        }
    }

    private void SkipIntroOnMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SkipTextOrSkipAnimation();
        }
    }

    private void SkipTextOrSkipAnimation()
    {
        if (!textIsCompleted)
        {
            partialText = fullText;
            textElement.text = partialText;
        }
        else
        {
            StartGame();
        }
    }


}
