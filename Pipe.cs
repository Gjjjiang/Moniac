using Godot;
using System;
using System.Collections.Generic; 

public class Pipe : Line2D{

    [Export]public double physicalMaxFlow, currentMaxFlow, currentFlow;
    public Tank tank_from, tank_to;
    public double pipe_inflow, pipe_outflow;
    public override void _Ready(){
        


    }
    public void ConnectPipe(Tank tank_from_in,Tank tank_to_in){
        tank_from = tank_from_in;
        tank_to = tank_to_in;
        SetPipePosition();
    }

    public void SetPipePosition(){
        if(tank_from != null && tank_to != null){
            this.SetPointPosition(0,new Vector2(tank_from.x_coor+tank_from.wall_width/2+tank_from.wall_thickness,tank_from.y_coor+tank_from.wall_height));
            this.SetPointPosition(1,new Vector2(tank_to.x_coor+tank_to.wall_width/2+tank_to.wall_thickness,tank_to.y_coor));
        }
        else if(tank_from != null){
            this.SetPointPosition(0,new Vector2(tank_from.x_coor+tank_from.wall_width/2+tank_from.wall_thickness,tank_from.y_coor+tank_from.wall_height));
            this.SetPointPosition(1,new Vector2(tank_from.x_coor+tank_from.wall_width/2+tank_from.wall_thickness,tank_from.y_coor+tank_from.wall_height));
        }
        else if(tank_to != null){
            this.SetPointPosition(0,new Vector2(tank_to.x_coor+tank_to.wall_width/2+tank_to.wall_thickness,tank_to.y_coor));
            this.SetPointPosition(1,new Vector2(tank_to.x_coor+tank_to.wall_width/2+tank_to.wall_thickness,tank_to.y_coor));
        }
    }

    
    public void outflow(){
        if(!tank_from.outflows.ContainsKey(this.Name)){
            tank_from.outflows.Add(this.Name,pipe_outflow);
            
        }
        else{
            tank_from.outflows[this.Name] = pipe_outflow;
            
        }
    }
    public void inflow(){
        if(!tank_to.inflows.ContainsKey(this.Name)){
            tank_to.inflows.Add(this.Name,pipe_inflow);
        }
        else{
            tank_to.inflows[this.Name] = pipe_inflow;
        }
    }
    
    public void FlowUpdate(){
        pipe_inflow = currentFlow;
        pipe_outflow = currentFlow;
        outflow();
        inflow();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta){
      FlowUpdate();
    }

}
