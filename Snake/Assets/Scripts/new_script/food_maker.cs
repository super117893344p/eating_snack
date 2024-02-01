using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class food_maker : MonoBehaviour
{
	private static food_maker _instance;
	public static food_maker Instance
	{
		get { return _instance; }
	}
	public int time = 0; private int count = 0;
	public int xlimit = 21;
	public int ylimit = 11;
	public int xoffset = 7;
	public GameObject food_prefer;
	public GameObject reward_prefer;
	public Sprite[] foods_sprites;
	private Transform food_holder;
	// Start is called before the first frame update
	void Awake()
	{
		_instance = this;
	}
	void Start()
	{
		food_holder = GameObject.Find("FoodHolder").transform;
		make_food(true);
	}

	// Update is called once per frame
	public void make_food(bool is_rewarding)
	{
		int index = Random.Range(0, foods_sprites.Length);
		GameObject food = Instantiate(food_prefer);
		food.GetComponent<Image>().sprite = foods_sprites[index];
		food.transform.SetParent(food_holder.transform, false);
		int x = Random.Range(-xlimit + xoffset, xlimit);
		int y = Random.Range(-ylimit, ylimit);
		food.transform.localPosition = new Vector3(x * 30, y * 30, 0);
		if (is_rewarding)
		{
			GameObject reward = Instantiate(reward_prefer);
			int a = Random.Range(-xlimit + xoffset, xlimit);
			int b = Random.Range(-ylimit, ylimit);
			reward.transform.localPosition = new Vector3(a * 30, b * 30, 0);
			reward.transform.SetParent(food_holder, false);
		}
	}
	void Update()
	{
		time = time + 1;
		if (time >= 60)
		{
			make_food(false);
			time = 0; count++;
		}
		else if (count >= 10)
		{
			make_food(true); count = 0;
		}
	}
}