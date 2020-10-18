using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LvlContrl : MonoBehaviour
{
    //ecran pour informer que la question est reussi
    //pas d'ecran de question echouer, partie perdue apres une mauvaise reponse
    public GameObject ecranQstReussie;
    public GameObject ecranPartiePerdue;

    public GameObject ecrancPartieGagnee;
    public GameObject joueur;

    //canvas ou les questions s'affichent
    public GameObject ecranQuestion;
    
    //Texte de la question courrante.
    public Text txtQuestionCourrante;

    //reponse entree par le joueur de la question courrante
    public Text reponseQuestionCourrante;

    public bool partieEstPerdue;

    //niveau de jeu, cela determinera les questions et le prochain niveau a charger.
    [SerializeField] int niveau;

    //Le temps que le joueur aura pour repondre aux questions
    [SerializeField]  int tempsDeReponse;

    //temps qui sera poser entre les differentes questions
    [SerializeField] int tempsEntreLesQuestion;

    //quand ce nombre de mauvaises reponses est atteint, la partie est perdue,
    //change selon le niveau.
    [SerializeField] int nbMauvaisesRepMax;

    //nombre de question qui on deja ete posee.
    private int nbQuestionsPoser=0;
    private PauseMenu scriptEcranPause;

    //le nombre de mauvaise rep
    private int nbMauvaisesRep;

 
    //Liste de questions du niveau courrant
    private List<Question> questionsNiv= new List<Question>();

    ///Craetion des Questions pour le niveau 1 (3 questions)
    private Question question1Niv1= new Question("Quelle est la capitale du canada?", "ottawa");
    private Question question2Niv1= new Question("Quel est le prenom du develloppeur de ce jeu? ", "hedi");
    private Question question3Niv1= new Question("Dans quel continent se situe la tunisie?", "afrique");

    //Creation des Questions pour le niveau 2(4 questions)
    private Question question1Niv2= new Question("10x19?", "190");
    private Question question2Niv2= new Question("Premier ministre du Canada?", "Justin Trudeau");
    private Question question3Niv2= new Question("En quelle année se termine la 2e Guerre Mondiale", "1945");
    private Question question4Niv2= new Question("En quel language est fait ce jeu?", "C#");

   

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale=1;
        partieEstPerdue=false;
         //on va y mettre le script de l'ecran de pause, cela va aider pour la coroutine des questions
        scriptEcranPause= GameObject.FindGameObjectWithTag("canvasPauseLvl1").GetComponent<PauseMenu>();
    
        //ajout des questions a la liste de questions selon le niveau
        //niveau 1
        if(niveau==1){
            questionsNiv.Add(question1Niv1);
            questionsNiv.Add(question2Niv1);
            questionsNiv.Add(question3Niv1);
        }

        //niveau 2
        else if(niveau==2){
            questionsNiv.Add(question1Niv2);
            questionsNiv.Add(question2Niv2);
            questionsNiv.Add(question3Niv2);
            questionsNiv.Add(question4Niv2);
        }
        StartCoroutine(coroutinePoserQuestion());
    }

    // Update is called once per frame
    void Update()
    {
        //si le joueur atteint le nombres de mauvaises reponses max, la partie est perdue
        if(nbMauvaisesRep==nbMauvaisesRepMax){
           StartCoroutine (GameOverCoroutine());
        }
    }

    //coroutine pour poser une question au joueur, s'arrete quand il ne reste plus de question a poser.
    //cela m'a pris 7h a faire :)
    IEnumerator coroutinePoserQuestion(){
        // si la coroutine est starter et que toutes les questions ne sont pas poser, on continue.

            while(true){
                
                // on att le temps entre chaque questions.
                // utilisation de WaitForSecondsRealtime puisque j'arrette le temps en montrant une question.
                yield return new WaitForSecondsRealtime(tempsEntreLesQuestion);
        
                if (!scriptEcranPause.IsPaused && !partieEstPerdue){
                    //on interdit la pause du jeu.
                    scriptEcranPause.CanBePaused=false;
                    
                    txtQuestionCourrante.text=questionsNiv[nbQuestionsPoser].QuestionTxt;
                    ecranQuestion.SetActive(true);

                    Time.timeScale=0;
                    
                    yield return new WaitForSecondsRealtime(tempsDeReponse);
                    RepondreQstCourrante();
                    ecranQuestion.SetActive(false);

                    //si reponse valide, le message s'affiche et on attend 2 sec
                    if(ecranQstReussie.activeSelf) yield return new WaitForSecondsRealtime(2);
                    ecranQstReussie.SetActive(false);
                    Time.timeScale=1;
                    //on permet la pause de jeu.
                    scriptEcranPause.CanBePaused=true;
                }

                // si la partie est perdue, on arette de poser des questions.
                else if(partieEstPerdue){
                    break;
                }
         }
    }

    //methode qui tente une reponse a la question courrante
    //ajoute 1 au nombre de mauvaise reponse si la reponse est
    public void RepondreQstCourrante(){
        bool reussi=questionsNiv[nbQuestionsPoser].checkReponse(reponseQuestionCourrante.text);
        nbQuestionsPoser+=1;
        if(reussi) 
        {
           ecranQstReussie.SetActive(true);
           
        }

        else
        {
            nbMauvaisesRep+=1;
        }
    }

    public IEnumerator GameOverCoroutine(){
        //on attend pour montrer l'animation d'explosion
        partieEstPerdue=true;
        yield return new WaitForSecondsRealtime(1);
        joueur.SetActive(false);
        Time.timeScale=0;
        scriptEcranPause.CanBePaused=false;
        ecranPartiePerdue.SetActive(true);
    }

    public void RecommencerNiveau(){
        Time.timeScale=1;
        string niveauACharger= "Level"+niveau.ToString();
        SceneManager.LoadScene(niveauACharger);
    }


    public void Gagner(){
        StopAllCoroutines();
        Time.timeScale=0;
        joueur.SetActive(false);
        scriptEcranPause.CanBePaused=false;
        ecrancPartieGagnee.SetActive(true);
    }

    public void ProchainNiv(){
        Time.timeScale=1;
        string niveauACharger= "Level"+(niveau+1).ToString();
        SceneManager.LoadScene(niveauACharger);
    }
}

