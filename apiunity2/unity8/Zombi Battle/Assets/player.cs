using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class player : MonoBehaviour
{
    public GameObject canvas;
    public GameObject DialogPanel1;
    public GameObject DialogPanel2;
    public GameObject DialogPanel3;
    public Text Dialog1text;
    public Text Dialog2text;
    public Text Dialog3text;
    public Text kultaText;
    public Text kokemusText;
    public Text Tehtavat;
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 8f;
    private bool isFacingRight = true;
    private bool canMove = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] public DataManager dataManager;
    public GameObject[] tekstit=new GameObject[3];
    public GameObject[] sienet = new GameObject[3];
    public GameObject[] vKukat = new GameObject[3];
    public GameObject[] pKukat = new GameObject[3];
    public string kuvaajaname;
    public int sieniCounter = 0;
    public int vkukkaCounter = 0;
    public int pkukkaCounter = 0;
    private int kulta = 0;
    private int kokemus = 0;
    [field: SerializeField] public Kuvaus myKuvaus { get; set; }
    public List<Quest> Listaus1;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            canvas.SetActive(true);
            canMove = false;
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump")&&IsGrounded()&&canMove==true)
        {
            rb.velocity=new Vector2 (rb.velocity.x, jumpingPower);
        }
        Flip();
        if (sieniCounter == 3)
        {
            SuoritaTehtava("1");
            sieniCounter = 0;
        }
        if (pkukkaCounter == 3)
        {
            SuoritaTehtava("2");
            pkukkaCounter = 0;
        }
        if (vkukkaCounter == 3)
        {
            SuoritaTehtava("3");
            vkukkaCounter = 0;
        }
    }
    private void FixedUpdate()
    {
        if(canMove==true)
        {
            rb.velocity=new Vector2 (horizontal*speed, rb.velocity.y);
        }
        if(canMove==false)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);
    }
    private void Flip()
    {
        if (isFacingRight&&horizontal<0f||!isFacingRight&&horizontal>0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale=transform.localScale;
            localScale.x*=-1f;
            transform.localScale = localScale;

        }
    }
    public void Resume()
    {
        canvas.SetActive(false);
        canMove = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC1"))
        {
            canMove = false;
            DialogPanel1.SetActive(true);
            dataManager.GetId("1");
        }
        if (collision.CompareTag("NPC2"))
        {
            canMove = false;
            DialogPanel2.SetActive(true);
            dataManager.GetId("2");

        }
        if (collision.CompareTag("NPC3"))
        {
            canMove = false;
            DialogPanel3.SetActive(true);
            dataManager.GetId("3");

        }
        if (collision.CompareTag("sieni"))
        {
            sieniCounter++;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("vKukka"))
        {
            vkukkaCounter++;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("pKukka"))
        {
            pkukkaCounter++;
            Destroy(collision.gameObject);
        }
    }
    public void AloitaTehtava(int id)
    {
        myKuvaus.onkoAloitettu = true;
        dataManager.PutData(id.ToString());
        canMove = true;
        tekstit[id-1].SetActive(false);
    }
    public void SuoritaTehtava(string id)
    {
        myKuvaus.onkoSuoritettu = true;
        dataManager.PutData(id);
        kulta+=myKuvaus.palkkioMaara;
        kokemus += myKuvaus.kokemusPisteet;
        UpdateText(kulta,kokemus);
    }

    public void Seuraava1(GameObject button )
    {
        kuvaajaname = myKuvaus.tehtavaKuvaus;
        Dialog1text.text = kuvaajaname;
        button.SetActive(false);
    }
    public void Seuraava2(GameObject button)
    {
        kuvaajaname = myKuvaus.tehtavaKuvaus;
        Dialog2text.text = kuvaajaname;
        button.SetActive(false);
    }
    public void Seuraava3(GameObject button)
    {
        kuvaajaname = myKuvaus.tehtavaKuvaus;
        Dialog3text.text = kuvaajaname;
        button.SetActive(false);
    }
    private void UpdateText(int kulta, int kokemus)
    {
        kultaText.text = "Kulta: "+kulta.ToString();
        kokemusText.text = "Kokemus: "+kokemus.ToString();
    }
    public void SieniTehtava()
    {
        foreach(GameObject sieni in sienet)
        {
            sieni.SetActive(true);
        }
    }
    public void VkukkaTehtava()
    {
        foreach (GameObject vkukka in vKukat)
        {
            vkukka.SetActive(true);
        }
    }
    public void PkukkaTehtava()
    {
        foreach(GameObject pkukka in pKukat)
        {
            pkukka.SetActive(true);
        }
    }
    public void ShowCompleted()
    {
        dataManager.GetCompleted();
        Tehtavat.text = "";
        foreach(Quest quest in Listaus1)
        {
            Tehtavat.text += quest.tehtavaNimi.ToString()+", ";
        }
    }
    public void ShowInProgress()
    {
        dataManager.GetInProgress();
        Tehtavat.text = "";
        foreach (Quest quest in Listaus1)
        {
            Tehtavat.text += quest.tehtavaNimi.ToString()+", ";
            
        }
    }
}
