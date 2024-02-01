using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class start_UI_controller : MonoBehaviour
{
	public Text lastText;
	public Text bestText;
	public Toggle blue;
	public Toggle yellow;
	public Toggle border;  //toggle ��ʾ���ػ��߸�ѡ��״̬
	public Toggle noBorder;
	void Awake()
	{
		lastText.text = "�ϴΣ�����" + PlayerPrefs.GetInt("lastl", 0) + "������" + PlayerPrefs.GetInt("lasts", 0);
		bestText.text = "��ã�����" + PlayerPrefs.GetInt("bestl", 0) + "������" + PlayerPrefs.GetInt("bests", 0);
	}

	// Start is called before the first frame update
	void Start()
	{
		if (PlayerPrefs.GetString("sh", "sh01") == "sh01")
		{
			blue.isOn = true;
			PlayerPrefs.SetString("sh", "sh01");
			PlayerPrefs.SetString("sb01", "sb0101");
			PlayerPrefs.SetString("sb02", "sb0102");
		}
		else
		{
			yellow.isOn = true;
			PlayerPrefs.SetString("sh", "sh02");
			PlayerPrefs.SetString("sb01", "sb0201");
			PlayerPrefs.SetString("sb02", "sb0202");
		}
		if (PlayerPrefs.GetInt("border", 1) == 1)
		{
			border.isOn = true;
			PlayerPrefs.SetInt("border", 1);
		}
		else
		{
			noBorder.isOn = true;
			PlayerPrefs.SetInt("border", 0);
		}
	}
	public void blue_select(bool is_on)
	{
		if (is_on)
		{
			PlayerPrefs.SetString("sh", "sh01");
			PlayerPrefs.SetString("sb01", "sb0101");
			PlayerPrefs.SetString("sb02", "sb0202");
		}
	}
	public void YellowSelected(bool isOn)
	{
		if (isOn)
		{
			PlayerPrefs.SetString("sh", "sh02");
			PlayerPrefs.SetString("sb01", "sb0201");
			PlayerPrefs.SetString("sb02", "sb0202");
		}
	}

	public void BorderSelected(bool isOn)
	{
		if (isOn)
		{
			PlayerPrefs.SetInt("border", 1);
		}
	}

	public void NoBorderSelected(bool isOn)
	{
		if (isOn)
		{
			PlayerPrefs.SetInt("border", 0);
		}
	}
	public void start_game()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(1);
	}
}