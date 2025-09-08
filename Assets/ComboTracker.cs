using NUnit.Framework.Internal;
using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class ComboTracker : MonoBehaviour
{
    [SerializeField]
    TMP_Text ScoreText,PotScoreText;
    [SerializeField]
    PlayerController Player;
    [SerializeField]
    TMP_Text VelText, AirText, GrappleText;
    [SerializeField]
    GameObject MathText,ComboTotal;
    public Vector3 lastGrappled;

    public int potentialScore;
    private int compoundScore;
    public int DsiplayScore;
    public int Score = 0;
    public int grappleInARow = 0;
    public float airTime = 0;

    public Image scoreFlair;

    private bool newGrapple;
    private bool isGrappled = false;
    private bool isGrounded = false;
    private bool died = false;
    public int fillAmnt;

    private bool boolToBeToggledForTheAddedTextThingCauseICantCode = false;
    Color flairColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ComboTotal.GetComponent<TMP_Text>().enabled = false;
        changeFlairColor();
    }

    // Update is called once per frame
    void changeFlairColor()
    {
        float R;
        float G;
        float B;

        int chosen = UnityEngine.Random.Range(1,3);

        switch(chosen)
        {
            case(1):
                R = 1;
                G = UnityEngine.Random.Range(0f,1f);
                B = UnityEngine.Random.Range(0f, 1f);
                break;
            case(2):
                R = UnityEngine.Random.Range(0f, 1f);
                G = 1;
                B = UnityEngine.Random.Range(0f, 1f);
                break;
            case(3):
                R = UnityEngine.Random.Range(0f, 1f);
                G = UnityEngine.Random.Range(0f, 1f);
                B = 1;
                break;
            default:
                R = 1f;
                G = 1f; 
                B = 1f;
                break;
        }

        flairColor = new Color(R, G, B);
        scoreFlair.color = flairColor;
        ComboTotal.GetComponent <TMP_Text>().color = flairColor;
        PotScoreText.color = flairColor;
    }
    void Update()
    {

        scoreFlair.fillAmount = (float)fillAmnt/100f;

        if (DsiplayScore < 999999)
        {
            ScoreText.text = DsiplayScore.ToString();
        }
        else if (DsiplayScore < 999999999)
        {
            ScoreText.text = Math.Round(DsiplayScore / 1000000f, 2) + "M";
        }
        else
        {
            ScoreText.text = "Stop :(";
        }
        if (died)
        {
            hideText();
            potentialScore = 0;
            compoundScore = 0;
            grappleInARow = 0;
            died = false;
            DsiplayScore = Score;
            ComboTotal.GetComponent<TMP_Text>().enabled = false;
        }
        if (isGrappled)
        {
            potentialScore = Player.getMagnitude() * Convert.ToInt32(airTime) * grappleInARow + compoundScore;
            
        }
        if (grappleInARow > 0)
        {
            VelText.text = "Velocity(" + Player.getMagnitude() + ")";
            AirText.text = "Air-Time(" + MathF.Round(airTime, 2)  + "s)";
            GrappleText.text = "Grapple(" + grappleInARow + ")";
            PotScoreText.text = potentialScore.ToString();
            ComboTotal.GetComponent<TMP_Text>().enabled = true;
            ComboTotal.GetComponent<TMP_Text>().text = potentialScore.ToString();
            boolToBeToggledForTheAddedTextThingCauseICantCode = true;
            MathText.SetActive(true);
        }
        if (isGrounded && !isGrappled)
        {
            airTime = 0;
            hideText();
            addScore();

            potentialScore = 0;
            compoundScore = 0;
            grappleInARow = 0;
        }
        if (!isGrounded && !isGrappled)
        {
            airTime += Time.deltaTime;
        }
    }
    public void hideText()
    {
        PotScoreText.text = "";
        VelText.text = "";
        AirText.text = "";
        GrappleText.text = "";
        if (boolToBeToggledForTheAddedTextThingCauseICantCode == true && potentialScore > 0) { ComboTotal.GetComponent<Animator>().Play("DepositScore"); }
        boolToBeToggledForTheAddedTextThingCauseICantCode = false;
        MathText.SetActive(false);
    }
    public void addScore()
    {
        Score += potentialScore;
        StartCoroutine("Ticker");
    }
    public void setIsGrappled(bool isG, Vector3 grappled)
    {
        if (lastGrappled != grappled)
        {
            newGrapple = true;
            compoundScore = potentialScore;
            lastGrappled = grappled;
            grappleInARow++;
        }
        isGrappled = isG;
    }
    public void setIsGrounded(bool isG)
    {
        isGrounded = isG;
    }
    public void getRespawned(bool respawn)
    {
        died = respawn;
    }
    public IEnumerator Ticker()
    {
        while (DsiplayScore != Score)
        {
            ScoreText.color = flairColor;
            //we don't want to increment CurrentScore to infinity, so we only do it if it's lower than TargetScore
            if (DsiplayScore < Score)
            {
                DsiplayScore += 132;
                fillAmnt += 1;
                if(fillAmnt >= 100)
                {
                    fillAmnt = 0;
                    changeFlairColor();
                }
                //this is a 'safety net' to ensure we never exceed our TargetScore
                if (DsiplayScore > Score)
                {
                    DsiplayScore = Score;
                }
            }

            //wait for some time before incrementing again
            yield return new WaitForSeconds(0.1f);

        }
        ScoreText.color = Color.white;
    }
}
