# DesignPatternsAssignment
Code by: Tove Backenroth

Making character movement and shooting using design patterns.

##Pattern 1: Singleton
####ObjectPool.cs
The object pool contains the player's bullets, which means there only 
should be one instance of the pool.

## Pattern 2: State Machine (with Pushdown Automata)
#### CharacterStateMachine.cs
I made the StateMachine generic, and made sure the playerinputs 
and movements were disconnected from the State Machine. 
This way, the state machine can be reused for NPC's movements.

## Pattern 3: Object Pool
#### ObjectPool.cs
The object pool fills up at start with a decided amount of bullet prefabs,
if the pool is empty, the script instantiates new bullets and then queues 
them into the pool after the bullets despawn.