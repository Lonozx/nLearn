package od.build.book;
import java.util.Random;
import java.util.Scanner;

public class HiLo {

	public static Scanner in = new Scanner(System.in);
	public static Random rand = new Random();
	public static void main(String[] args) {
		
		GuessNumber();

	}
	//guess the number game
	public static void GuessNumber() {
		boolean statement = true;
		String playAgain = "";
		int guess = 0;
		do {
		int number = rand.nextInt(100);
		System.out.println("Randomly created number is equals to " + number);
		int x = 0;
		while(guess!=number) {
			
			x++;
			System.out.println("Enter the number you guess: ");
			guess = in.nextInt();
			if(guess == number) {
				System.out.println("You win! Number is " + guess + ".");
				System.out.println("The number of steps you make is " + x + ".");
				System.out.println("Goodnight.");
//				break;
			} else if(guess < number) {
				System.out.println("The number is too low!");
				System.out.println("The number of steps you make is " + x + ".");
			}else if(guess > number) {
				System.out.println("The number is too high!");
				System.out.println("The number of steps you make is " + x + ".");
			}else if((guess-1) == number) {
				System.out.println("One more up!");
			}else if((guess+1) == number) {
				System.out.println("One more down!");
			}
		}
		System.out.println("Its getting late. Goodbye!");
		System.out.println("Would you like to play again? y/n");
		playAgain = in.next();
		}	while(playAgain.equalsIgnoreCase("y"));
	}
	
}
