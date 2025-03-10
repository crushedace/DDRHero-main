extends CharacterBody2D

@export var movement_speed = 50.0
@export var Hp = 10
@onready var player = get_tree().get_first_node_in_group("player")

func _physics_process(_delta):
	var direction = global_position.direction_to(player.global_position)
	velocity=direction*movement_speed
	move_and_slide()
