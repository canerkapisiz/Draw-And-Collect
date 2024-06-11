using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopAtar : MonoBehaviour
{
    [SerializeField] private GameObject[] toplar;
    [SerializeField] private GameObject topAtarMerkezi;
    [SerializeField] private GameObject kova;
    [SerializeField] private GameObject[] kovaNoktalari;
    int aktifTopIndex;
    int randomKovaPintIndex;
    bool kilit;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OyunBaslasin()
    {
        StartCoroutine(TopAtisSistemi());
    }

    IEnumerator TopAtisSistemi()
    {
        while (true)
        {
            if (!kilit)
            {
                yield return new WaitForSeconds(0.5f);

                kova.SetActive(false);
                toplar[aktifTopIndex].transform.position = topAtarMerkezi.transform.position;
                toplar[aktifTopIndex].SetActive(true);

                float aci = Random.Range(70f, 110f);
                Vector3 pos = Quaternion.AngleAxis(aci, Vector3.forward) * Vector3.right;
                toplar[aktifTopIndex].GetComponent<Rigidbody2D>().AddForce(pos * 750);

                if (aktifTopIndex != toplar.Length - 1)
                {
                    aktifTopIndex++;
                }
                else
                {
                    aktifTopIndex = 0;
                }

                yield return new WaitForSeconds(0.7f);

                randomKovaPintIndex = Random.Range(0, kovaNoktalari.Length - 1);
                kova.transform.position = kovaNoktalari[randomKovaPintIndex].transform.position;
                kova.SetActive(true);
                kilit = true;

                Invoke("TopuKontrolEt", 4.5f);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void DevamEt()
    {
        kilit = false;
        kova.SetActive(false);
        CancelInvoke();
    }

    public void TopAtmaDurdur()
    {
        StopAllCoroutines();
    }

    void TopuKontrolEt()
    {
        if (kilit)
        {
            GetComponent<GameManager>().OyunBitti();
        }
    }
}
