using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text tryagain;
    private Rigidbody rb;
    private int count;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count =0 ;
        setCountText();
        tryagain.gameObject.SetActive(false);
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        if (rb.position.y >=0 )
        {
          if (Input.GetKeyDown ("space")) {
          Vector3 jump = new Vector3 (0.0f, 200.0f, 0.0f);
          GetComponent<Rigidbody>().AddForce (jump);
          }
          Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

          rb.AddForce (movement * speed);
        }
        else{
            StartCoroutine (ExecuteAfterTime("Tente Novamente!"));


        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ("Pick Up Flower"))
        {
            other.gameObject.SetActive (false);
            count = count + 2;
            setCountText();

        }
        if (other.gameObject.CompareTag ("Pick Up"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            setCountText();


        }

        if (other.gameObject.CompareTag ("Kill lion") )
        {
          count = 0 ;
          setCountText();
          StartCoroutine (ExecuteAfterTime("Tente Novamente!"));


        }
    }
    void setCountText()
    {
      countText.text = "Pontuação: " +  count.ToString ();
      if (count >= 9){
        StartCoroutine (ExecuteAfterTime("Você Ganhou!!"));

      }

    }
 IEnumerator ExecuteAfterTime(string te)
    {
        tryagain.gameObject.SetActive(true);
        tryagain.text = te;

        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("SampleScene");
        count = 0 ;
        setCountText();


    }


}
