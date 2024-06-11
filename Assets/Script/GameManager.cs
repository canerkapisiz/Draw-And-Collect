using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("----TOP VE TEKNIK OBJELER")]
    [SerializeField] private TopAtar topAtar;
    [SerializeField] private CizgiCiz cizgiCiz;

    [Header("----GENEL OBJELER")]
    [SerializeField] private ParticleSystem kovaGirme;
    [SerializeField] private ParticleSystem bestScoreGecis;

    [Header("----UI OBJELER")]
    [SerializeField] private GameObject[] paneller;
    [SerializeField] private TextMeshProUGUI[] scoreTextleri;

    int girenTopSayisi;

    void Start()
    {
        if (PlayerPrefs.HasKey("bestScore"))
        {
            scoreTextleri[0].text = PlayerPrefs.GetInt("bestScore").ToString();
            scoreTextleri[1].text = PlayerPrefs.GetInt("bestScore").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("bestScore", 0);
            scoreTextleri[0].text = PlayerPrefs.GetInt("bestScore").ToString();
            scoreTextleri[1].text = PlayerPrefs.GetInt("bestScore").ToString();
        }
    }

    void Update()
    {
        
    }

    public void DevamEt(Vector2 pos)
    {
        kovaGirme.transform.position = pos;
        kovaGirme.gameObject.SetActive(true);
        kovaGirme.Play();
        topAtar.DevamEt();
        cizgiCiz.DevamEt();
        girenTopSayisi++;
    }

    public void OyunBitti()
    {
        paneller[1].SetActive(true);
        paneller[2].SetActive(false);
        if (girenTopSayisi > PlayerPrefs.GetInt("bestScore"))
        {
            PlayerPrefs.SetInt("bestScore", girenTopSayisi);
            bestScoreGecis.Play();
        }
        scoreTextleri[1].text = PlayerPrefs.GetInt("bestScore").ToString();
        scoreTextleri[2].text = girenTopSayisi.ToString();
        topAtar.TopAtmaDurdur();
        cizgiCiz.CizmeyiDurdur();
    }

    public void OyunBaslasin()
    {
        paneller[0].SetActive(false);
        paneller[2].SetActive(true);
        topAtar.OyunBaslasin();
        cizgiCiz.CizmeyiBaslat();
    }

    public void TekrarOyna()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
