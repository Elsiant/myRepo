using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int _recovery;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRecovery(int value)
    {
        _recovery = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ("Player" == collision.gameObject.tag)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.LifeUp(_recovery);

            Debug.Log(_recovery + "회복");

            this.gameObject.SetActive(false);

            SoundManager._instance.PlaySound("lifeUp");
        }
    }
}
