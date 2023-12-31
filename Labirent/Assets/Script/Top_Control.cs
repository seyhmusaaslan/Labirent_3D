using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top_Control : MonoBehaviour
{
    public UnityEngine.UI.Button btn;
    public UnityEngine.UI.Text zaman, can, Durum;
    private Rigidbody rg;
    public float hiz = 10f;
    float zamanSayacı = 25;
    int canSayaci = 10;
    bool oyunDevam = true;
    bool oyunTamam = false;
    void Start()
    {
        rg = GetComponent<Rigidbody> ();
    }

    void Update()
    {
        if (oyunDevam && !oyunTamam)
        {
            zamanSayacı -= Time.deltaTime;
            zaman.text = (int)zamanSayacı + "";
        }
        else if(!oyunTamam)
        {
            Durum.text = "Oyun Tamamlanamadi.";
            btn.gameObject.SetActive(true);
        }
        if(zamanSayacı<0)
        {
            oyunDevam = false;
        }
        
    }

    void FixedUpdate()
    {
        if (oyunDevam && !oyunTamam)
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(-dikey, 0, yatay);
            rg.AddForce(kuvvet * hiz);
        }
        else
        {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero;
        }

    }
    private void OnCollisionEnter(Collision cls)
    {
        Debug.Log(cls.gameObject.name);
        string objIsmi = cls.gameObject.name;
        if (objIsmi.Equals("Bitis_Noktası"))
        {
            //print("Oyun Tamamlandı");
            oyunTamam = true;
            Durum.text = "Oyun Tamamlandı Tebrikler!";
            btn.gameObject.SetActive(true);
        }
        else if( !objIsmi.Equals("Oyun_zemin") && !objIsmi.Equals("Zemin"))
        {
            canSayaci -= 1;
            can.text = canSayaci + "";
            if (canSayaci == 0) 
            {
                oyunDevam = false;
            }
        }

    }
}
