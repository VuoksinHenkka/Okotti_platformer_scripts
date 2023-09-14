using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.UI;
using TMPro;

public class UI_MainMenu : MonoBehaviour
{

    public GameObject mainMenuObjects;
    public GameObject optionsMenuObjects;
    public Bus volumeBus_Master;
    public Slider volumeSlider_Master;
    public TMP_Text value_volumeslider;
    private float currentvalue_volumeslider;

    private void Update()
    {
        if (Manager_Game.Instance.currentGameState != Manager_Game.State.Action) return;
        if (Input.GetKeyDown(KeyCode.Escape)) OpenMenu();
    }

    private void Start()
    {
        volumeBus_Master = RuntimeManager.GetBus("bus:/");
        volumeBus_Master.getVolume(out currentvalue_volumeslider);
        volumeSlider_Master.SetValueWithoutNotify(currentvalue_volumeslider);
        value_volumeslider.text = ((currentvalue_volumeslider * 100).ToString("000") + "%");

        OpenMenu();
    }

    public void slider_volume(float volume)
    {
        currentvalue_volumeslider = volume;
        volumeBus_Master.setVolume(currentvalue_volumeslider);
        value_volumeslider.text = ((currentvalue_volumeslider*100).ToString("000") + "%");
    }

    public void OpenMenu()
    {
        Manager_Game.Instance.currentGameState = Manager_Game.State.Menu;
        mainMenuObjects.SetActive(true);
        optionsMenuObjects.SetActive(false);

    }

    public void OpenOptions()
    {
        Manager_Game.Instance.currentGameState = Manager_Game.State.Menu;
        mainMenuObjects.SetActive(false);
        optionsMenuObjects.SetActive(true);
    }

    public void StartGame()
    {
        mainMenuObjects.SetActive(false);
        Manager_Game.Instance.currentGameState = Manager_Game.State.Action;
        optionsMenuObjects.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
