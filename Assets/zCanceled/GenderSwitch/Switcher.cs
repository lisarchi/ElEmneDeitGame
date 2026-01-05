//using UnityEngine;
//using UnityEngine.UI;


//public class Switcher : MonoBehaviour
//{
//    public Slider _slider;
//    public int _pictureSet;

//    private void Start()
//    {
//        SaveData.Load();
//        _pictureSet = SaveData.data.PictureSet;
//        _slider.value = _pictureSet;
//    }

//    public void SwitchGender()
//    {
        
//        if (_slider.value == 1)
//        {
//            _pictureSet = 1;
//            SaveData.data.PictureSet = 1;
//            SaveData.Save();
//        }
//        if (_slider.value == 2)
//        {
//            _pictureSet = 2;
//            SaveData.data.PictureSet = 2;
//            SaveData.Save();
//        }
//    }
//}
