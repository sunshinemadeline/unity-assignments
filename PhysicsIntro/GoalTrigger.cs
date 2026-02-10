using UnityEngine;
using UnityEngine.UI;

public class GoalTrigger : MonoBehaviour
{
    public Text victoryText;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            victoryText.gameObject.SetActive(true);
            victoryText.text = "You Win!";
            Debug.Log("You Win!");
        }
    }
}
