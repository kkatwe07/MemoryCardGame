using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 4; 
    public const int gridCols = 5; 
    public const float OffsetX = 3f; 
    public const float OffsetY = 2f; 

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] images;

    private void Start()
    {
        Vector3 StartPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (OffsetX * i) + StartPos.x;
                float posY = (OffsetY * j) + StartPos.y;
                card.transform.position = new Vector3(posX, posY, StartPos.z);

            }
        }

    }   

    public int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0; i< newArray.Length; i++)
        {
            int temp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = temp;
        }
        return newArray;
    }

    //------------------------------------------------------------------------------------------------------------------------------

    private MainCard firstRevealed;
    private MainCard secondRevealed;

    private int score = 0;
    [SerializeField] private TextMesh scoreLabel;

    public bool CanReveal
    {
        get { return secondRevealed == null; }
    }

    public void CardRevealed(MainCard card)
    {
        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        if(firstRevealed.id == secondRevealed.id)
        {
            score++;
            scoreLabel.text = "Score : " + score;
            
            if (score == 10)
            {
                yield return new WaitForSeconds(2F); 
                NextScene(); 
            }//new code
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
        }

        firstRevealed = null;
        secondRevealed = null;
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }
}
