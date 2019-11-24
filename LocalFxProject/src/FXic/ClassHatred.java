package FXic;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;

import javax.swing.JOptionPane;

import javafx.animation.RotateTransition;
import javafx.application.Application;
import javafx.beans.binding.Bindings;
import javafx.geometry.Insets;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextArea;
import javafx.scene.control.TextField;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.Background;
import javafx.scene.layout.BackgroundFill;
import javafx.scene.layout.CornerRadii;
import javafx.scene.layout.Pane;
import javafx.scene.paint.Color;
import javafx.stage.Stage;
import javafx.util.Duration;

public class ClassHatred extends Application{

	public static boolean running = true;
	public static boolean keyPressed = false;
	public static int healingInt; 
	public static int damagingInt;
	public static int dealingInt;
	
	public static void main(String[] args) {
		launch(args);
	}

	Stage stage;
	Scene scenery;
	Pane windowBuild;
	Button buttonOn;
	Button buttonOff;
	Group group;
	TextArea text;
	TextField fieldOne;
	TextField fieldTwo;
	TextField fieldRes;
	Label labHP;
	Label addedHP;
	Button createNote;
	TextField textOn_0;
	TextField textOn_1;
	Button saveIt;
	
	@Override
	public void start(Stage stage) throws Exception {
	//this.stage = stage;	
	group = new Group();
	Image image = new Image("file:Stellar.jpg");
	ImageView view = new ImageView(image);
	
	windowBuild = new Pane();
	scenery = new Scene(group, 1250, 700);
	text = new TextArea();
	text.setPrefSize(200, 300);
	text.setTranslateX(200);
	text.setTranslateY(70);
	//text.setStyle("text-area-background: green;");
	labHP = new Label("|....................................|");
	labHP.setTranslateX(206);
	labHP.setTranslateY(348);
	addedHP = new Label("|....................................|");
	addedHP.setTranslateX(206);
	addedHP.setTranslateY(280);
	
	buttonOn = new Button("Heal it!");
	buttonOff = new Button("Deal it!");
	
	fieldOne = new TextField();
	fieldTwo = new TextField();
	fieldRes = new TextField();
	fieldRes.setEditable(false);
	fieldRes.setVisible(false);
	
	fieldOne.setTranslateX(6);
	fieldOne.setTranslateY(70);
	
	fieldTwo.setTranslateX(6);
	fieldTwo.setTranslateY(200);
	
	buttonOn.setTranslateX(50);
	buttonOn.setTranslateY(120);
	
	buttonOff.setTranslateX(50);
	buttonOff.setTranslateY(250);
	
	buttonOn.setOnMouseEntered(event -> {
		
		RotateTransition rt = new RotateTransition(Duration.millis(100), buttonOn);
	     rt.setByAngle(10);
	     rt.setCycleCount(8);
	     rt.setAutoReverse(true);
	 
	     rt.play();
	});
	
	buttonOff.setOnMouseEntered(event -> {
		
		RotateTransition rt = new RotateTransition(Duration.millis(100), buttonOff);
	     rt.setByAngle(10);
	     rt.setCycleCount(8);
	     rt.setAutoReverse(true);
	 
	     rt.play();
	});
	
	fieldOne.setOnMouseEntered(event -> {
		
		RotateTransition rt = new RotateTransition(Duration.millis(100), fieldOne);
	     rt.setByAngle(2);
	     rt.setCycleCount(4);
	     rt.setAutoReverse(true);
	 
	     rt.play();
	});
	fieldTwo.setOnMouseEntered(event -> {
		
		RotateTransition rt = new RotateTransition(Duration.millis(100), fieldTwo);
	     rt.setByAngle(2);
	     rt.setCycleCount(4);
	     rt.setAutoReverse(true);
	 
	     rt.play();
	});
	buttonOn.setOnAction(event -> {
		int allowOne = Integer.parseInt(fieldOne.getText());
		int summary = 0;
		healingInt = allowOne;
		dealingInt = summary;
		//int allowTwo = Integer.parseInt(fieldTwo.getText());
		
		//if(allowOne!=0) {
			
			dealingInt+=healingInt;
			
			addedHP.setText(fieldOne.getText());
			text.setText("Added: " + "\n");
			text.setText(healingInt + "HP");
			labHP.setText("Added " + fieldOne.getText() + "HP");
			System.out.println(healingInt);
					//	}
//			else {
//			JOptionPane.showMessageDialog(null, "This is not allowed, enter the number first!");
//			 }
		
	});
	buttonOff.setOnAction(event ->{
		
		int healing = Integer.parseInt(fieldOne.getText());
		int damaging = Integer.parseInt(fieldTwo.getText());
		int dealing = 0;

		dealingInt = dealing;
		healingInt = healing;
		damagingInt = damaging;
		while(true) {
		dealingInt = healingInt - damagingInt;
		
		addedHP.setText("Gotcha " + dealingInt);
		System.out.println("::Delete " + dealingInt);
		fieldRes.setText(dealingInt + "");
		text.setText(fieldRes.getText() + "");
		
		fieldOne.setText(fieldRes.getText());
		labHP.setText(fieldRes.getText() + " HP left...");
		
		
		
		text.setText("Now you need help..." + "\n" + "Dismiss -" + fieldTwo.getText() + " HP");
		//serios.deserialize();
		break;}
		
		
		
//		int allowOne = Integer.parseInt(fieldOne.getText());
//		int allowTwo = Integer.parseInt(fieldTwo.getText());
//		int minussy = 0;
//		healingInt = allowOne;
//		damagingInt = allowTwo;
//		dealingInt = minussy;
//			//if(healingInt!=0 && damagingInt!=0) {
//				while(running) {
//				dealingInt = healingInt - damagingInt;
//				System.out.println(dealingInt);
//				
//				text.setText("Edited now: " + "\n"
//				+ "-" + fieldTwo.getText() + "HP");
//				labHP.setText("Dismiss " + fieldTwo.getText() + "HP");
//				break;
//				}
				
									//		}
//			else {
//			JOptionPane.showMessageDialog(null, "This is not allowed, enter the number first!");
//			}
	});
	textOn_0 = new TextField();
	textOn_0.setVisible(false);
	textOn_0.setPrefSize(150, 300);
	textOn_0.setTranslateX(1000);
	textOn_0.setTranslateY(70);
	
	
	textOn_1 = new TextField();
	textOn_1.setVisible(false);
	textOn_1.setPrefSize(150, 300);
	textOn_1.setTranslateX(830);
	textOn_1.setTranslateY(70);
	
	
	saveIt = new Button("Save notes");
	saveIt.setVisible(false);
	saveIt.setPrefSize(100, 50);
	saveIt.setTranslateX(1050);
	saveIt.setTranslateY(380);
	
	
	createNote = new Button("Create note");
	createNote.setPrefSize(100, 50);
	createNote.setTranslateX(600);
	createNote.setTranslateY(320);
	createNote.setOnAction(event ->{
	
//			for(int i=0;i<1;i++)
//			{
				//Succas
				
				textOn_0.setVisible(true);
				//Adding some button for you
				
				saveIt.setVisible(true);
				
				if(textOn_0.localToScreen(1000, 100) != null) {
					
					textOn_1.setVisible(true);
				//}
			}
			
	});
	createNote.setOnMouseEntered(event -> {
		
		RotateTransition rt = new RotateTransition(Duration.millis(100), createNote);
	     rt.setByAngle(2);
	     rt.setCycleCount(8);
	     rt.setAutoReverse(true);
	 
	     rt.play();
	});
	textOn_0.setOnMouseEntered(event -> {
		
		RotateTransition rt = new RotateTransition(Duration.millis(100), textOn_0);
	     rt.setByAngle(2);
	     rt.setCycleCount(4);
	     rt.setAutoReverse(true);
	 
	     rt.play();
	});
	textOn_1.setOnMouseEntered(event -> {
		
		RotateTransition rt = new RotateTransition(Duration.millis(100), textOn_1);
	     rt.setByAngle(2);
	     rt.setCycleCount(4);
	     rt.setAutoReverse(true);
	 
	     rt.play();
	});
	saveIt.setOnMouseEntered(event -> {
		
		RotateTransition rt = new RotateTransition(Duration.millis(100), saveIt);
	     rt.setByAngle(2);
	     rt.setCycleCount(8);
	     rt.setAutoReverse(true);
	 
	     rt.play();
	});
	
	group.getChildren().addAll(view);
	group.getChildren().addAll(buttonOff, buttonOn, text);
	group.getChildren().addAll(fieldOne, fieldTwo, labHP, fieldRes, addedHP);
	group.getChildren().addAll(createNote, textOn_0, textOn_1, saveIt);
	//windowBuild.getChildren().add(view);
//	windowBuild.getChildren().addAll(buttonOn, buttonOff);
	stage.setTitle("Tree: waste of time tittle...1001");
	stage.setScene(scenery);
	stage.setResizable(false);
	stage.show();
	}

}

//Class<?> clazz = this.getClass();
//InputStream input = clazz.getClass().getResourceAsStream("/FXLocalShit/src/pics/Great.jpg");
//String input = "Great.jpg";
//Image image = new Image(input);
//File file = new File("https://www.google.com/url?sa=i&rct=j&q=&esrc=s&source=images&cd=&ved=2ahUKEwjliqOM2cLiAhUOxIsKHYQ8AckQjRx6BAgBEAU&url=https%3A%2F%2Fwww.dnaindia.com%2Findia%2Freport-see-pic-indian-shutterbug-wins-global-wikimedia-monument-photo-contest-2568047&psig=AOvVaw3I9QOyQI1U-FxwNtzI-fps&ust=1559284340302529");
//String localUrl = file.toURI().toURL().toString();
//Image image = new Image(localUrl);
//Image image = new Image(localUrl, 1280, 720, false, true);