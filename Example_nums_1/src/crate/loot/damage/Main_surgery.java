package crate.loot.damage;

import java.util.ArrayList;
import java.util.Random;
import java.util.Scanner;

public class Main_surgery {

	public static Scanner in = new Scanner(System.in);
	public static Random ran = new Random();
	public static void main(String[] args) {
		while(true) {
		System.out.println("Enter the newest index below, sir.");
		int index = in.nextInt();
		
		Headbummer head = new Headbummer(1);
		Lucidog dog = new Lucidog(2);
		Undead und = new Undead(3);
		switch(index) {
		case 1: head.claimReward(ran.nextInt(10)); break;
		case 2: dog.claimReward(ran.nextInt(10));  break;
		case 3: und.claimReward(ran.nextInt(10));  break;
		default: System.out.println("Go home, beasties <:)");
		}
		}
		
		
		
		
		
		
		
		
	}

}
