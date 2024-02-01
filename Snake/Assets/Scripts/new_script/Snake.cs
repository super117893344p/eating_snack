using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Snake : MonoBehaviour
{
	public List<Transform> body_list = new List<Transform>();
	public float velocity = 0.35f;
	public int step;
	private int x, y;
	private Vector3 head_position;
	private Transform canvas;
	private bool is_die = false;
	public AudioClip eat_clip;
	public AudioClip die_clip;
	public GameObject dead_effects;
	public GameObject body_prefab;
	public Sprite[] body_sprite = new Sprite[2];
	void Awake()
	{
		canvas = GameObject.Find("Canvas").transform;
		gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>
			(PlayerPrefs.GetString("sh", "sh02"));
		body_sprite[0] = Resources.Load<Sprite>(PlayerPrefs.GetString("sh01", "sh0201"));
		body_sprite[1] = Resources.Load<Sprite>(PlayerPrefs.GetString("sh02", "sh0202"));
	}
	void Start()
	{
		InvokeRepeating("Move", 0, velocity);
		x = 0; y = step;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.W))
		{
			x = 0; y = step;
		}
		if (Input.GetKey(KeyCode.S))
		{
			x = 0; y = -step;
		}
		if (Input.GetKey(KeyCode.D))
		{
			x = step; y = 0;
		}
		if (Input.GetKey(KeyCode.A))
		{
			x = -step; y = 0;
		}
		if (Input.GetKeyDown(KeyCode.Space) && game_UI_controller.Instance.is_pause == false && is_die == false)
		{
			CancelInvoke();
			InvokeRepeating("Move", 0, velocity - 0.2f);
		}
		if (Input.GetKeyUp(KeyCode.Space) && game_UI_controller.Instance.is_pause == false && is_die == false)
		{
			CancelInvoke();
			InvokeRepeating("Move", 0, velocity);
		}
	}

	void Move()
	{
		head_position = gameObject.transform.localPosition;                                               //保存下来蛇头移动前的位置
		gameObject.transform.localPosition = new Vector3(head_position.x + x, head_position.y + y, head_position.z);  //蛇头向期望位置移动

		if (body_list.Count > 0)
		{
			for (int i = body_list.Count - 2; i >= 0; i--)
			{
				body_list[i + 1].localPosition = body_list[i].localPosition;
			}
			body_list[0].localPosition = head_position;
		}
	}
	void Body_Growing()
	{
		AudioSource.PlayClipAtPoint(eat_clip, Vector3.zero);
		int index = (body_list.Count % 2 == 0) ? 0 : 1;
		GameObject body = Instantiate(body_prefab, new Vector3(2000, 2000, 0), Quaternion.identity);
		body.GetComponent<Image>().sprite = body_sprite[index];
		body.transform.SetParent(canvas, false);
		body_list.Add(body.transform);
	}
	void Die()
	{
		AudioSource.PlayClipAtPoint(die_clip, Vector3.zero);
		CancelInvoke(); is_die = true;
		Instantiate(dead_effects);
		PlayerPrefs.SetInt("lastl", game_UI_controller.Instance.length);
		PlayerPrefs.SetInt("lasts", game_UI_controller.Instance.score);
		if (PlayerPrefs.GetInt("bests", 0) <= game_UI_controller.Instance.score)
		{
			PlayerPrefs.SetInt("bestl", game_UI_controller.Instance.length);
			PlayerPrefs.SetInt("bests", game_UI_controller.Instance.score);
		}
		StartCoroutine(GameOver(1.5f)); //  1.5s游戏结束
	}
	IEnumerator GameOver(float time)
	{
		yield return new WaitForSeconds(time);
		UnityEngine.SceneManagement.SceneManager.LoadScene(1);
	}
	private void Touching_Foods_Rewards(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Food"))
		{
			Destroy(collision.gameObject);
			game_UI_controller.Instance.updateUI();
			Body_Growing();
			food_maker.Instance.make_food((Random.Range(0, 100) < 20) ? true : false);
		}
		else if (collision.gameObject.CompareTag("Reward"))
		{
			Destroy(collision.gameObject);
			game_UI_controller.Instance.updateUI();
			Body_Growing();
		}
		else if (collision.gameObject.CompareTag("Body"))
		{
			Destroy(collision.gameObject); Die();
		}
		else
		{
			if (game_UI_controller.Instance.is_having_border)
			{
				Die();
			}
			else
			{
				switch (collision.gameObject.name)
				{
					case "Up":
						transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y + 30, transform.localPosition.z); break;
					case "Down":
						transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y - 30, transform.localPosition.z); break;
					case "Left":
						transform.localPosition = new Vector3(-transform.localPosition.x + 180, transform.localPosition.y, transform.localPosition.z); break;
					case "Right":
						transform.localPosition = new Vector3(-transform.localPosition.x + 240, transform.localPosition.y, transform.localPosition.z); break;
					default:
						break;
				}
			}
		}
	}
}