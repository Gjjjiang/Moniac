using Godot;
using System;
using System.Collections.Generic; 
using System.Runtime.InteropServices;


public class Tank : KinematicBody2D{
    //======= Import functions from water_tank_win64.dll ===========
    [DllImport ("water_tank_win64")] static extern IntPtr createWaterTank(); //Initialise water tank object in DLL and returns pointer said object
    [DllImport("water_tank_win64")] static extern void setWaterTankInput(IntPtr waterTankPtr,double flow_in,double flow_out,double startVol); //Set Inputs of Water Tank Simulation
    [DllImport("water_tank_win64")] static extern double getWaterTankVolume(IntPtr waterTankPtr); //Retrieves Current Volume from DLL Simulation
    [DllImport("water_tank_win64")] static extern void waterTankStep(IntPtr waterTankPtr); //Steps Simulation forward by 1/60 s
    [DllImport("water_tank_win64")] static extern void waterTankDestructor(IntPtr waterTankPtr); //Destroys Water Tank Object
    //[DllImport("water_tank_win64")] static extern int test();

    IntPtr waterTankPtr = createWaterTank();

    //===================================================================
    //===== Water Tank Drawing Information ==============================
    [Export]public float x_coor =0, y_coor =0, wall_thickness =4, wall_height=100, wall_width=50;
    [Export]public float fill_percent;
    //===================================================================

    //===== Water Tank Characteristics ==================================
    [Export]public double crossArea, maxVol, currentVol, startVol;// m^2 and L
    [Export]public double TotalInputFlow,TotalOutputFlow;// L/s
    public Dictionary<String,double> outflows = new Dictionary<String,double>(),inflows = new Dictionary<String,double>(); 
    public bool started = false, paused = false;
    //===================================================================
    public override void _Draw(){
       draw_tank(x_coor,y_coor,wall_thickness,wall_height,wall_width,(float)(currentVol/maxVol));
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
        if(fill_percent > 1){
            DrawRect(new Rect2(x+thickness,(y+height-thickness)-(height-thickness),width,(height-thickness)),new Color("FF0000"));
        }
        else {
            DrawRect(new Rect2(x+thickness,(y+height-thickness)-(height-thickness)*fill_percent,width,(height-thickness)*fill_percent),water_color);
        }
    }

    public override void _Ready(){
        AddToGroup("WaterTanks");

        started = false;
        paused = false;
        currentVol = startVol;
        maxVol = 10;
        crossArea = 1.0;
        setWaterTankInput(waterTankPtr,0,0,startVol);
        updateTankInfo();
        ((RichTextLabel)GetNode("TankName")).SetPosition(new Vector2(x_coor,y_coor));
        ((RichTextLabel)GetNode("TankName")).BbcodeText = this.Name;
        ((RichTextLabel)GetNode("TankName")).Visible = true;
        ((RichTextLabel) GetNode("TankInfo")).Visible=false;
        ((Button) GetNode("Edit")).Visible=false; 
        ((Button) GetNode("Hide Info")).Visible=false;
        
    }
    

    //Using Physics Process to ensure call every 1/60 sec
    public override void _PhysicsProcess(float delta){
        UpdateTotalFlows();
            if(started&&!paused){
                waterTankStep(waterTankPtr);
                currentVol = (double) getWaterTankVolume(waterTankPtr);
                //GD.Print(currentVol);
                if(currentVol<0){
                    currentVol = 0;
                }
            }
            
            setWaterTankInput(waterTankPtr,TotalInputFlow,TotalOutputFlow,currentVol);

            Update();

        //GD.Print(this.Name +" "+_getCurrentVol().ToString());
    }

    
    public void updateTankInfo(){

        ((RichTextLabel)GetNode("TankInfo")).BbcodeText = 
        "Current Volume: " + currentVol.ToString("N3") + " m^3\n" +
        "Water Height: "+ (currentVol/crossArea).ToString("N3")+ " m \n" + 
        "Cross Sectional Area: " + (crossArea).ToString("N3")+ "m^2";
    }

    //Signal Emitters and Listeners

    public void _on_Hide_Info_pressed(){
        ((RichTextLabel)GetNode("TankName")).Visible = true;
        ((RichTextLabel) GetNode("TankInfo")).Visible=false;
        ((Button) GetNode("Edit")).Visible=false; 
        ((Button) GetNode("Hide Info")).Visible=false;
    }


    [Signal] public delegate void _on_tank_EditInfo(Tank watertank);
    public void _on_Edit_pressed(){
        GetTree().CallGroup("WaterTanks","PauseButton");
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
            ((RichTextLabel)GetNode("TankName")).Visible = false;
            ((RichTextLabel)GetNode("TankName")).SetPosition(new Vector2(x_coor,y_coor));
            ((RichTextLabel) GetNode("TankInfo")).Visible=true;
            ((Button) GetNode("Edit")).Visible=true; 
            ((Button) GetNode("Hide Info")).Visible=true;
        }
        
    }

    public void _on_EditValue_send_and_set_new_values(){
        waterTankDestructor(waterTankPtr);
        waterTankPtr = createWaterTank();
        setWaterTankInput(waterTankPtr,TotalInputFlow,TotalOutputFlow,currentVol);
    }
    public void StartStop(){
        if(!started){
            started = true;
            paused = false;
        }
        else{
            //GetTree().ReloadCurrentScene();
        }
    }

    public void PauseButton(){
        
        if(!paused){
            paused=true;
        }
        else{
            paused=false;
        }
    }

    public void UpdateTotalFlows(){
        TotalOutputFlow = 0;
        TotalInputFlow = 0;
        
        foreach(KeyValuePair<String,double> item in outflows){
            TotalOutputFlow += item.Value;
            
        }
       

        foreach(KeyValuePair<String,double> item in inflows){
            TotalInputFlow += item.Value;
           // GD.Print(TotalInputFlow);
        }
         //GD.Print();

    }
    
}

