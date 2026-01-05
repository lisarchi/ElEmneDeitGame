//using System.Collections.Generic;
//using UnityEngine;

//public class MessageManager : MonoBehaviour
//{
//    public static MessageManager Instance;

//    [Header("Список картинок М")]
//    public List<Sprite> picturesMen; 
    
//    [Header("Список картинок Ж")]
//    public List<Sprite> picturesWomen;

//    private Queue<Sprite> pictureQueue;

//    [SerializeField] int _pictureSet;

//    private void Awake()
//    {
//        Instance = this;

//        SaveData.Load();
//        _pictureSet = SaveData.data.PictureSet;
//        LoadSet(_pictureSet);
//    }

//    public void LoadSet(int _pictureSet)
//    {
//        List<Sprite> selectedList = (_pictureSet == 1) ? picturesMen : picturesWomen;
//        Shuffle(selectedList);
//    }

//    private void Shuffle(List<Sprite> list)
//    {
//        List<Sprite> shuffled = new List<Sprite>(list);

//        for (int i = 0; i < shuffled.Count; i++)
//        {
//            Sprite temp = shuffled[i];
//            int rand = Random.Range(i, shuffled.Count);
//            shuffled[i] = shuffled[rand];
//            shuffled[rand] = temp;
//        }

//        pictureQueue = new Queue<Sprite>(shuffled);
//    }

//    public Sprite GetNextPicture()
//    {
//        if (pictureQueue == null || pictureQueue.Count == 0)
//            return null;

//        return pictureQueue.Dequeue();
//    }
//}