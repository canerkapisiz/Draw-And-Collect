using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("topGirdi"))
        {
            gameManager.DevamEt(transform.position);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("oyunBitii"))
        {
            gameManager.OyunBitti();
            gameObject.SetActive(false);
        }
    }
}
