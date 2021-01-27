using Godot;
using System;

public class Options : RichTextLabel
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready(){
        
        Connect("meta_clicked",GetNode("Options"),"_on_Options_meta_clicked");
    }
    public void _on_Options_meta_clicked(String meta){
        GD.Print("hi");
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
