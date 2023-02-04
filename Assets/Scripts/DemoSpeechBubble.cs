using UnityEngine;

public class DemoSpeechBubble : MonoBehaviour
{
    public Transform[] Animals;
    public Transform Animal;

    public string[] Phrases;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Animal = Animals[Random.Range(0, Animals.Length)];
            string Phrase = Phrases[Random.Range(0, Phrases.Length)];
            SpeechBubble.Create(Animal, new Vector3(0, Animal.localPosition.y + 1f, 0), Phrase, 1f);
        }
    }
}
