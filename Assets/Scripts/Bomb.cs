using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bomb : MonoBehaviour, IPointerClickHandler
{
    SpriteRenderer spriteRenderer;

    [SerializeField] float expiryTime = 3.0f;

    [SerializeField] GameObject explosion;
    [SerializeField] AudioSource boomSFX;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        boomSFX.Play();
        spriteRenderer.enabled = false;
        Destroy(gameObject, boomSFX.clip.length);

        GameManager.instance.EndGame();
    }
    void Update()
    {
        if (expiryTime > 0)
            expiryTime -= Time.deltaTime;
        else
            Destroy(gameObject);
    }
}
