package crate.loot.damage;

import java.util.ArrayList;
import java.util.Random;

public class Headbummer {
	
	int INDEX;
	public Headbummer(int index) {
		index = INDEX;
		System.out.println("You may visit a headbummers!");
	}
	private static int damage;
	private static int gold;
	public static Random randomReward = new Random();
	
	public static void claimReward(int indexOf)
	{
		for(int i=1;i<indexOf;i++) {
			gold = randomReward.nextInt(100);
			System.out.println("You receive gold: + " + gold);
		}
		String[] items = new String[2];
		items[0] = "'Head of the bum'";
		items[1] = "'Shoulders of bummer'";
		//String itemYouWon = items.toS;
		//System.out.println("Grab your new item! -> " + itemYouWon);
		//System.out.println(randomReward.toString(items));
	}
}
