using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    #region singleton
    void Awake()
    {
        if (null == _instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }
    #endregion singleton

    public int _score;
    public int _currentStage;
    public int _life;

    public Text _scoreText;
    public Text _lifeText;

    public Potion _potion;

    public Potion[] _potions;

    public Image _menuSet;
    public Image _retrySet;

    public Image _menu;
    public Image _option;

    public Slider _sliderBGM;
    public Slider _sliderSound;

    public GameObject[] _stages;
    public GameObject[] _Enemys;

    public Player _player;

    void Start()
    {
        _life   = 0;
        _score  = 0;
        _scoreText.text = _score.ToString();

        _potions = new Potion[5];

        for (int i = 0; i < 5; i++)
        {
            _potions[i] = Instantiate(_potion, transform);
        }

        _sliderBGM.SetValueWithoutNotify(SoundManager._instance.GetVolumeBGM());
        _sliderSound.SetValueWithoutNotify(SoundManager._instance.GetVolumeSound());
    }

    void Update()
    {
        if(true == Input.GetButtonDown("Cancel"))
        {
            _menu.gameObject.SetActive(!_menuSet.gameObject.activeSelf);
            _menuSet.gameObject.SetActive(!_menuSet.gameObject.activeSelf);

            _option.gameObject.SetActive(false);        //옵션창은 무조건 꺼진다.
        }

    }

    public void ScoreUp(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString();
    }

    public void SetLifeText(int life)
    {
        _life = life;
        _lifeText.text = _life.ToString();
    }

    public void CreatePotion(Vector2 position)
    {
        for (int i = 0; i < 5; i++)
        {
            if (false == _potions[i].gameObject.activeSelf)
            {
                _potions[i].gameObject.SetActive(true);
                _potions[i].transform.position = position;
                return;
            }
        }

        Debug.Log("모든 포션이 활성화");
    }

    public void ButtonOption()
    {
        _menu.gameObject.SetActive(false);
        _option.gameObject.SetActive(true);
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void ChangeStage(int stage)
    {
        _stages[_currentStage].SetActive(false);
        _stages[stage].SetActive(true);
        _currentStage = stage;

        _player.ResetPosition();
        ResetStage();
    }

    public void SetRetryActive(bool active)
    {
        _retrySet.gameObject.SetActive(active);
    }

    public void ResetStage()
    {
        Actor[] children = _Enemys[_currentStage].GetComponentsInChildren<Actor>(true);
        
        foreach (Actor child in children)
        {
            child.GraveBurst();
        }

        for (int i = 0; i < 5; i++)
        {
            _potions[i].gameObject.SetActive(false);
        }
    }
}
