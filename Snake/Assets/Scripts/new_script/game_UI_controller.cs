using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class game_UI_controller : MonoBehaviour
{
	private static game_UI_controller _instance;
	public static game_UI_controller Instance
	{
		get { return _instance; }
	}
	public bool is_having_border = true;
	public bool is_pause = false;
	public int score; public int length;
	public Text messageText; public Text score_text;
	public Text length_text; public Image pause_image;
	public Sprite[] pause_sprites; public Image background_image;
	public Color temp_color;
	void Awake()
	{
		_instance = this;
	}
	void Start()
	{
		if (PlayerPrefs.GetInt("border", 1) == 0)
		{
			is_having_border = false;
			foreach (Transform t in background_image.gameObject.transform)
			{
				t.gameObject.GetComponent<Image>().enabled = false;
			}
		}
	}

	void Update()
	{
		switch (score)
		{
			case 400:
				ColorUtility.TryParseHtmlString("#CCEEFFFF", out temp_color);
				background_image.color = temp_color;
				messageText.text = "阶段" + 2; break;

			case 600:
				ColorUtility.TryParseHtmlString("#CCFFDBFF", out temp_color);
				background_image.color = temp_color;
				messageText.text = "阶段" + 3; break;
			case 800:
				ColorUtility.TryParseHtmlString("#EBFFCCFF", out temp_color);
				background_image.color = temp_color;
				messageText.text = "阶段" + 4; break;
			case 1000:
				ColorUtility.TryParseHtmlString("#FFF3CCFF", out temp_color);
				background_image.color = temp_color;
				messageText.text = "阶段" + 5; break;
			default:
				ColorUtility.TryParseHtmlString("#FFDACCFF", out temp_color);
				background_image.color = temp_color;
				messageText.text = "无尽阶段";
				break;
		}
	}
	public void updateUI()
	{
		int s = 0; int l = 0;
		s = 5; l = 1;
		score = score + s; length = length + l;
		score_text.text = "当前分数: " + score;
		length_text.text = "当前长度: " + length;
	}
	public void pause_game()
	{
		is_pause = true;
		if (is_pause)
		{
			Time.timeScale = 0;
			pause_image.sprite = pause_sprites[1];
		}
		else
		{
			Time.timeScale = 1;
			pause_image.sprite = pause_sprites[0];
		}
	}
	public void Home()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}