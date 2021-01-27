using Godot;
using System;

public class HitBox : CollisionShape2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

     [Export] float x_coor =0, y_coor =0, wall_thickness =4, wall_height=100, wall_width=50;
    
 
    [Export]public float fill_percent = 0.1f;
    //Vector2 pos = Position;


    public override void _Draw(){
       
      //  x_coor = pos.x;
      //  y_coor = pos.y;

        
        draw_tank(x_coor,y_coor,wall_thickness,wall_height,wall_width,fill_percent);
    }
    public void draw_tank(float x,float y, float thickness,float height,float width, float fill_percent){
        var tank_wall_color = new Color("7F7F7F");
        var water_color = new Color ("0000FF");

        DrawRect(new Rect2(x,y,thickness,height),tank_wall_color); //left tank wall
	    DrawRect(new Rect2(x+width,y,thickness,height),tank_wall_color);   //right tank wall
	    DrawRect(new Rect2(x+thickness,y+height-thickness,width,thickness),tank_wall_color); //bottom tank wall
	
	    DrawRect(new Rect2(x+thickness,(y+height-thickness)-(height-thickness)*fill_percent,width-thickness,(height-thickness)*fill_percent),water_color);

    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready(){
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){
        Update();
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
