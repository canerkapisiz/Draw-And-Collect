using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CizgiCiz : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject cizgi;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> parmakPozsiyonListesi;
    public List<GameObject> cizgiler;

    bool cizmekMumkunMu;
    int cizmeHakki;

    [SerializeField] private TextMeshProUGUI cizmeHakkiText;

    private void Start()
    {
        cizmekMumkunMu = false;
        cizmeHakki = 3;
        cizmeHakkiText.text = cizmeHakki.ToString();
    }

    void Update()
    {
        if (cizmekMumkunMu && cizmeHakki !=0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CizgiOlustur();
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 parmakPozisyonu = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(parmakPozisyonu, parmakPozsiyonListesi[^1]) > 0.1f)
                {
                    CizgiyiGuncelle(parmakPozisyonu);
                }
            }
        }

        if(cizgiler.Count!=0 && cizmeHakki != 0)
        {
            if (Input.GetMouseButtonUp(0))
            {
                cizmeHakki--;
                cizmeHakkiText.text = cizmeHakki.ToString();
            }
        }
    }

    void CizgiOlustur()
    {
        cizgi = Instantiate(linePrefab, Vector2.zero, Quaternion.identity);
        cizgiler.Add(cizgi);
        lineRenderer = cizgi.GetComponent<LineRenderer>();
        edgeCollider = cizgi.GetComponent<EdgeCollider2D>();
        parmakPozsiyonListesi.Clear();
        parmakPozsiyonListesi.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        parmakPozsiyonListesi.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, parmakPozsiyonListesi[0]);
        lineRenderer.SetPosition(1, parmakPozsiyonListesi[1]);
        edgeCollider.points = parmakPozsiyonListesi.ToArray();
    }

    void CizgiyiGuncelle(Vector2 gelenParmakPozsiyonu)
    {
        parmakPozsiyonListesi.Add(gelenParmakPozsiyonu);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, gelenParmakPozsiyonu);
        edgeCollider.points = parmakPozsiyonListesi.ToArray();
    }

    public void DevamEt()
    {
        foreach (var item in cizgiler)
        {
            Destroy(item.gameObject);
        }

        cizgiler.Clear();

        cizmeHakki = 3;
        cizmeHakkiText.text = cizmeHakki.ToString();
    }

    public void CizmeyiDurdur()
    {
        cizmekMumkunMu = false;
    }

    public void CizmeyiBaslat()
    {
        cizmeHakki = 3;
        cizmeHakkiText.text = cizmeHakki.ToString();
        cizmekMumkunMu = true;
    }
}
