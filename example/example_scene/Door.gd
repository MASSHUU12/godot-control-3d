extends StaticBody3D

@onready var animation_player: AnimationPlayer = $AnimationPlayer

const MOVE_DISTANCE := 1.5


func _on_display_interface_pin_accepted() -> void:
	animation_player.play("open")
