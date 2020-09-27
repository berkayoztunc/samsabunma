using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("Game objects:")]
    public GameObject Enemy;
    public GameObject EnemyBigger;
    public GameObject OverGanePanel;
    public GameObject winGamePanel;
    public GameObject ligths;
    public GameObject tripleKillPanel;
    public GameObject doubleKillPanel;

    [Header("Setter:")]

    public float enemyDistance = 2.0f;
    public float enemyWaveCount = 2.0f;
    public float enemyWaveCountEnemy = 2.0f;

    public float radius = 40.0f;
    public int cockroachCount = 0;
    public float totalCockroachCount = 1;
    [Header("IU instance:")]
    public Text cockroach;
    public Text comboText;
    public List<Text> renadomTexts;

    public static GameManager staticManager;
    private bool combo = false;
    public bool spelled = false;
    public bool gameStatus = true;
    public bool readyToPlay = false;
    public int level=1;

    public int comboCount = 0;
    public string[] metinler =
    {
        "\"Biraz daha uyusam bütün bu olanlardan kurtulabilir miyim?\"",
        "\"Herkes, beraberinde taşıdığı bir parmaklığın ardında yaşıyor.\"",
        "\"Müzikten bu denli etkilendiğine göre,bir hayvan mıydı gerçekten?\"",
        "\"Gelgelelim insan hep hastalığını ayakta geçirebileceğine inanıyor.\"",
        "\"Gerisin geri eski konumuna kaydı. 'Şu erken kalkma yok mu,' diye düşündü, 'insanı aptallaştırıyor. İnsan uykusunu almalı.\"",
        "\"Öyküm uyutmazdı beni, ama sen düşlerle birlikte uykuyu getiriyorsun bana.\"",
        "\"Kendi içimde zaman zaman boğuluyorum.\"",
        "\"Herkes beraberinde taşıdığı bir parmaklığın ardında yaşıyor\"",
        "\"Paltom bile ağır gelirken, nasıl taşırım koskoca dünyayı sırtımda?\"",
        "\"Tüm çaresizliğine rağmen gülümsemekten kendini alamadı.\"",
        "\"...sürekli değişen hiç kalıcı ve samimi olmayan insan ilişkileri.\"",
        "\"İnsanın yaşı kaç olursa olsun, ağlarken hep kimsesiz bir çocuktur..\"",
        "\"Sürekli değişen, hiç süreklilik kazanmayan asla samimileşmeyen insan ilişkileri. Yerin dibine batsın...\"",
        "\"Ölmekten müthiş bir şekilde korkuyordu.Çünkü henüz gerçek anlamda yaşamamıştı.\"",
        "\"Müzikten böylesine etkilendiğine göre, bir hayvan olabilir miydi? Sanki özlediği, o bilinmeyen gıdaya giden yol karşısına çıkıvermişti.\"",
        "\"Zira boş bir duvarın görüntüsü kalbine ağır bir yük gibi biniyordu.\"",
        "\"Alelacele koşup yaşama sığınmıyorsa insan, yaşamdan zevk alabilir mi?\"",
        "\"Kendimden başka hiçbir eksiğim yok\"",
        "\"Anlaşılan insanlar onun ne dediğini anlamıyorlardı artık, oysa o, sesinin anlaşılır olduğunu sanmıştı, öncekinden daha anlaşılır, belki de kulakları kendi sesini alışmıştı.\""
    };


    // Start is called before the first frame update
    void Start()
    {
        gamePlaner();
        Time.timeScale = 0;
        if (staticManager == null)
        {
            staticManager = this;
        }
       


    }
    void gamePlaner(){
        level = PrefManager.GetInt("last_level");
        if(level <= 0)
        {
            PrefManager.SetInt("last_level",1);
            level = 1;
        } 
        int gameType = PrefManager.GetInt("GameType");
        if (gameType <= 0)
        {
            PrefManager.SetInt("last_level", 1);
            level = 1;
        }
        enemyWaveCount = level == 1 && gameType == 1 ? 3 : level ;
        enemyWaveCountEnemy = gameType * level;

        
        var bossCount = level > 2 ? 1 : 0;

        totalCockroachCount = (enemyWaveCount * enemyWaveCountEnemy) + bossCount;
        
        for (var i = 0; i < renadomTexts.Count; i++)
        {
            renadomTexts[i].text = "\"" + metinler[Random.Range(0, metinler.Length)] + "\"";
        }
        StartCoroutine(UpdateEverySecond());
    }

    // Update is called once per frame
    void Update()
    {
        if(totalCockroachCount == 0)
        {
            winGame();
        }
        
    }
    public static void winGame()
    {
    
        if (staticManager.gameStatus)
        {

            staticManager.gameStatus = false;
            staticManager.level++;
            PrefManager.SetInt("last_level", staticManager.level);
            staticManager.winGamePanel.SetActive(true);
            LeanTween.moveY(staticManager.winGamePanel, 850, 1);
            staticManager.StartCoroutine(staticManager.stopTheGame());
        }


    }
    public void touched()
    {
        ligths.SetActive(true);
        StartCoroutine(ligthOF());
    }
    public void bigComboKill()
    {
        doubleKillPanel.SetActive(false);
        tripleKillPanel.SetActive(true);
        StartCoroutine(offPenel(tripleKillPanel));
    }
    public void doubleKill()
    {
       
        doubleKillPanel.SetActive(true);
        StartCoroutine(offPenel(doubleKillPanel));
    }
    public static void gameOver()
    {
        if (staticManager.gameStatus)
        {
            
            staticManager.gameStatus = false;
            staticManager.OverGanePanel.SetActive(true);
            LeanTween.moveY(staticManager.OverGanePanel, 850, 1);
            staticManager.StartCoroutine(staticManager.stopTheGame()) ;
        }
        

    }
    IEnumerator  stopTheGame()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }
    public static void countCoconocho()
    {
        staticManager.cockroachCount++;
        staticManager.totalCockroachCount--;
        staticManager.cockroach.text = staticManager.cockroachCount.ToString();
        staticManager.comboCount++;
        //Debug.Log(staticManager.comboCount);
        if(staticManager.comboCount > 4)
        {
            
            staticManager.bigComboKill();
            staticManager.comboText.text = "x" + staticManager.comboCount.ToString();
        }
        else
        if (staticManager.comboCount > 2)
        {

            staticManager.doubleKill();
        }
    }

    IEnumerator offPenel(GameObject objects)
    {
        yield return new WaitForSeconds(0.8f);
        objects.SetActive(false);
    }
    IEnumerator UpdateEverySecond(){
        for (int i = 0; i < enemyWaveCount; i++)
        {
            yield return new WaitForSeconds(enemyDistance);
            StartCoroutine(Resp());
            
        }
        if(level > 2)
        {
            Instantiate(EnemyBigger, positionRandom(), Quaternion.identity);
        }

    }
    IEnumerator ligthOF()
    {
        yield return new WaitForSeconds(0.05f);
        ligths.SetActive(false);
    }
    IEnumerator Resp()
    {


        for (int j = 0; j < enemyWaveCountEnemy; j++)
        {
            
            yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));

            // Random.Range(1, 3);
            // tip  yapıcaz dairesel uzaklıkta canlanıcak
            Instantiate(Enemy, positionRandom(), Quaternion.identity);
        }
    }
    public Vector3 positionRandom()
    {
        var randomX = Random.Range(1, (radius / 2));
        var randomy = Mathf.Sqrt(radius * radius - randomX * randomX);

        //var Distance = GregorSamsa + new Vector3(0,0,10);
        var side = 1;
        if (Random.Range(0, 2) == 1)
        {
            side = -1;
        }
        Vector3 position = new Vector3(randomX * side, 0, randomy);
        return position;
    }
}
