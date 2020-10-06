using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStageDoor : MonoBehaviour
{
    public GameObject _bound;
    public CameraManager _camera;
    public int _destStage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("들어옴");
        if("Player" == collision.gameObject.tag &&
            true == Input.GetButton("Fire1"))
        {
            Debug.Log("눌렀음");
            GameManager._instance.ChangeStage(_destStage);
            _camera.SetBound(_bound.GetComponent<BoxCollider2D>());

            SoundManager._instance.PlaySound("run");
        }
    }
}
