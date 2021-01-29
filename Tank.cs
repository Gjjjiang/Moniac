using Godot;
using System;
using System.Runtime.InteropServices;
 [StructLayout(LayoutKind.Sequential)] public struct ExtU_water_tank_T{
     public double flow_in;
     public double flow_out;
     public double StartVol;
 }; 

[StructLayout(LayoutKind.Sequential)]public struct ExtY_water_tank_T{
    public double CurrentVol;  
};
public class Tank : KinematicBody2D{
    //======= Import functions from water_tank_win64.dll ===========
    
    //initializes the water tank matlab model
    [DllImport("water_tank_win64.dll")] public static extern void water_tank_initialize(); 
    
    //steps the simulation forward by 1/60 seconds
    [DllImport("water_tank_win64.dll")] public static extern void water_tank_step(); 
    
    //water tank model destructor
    [DllImport("water_tank_win64.dll")] public static extern void water_tank_terminate(); 
   
    //sets flow rate in
    [DllImport("water_tank_win64.dll")] public static extern void _setFlowIn(double FlowInInput); 
    

    [DllImport("water_tank_win64.dll")] public static extern void _setFlowOut(double FlowOutInput); //sets flow rate out
    
    //sets initial volume
    [DllImport("water_tank_win64.dll")] public static extern void _setStartVol(double StartVolInput);
    
    //returns current volume, calculated from differential equation dV/dt = flow in - flow out
    [DllImport("water_tank_win64.dll")] public static extern double _getCurrentVol(); 
    //===================================================================
    [Export]public float x_coor =0, y_coor =0, wall_thickness =4, wall_height=100, wall_width=50;
    [Export]public float fill_percent;
    [Export]public float crossArea, maxVol, currentVol;
   
    public override void _Draw(){
       draw_tank(x_coor,y_coor,wall_thickness,wall_height,wall_width,fill_percent);
    }

    public void draw_tank(float x,float y, float thickness,float height,float width, float fill_percent){
        //Align info page to Tank
        updateTankInfo();

        //Set CollisionShape2D hitbox over tank drawing
        CollisionShape2D TankHitbox = (CollisionShape2D)GetNode("HitBox");
        RectangleShape2D hitboxShape =  (RectangleShape2D)(TankHitbox.Shape);
        hitboxShape.Extents= new Vector2(wall_width/2+wall_thickness, (wall_height)/2);
        TankHitbox.Shape = hitboxShape;
        TankHitbox.Position = new Vector2(x_coor+wall_width/2+wall_thickness,y_coor+(wall_height)/2);
        
        //Draw tank
        Color tank_wall_color = new Color("7F7F7F");
        Color water_color = new Color ("0000FF");
        DrawRect(new Rect2(x,y,thickness,height),tank_wall_color); //left tank wall
	    DrawRect(new Rect2(x+width+thickness,y,thickness,height),tank_wall_color);   //right tank wall
	    DrawRect(new Rect2(x+thickness,y+height-thickness,width,thickness),tank_wall_color); //bottom tank wall
        
        //Draw Water
	    DrawRect(new Rect2(x+thickness,(y+height-thickness)-(height-thickness)*fill_percent,width,(height-thickness)*fill_percent),water_color);

    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready(){
        water_tank_initialize();

        currentVol = 0.0f;
        maxVol = 0.0f;
        crossArea = 0.0f;
        updateTankInfo();
        ((RichTextLabel) GetNode("TankInfo")).Visible=false;
        ((Button) GetNode("Edit")).Visible=false; 
        ((Button) GetNode("Hide Info")).Visible=false;
        
    }
    


    public override void _PhysicsProcess(float delta){
        Update();
        water_tank_step();
        //GD.Print(_getCurrentVol());
    }

    
    public void updateTankInfo(){
        ((RichTextLabel)GetNode("TankInfo")).BbcodeText = 
        "Current Volume: " + currentVol.ToString() + " m^3\n" +
        "Water Height: "+ (currentVol/crossArea).ToString()+ " m \n" + 
        "Cross Sectional Area: " + (crossArea).ToString()+ "m^2";
    }

    //Signal Emitters and Listeners

    public void _on_Hide_Info_pressed(){
        ((RichTextLabel) GetNode("TankInfo")).Visible=false;
        ((Button) GetNode("Edit")).Visible=false; 
        ((Button) GetNode("Hide Info")).Visible=false;
    }


    [Signal]
    public delegate void _on_tank_EditInfo(Tank watertank);
    public void _on_Edit_pressed(){
        EmitSignal(nameof(_on_tank_EditInfo),this);
    }

    //Mouse Event listeners
    bool in_tank=false;
    public void _on_Tank_mouse_entered(){
        in_tank=true;
        //GD.Print(in_tank);
    }
    public void _on_Tank_mouse_exited(){
        in_tank=false;
        //GD.Print(in_tank);
    }
    public override void _Input(InputEvent @event){
        // Listens for mouse click
        if (@event is InputEventMouseButton eventMouseButton&&in_tank){
            RichTextLabel TankInfo = (RichTextLabel) GetNode("TankInfo");
            Button Edit = (Button) GetNode("Edit"); 
            Button Hide = (Button) GetNode("Hide Info");
            
            TankInfo.SetPosition(new Vector2(x_coor,y_coor));
            Edit.SetPosition(new Vector2(x_coor,y_coor+TankInfo.RectSize.y));
            Hide.SetPosition(new Vector2(x_coor+Edit.RectSize.x+4,y_coor+TankInfo.RectSize.y));
            
            ((RichTextLabel) GetNode("TankInfo")).Visible=true;
            ((Button) GetNode("Edit")).Visible=true; 
            ((Button) GetNode("Hide Info")).Visible=true;
        }
        
    }

    public void _on_EditValue_send_and_set_new_values(Tank input){
        //this.x_coor = input.x_coor;
        //Change respective variables
    }

    
}
