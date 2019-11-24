package crate.loot.damage;

import java.util.ArrayList;
import java.util.Random;

public class Undead {
	
	
	int INDEX;
	public Undead(int index) {
		index = INDEX;
		System.out.println("You may visit an undeads!");
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
		ArrayList<String> items = new ArrayList<>();
		items.add("'Head of the unleashed undead'");
		items.add("'Shoulders of undead bones'");
		String itemYouWon = items.get((int) Math.random());
		System.out.println("Grab your new item! -> " + itemYouWon);
	}
}
