using UnityEngine;

public class Fruit : MonoBehaviour
{
    // [SerializeField] private GameObject fruitHalfPrefab;  // Prefab of each half of the fruit
    [SerializeField] private float destroyThresholdY = -6f; // Y-position below which fruit is destroyed
    // [SerializeField] private float splitForce = 5f; // Force applied to each half

    private bool isSliced = false; // To prevent multiple slices of the same fruit

    private void Update()
    {
        // Destroy the fruit if it falls below the threshold
        if (transform.position.y < destroyThresholdY && !isSliced)
        {
            Destroy(gameObject);
            ScoreManager.Instance.SubScore(1);
            ScoreManager.Instance.SubLives(1);
        }
        
        if (transform.position.y < destroyThresholdY && !isSliced && tag == "Bomb")
        {
            Destroy(gameObject);
        }
    }

    public void Slice()
    {
        if (isSliced) return; // Avoid multiple slices
        isSliced = true;

        if (tag == "Bomb")
        {
            ScoreManager.Instance.SubScore(1);
            ScoreManager.Instance.SubLives(1);
        }
        else
        {
            // Add score when the fruit is sliced
            ScoreManager.Instance.AddScore(1); // You can change this value for different scores based on fruit type
        }

        // Create two halves from the prefab
        // GameObject half1 = Instantiate(fruitHalfPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(-45, 45)));
        // GameObject half2 = Instantiate(fruitHalfPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(-45, 45)));

        // Add force to each half to simulate them flying apart
        // Rigidbody2D rb1 = half1.GetComponent<Rigidbody2D>();
        // Rigidbody2D rb2 = half2.GetComponent<Rigidbody2D>();

        // if (rb1 != null)
        // {
        //     rb1.AddForce(new Vector2(-1, 1) * splitForce, ForceMode2D.Impulse);
        // }
        // if (rb2 != null)
        // {
        //     rb2.AddForce(new Vector2(1, 1) * splitForce, ForceMode2D.Impulse);
        // }

        // Destroy the original fruit
        Destroy(gameObject);
    }
}