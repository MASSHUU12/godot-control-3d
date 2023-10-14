extends Control

signal pin_accepted()

@onready var grid_container: GridContainer = $CenterContainer/PanelContainer/MarginContainer/VBoxContainer/CenterContainer/GridContainer
@onready var line_edit: LineEdit = $CenterContainer/PanelContainer/MarginContainer/VBoxContainer/LineEdit

const PIN := "1234"


func _ready() -> void:
	for child in grid_container.get_children():
		child.connect("pressed", func(): handle_keypad(child))


func handle_keypad(btn: Button) -> void:
	var text: String = btn.text

	if text == "*" and PIN == line_edit.text:
		pin_accepted.emit()

	if text == "C" or text == "*":
		line_edit.text = ""
		return

	line_edit.text += text
