extends Node2D

var fill_level = 1
var tank_list=[]
export var running=false
func _draw():
	for i in tank_list:
		draw_tank(i)
	pass


func draw_tank(tank):
	var x = tank["x"]
	var y = tank["y"]
	var thickness = tank["thickness"]
	var height = tank["height"]
	var width = tank["width"]
	var fill_percent = tank["fill_percent"]
	
	var wall_color = Color("FFFFFF")
	var water_color = Color("0077FF")
	draw_rect(Rect2(x,y,thickness,height),wall_color) #left tank wall
	draw_rect(Rect2(x+width,y,thickness,height),wall_color)   #right tank wall
	draw_rect(Rect2(x+thickness,y+height-thickness,width,thickness),wall_color) #bottom tank wall
	
	draw_rect(Rect2(x+thickness,(y+height-thickness)-(height-thickness)*fill_percent,width-thickness,(height-thickness)*fill_percent),water_color)
	pass

func empty_or_fill_tank(flow_rate,tank):
	if(tank["fill_percent"]>=0&&tank["fill_percent"]<=1):
		tank["fill_percent"]+=flow_rate
	pass

# Called when the node enters the scene tree for the first time.

func _ready():
	var tank = {
	"x":50,
	"y":50,
	"width":100,
	"height":50,
	"thickness":4,
	"fill_percent":90
	}
	tank_list.append(tank)
	pass

func _process(delta):
	if(running):
		empty_or_fill_tank(-0.1*delta,tank_list[0])
	update()


func _on_Start_Stop_pressed():
	if(!running):
		
		running = true
	else:
		
		running=false
		
		
	pass # Replace with function body.
