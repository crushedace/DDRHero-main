extends Area2D
@export_enum("cooldown","HitOnce","DisableHitBox") var HurtboxType = 0

@onready var collision = $CollisionShape2D
@onready var disableTimer = $Timer

signal  hurt(Damage)


func _on_area_entered(area):
	if area.is_in_group("Attack"):
		if not area.get("Damage") == null:
			match HurtboxType:
				0: #Cooldown
					collision.call_deferred("set","disabled",true)
					disableTimer.start()
					#hitOnce
					pass
					#DisableHitBox
					if area.has_method("tempdisable"):
						area.tempdisable()
			var damage = area.damage
			emit_signal("hurt",damage)

func _on_disable_timer_timeout():
	collision.call_deferred("set","disabled",false)
