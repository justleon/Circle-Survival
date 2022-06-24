using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Circle : MonoBehaviour, IPointerClickHandler
{
    float explosionTime;
    float timeElapsed = 0.0f;

    SpriteRenderer spriteRenderer;

    [SerializeField] float timeLimit = 1.0f;
    [SerializeField] float upperLimit = 4.0f;
    [SerializeField] float bottomLimit = 2.0f;
    [SerializeField] float upperFactor = 0.09f;
    [SerializeField] float bottomFactor = 0.0333f;

    [SerializeField] Image fillUp;
    [SerializeField] GameObject burst;
    [SerializeField] GameObject explosion;

    [SerializeField] AudioSource popSFX;
    [SerializeField] AudioSource boomSFX;

    void Start()
    {
        explosionTime = DetonationTimeRandomizer();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.instance.isRunning)
        {
            GameManager.instance.points++;
            Instantiate(burst, transform.position, Quaternion.identity);
            popSFX.Play();
            spriteRenderer.enabled = false;
            fillUp.enabled = false;
            Destroy(gameObject, popSFX.clip.length);
        }
    }

    void Update()
    {
        if(GameManager.instance.isRunning)
        {
            if (timeElapsed < explosionTime)
            {
                timeElapsed += Time.deltaTime;
                fillUp.fillAmount = timeElapsed / explosionTime;
            }
            else
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                boomSFX.Play();
                spriteRenderer.enabled = false;
                fillUp.enabled = false;
                Destroy(gameObject, boomSFX.clip.length);

                GameManager.instance.EndGame();
            }
        }
    }
    float DetonationTimeRandomizer()
    {
        if (upperLimit > timeLimit || bottomLimit > timeLimit)
        {
            int modifier = Mathf.FloorToInt(GameManager.instance.timer.timeVal / 5);
            upperLimit = upperLimit - (upperFactor * modifier);
            bottomLimit = Mathf.Max(bottomLimit - (bottomFactor * modifier), timeLimit);
        }

        return Random.Range(bottomLimit, upperLimit);
    }
}
