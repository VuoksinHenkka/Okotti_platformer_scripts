using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_HUD : MonoBehaviour
{
    [SerializeField]  public TMP_Text TEXT_INPUT;
    private Vector3 input = Vector3.zero;
    private Vector3 target = Vector3.zero;
    public bool showINPUT = false;


    [SerializeField] public TMP_Text TEXT_SCORE_COLLECTED;
    public UI_TMP_ColorFlash colorFlash_scorecollected;
    public UI_PopScale popSCale_scorecollected;


    [SerializeField] public TMP_Text TEXT_SCORE_LEFT;
    public UI_TMP_ColorFlash colorFlash_scoreleft;

    [SerializeField] public TMP_Text TEXT_GAME_FINISHED;


    private int score = -1;
    private int score_left = -200;
    private void Update() //toi mun UI elementti mikä näytti inputit
    {
        if (Manager_Game.Instance.currentGameState == Manager_Game.State.Action)
        {
            Update_GamePlayHUD();
        }
        else if (Manager_Game.Instance.currentGameState == Manager_Game.State.LevelFinished)
        {
            Update_Gameplay_Finished_HUD();
        }

    }
    private void Update_Gameplay_Finished_HUD()
    {
        if (TEXT_SCORE_LEFT.gameObject.activeSelf) TEXT_SCORE_LEFT.gameObject.SetActive(false);
        if (TEXT_SCORE_COLLECTED.gameObject.activeSelf) TEXT_SCORE_COLLECTED.gameObject.SetActive(false);

        if (TEXT_GAME_FINISHED.gameObject.activeSelf == false) TEXT_GAME_FINISHED.gameObject.SetActive(true);



        //DEBUG ROINA
        if (!showINPUT) return;
        if (TEXT_INPUT.gameObject.activeSelf) TEXT_INPUT.gameObject.SetActive(false);


            
    }
    private void Update_GamePlayHUD()
    {

        if (score != Manager_Game.Instance.Score)
        {

            score = Manager_Game.Instance.Score;
            TEXT_SCORE_COLLECTED.text = "Collected:" + score.ToString();
            popSCale_scorecollected.PopScale();
            colorFlash_scorecollected.Flash();
        }

        if (score_left != Manager_Game.Instance.ScoreLeft)
        {

            score_left = Manager_Game.Instance.ScoreLeft;
            TEXT_SCORE_LEFT.text = "Coins left: " + score_left.ToString();
            colorFlash_scoreleft.Flash();
        }

 
        if (TEXT_SCORE_COLLECTED.gameObject.activeSelf == false) TEXT_SCORE_COLLECTED.gameObject.SetActive(true);
        if (TEXT_SCORE_LEFT.gameObject.activeSelf == false) TEXT_SCORE_LEFT.gameObject.SetActive(true);
        if (TEXT_GAME_FINISHED.gameObject.activeSelf) TEXT_GAME_FINISHED.gameObject.SetActive(false);



        //DEBUG
        if (!showINPUT) return;

        target = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (input != target)
        {
            input = target;
            TEXT_INPUT.text = "Input: " + input.ToString();

        }

        if (TEXT_INPUT.gameObject.activeSelf == false) TEXT_INPUT.gameObject.SetActive(true);
    }
}
