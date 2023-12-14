using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    private float speed = 10f;
    private Rigidbody rb;
    public Text counttext;
    public Text winText;
    private int count;
    public Button restartButton;
    public Renderer renderer;
    public Text highestscore;
    //public Text score;
    public Text prevusscore;
    private int highestScore; // Variable to store the highest score

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
        restartButton.gameObject.SetActive(false);

        // Load the highest score from PlayerPrefs
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);

        // Display the highest score
        highestscore.text = "Highest Score: " + highestScore.ToString();

      //  prevusscore.text = count.ToString();
          Debug.Log(highestScore);
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            renderer = other.GetComponent<Renderer>();
            if (renderer.material.color == Color.red)
            {
                renderer.gameObject.SetActive(false);
                count = count + 1;
                setCountText();

                // Check if the current score is higher than the highest score
                if (count > highestScore)
                {
                    highestScore = count;
                    highestscore.text = "Highest Score: " + highestScore.ToString();

                    // Save the new highest score to PlayerPrefs
                    PlayerPrefs.SetInt("HighestScore", highestScore);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                renderer.material.color = Color.red;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            speed = 0;
            winText.text = "Game Over ";
            restartButton.gameObject.SetActive(true);
        }
    }

    private void setCountText()
    {
        counttext.text = "Count: " + count.ToString();

        if (count >= 8)
        {
            winText.text = "You win";
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        restartButton.gameObject.SetActive(false);
       // setHeightScore();
    }}

//     private void setHeightScore()
//     {
// //        prevusscore.text = count.ToString();
//     }
// }

