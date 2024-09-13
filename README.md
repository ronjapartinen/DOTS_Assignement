# DOTS_Assignement

Im using ECS in this game for better performance.
I started by following the lectures. First I made a subscene that I can work with entities.

#Player
Following the recordings I started making the authoring, input system and move system for the player.
We hold entities data in the structs because of their cache effiency.

#Bullet
Next I made authoring, fire system and move system for the projectile.
We use entity command buffer to safely create the entity using playback.

#Input reset
After that I made ResetInputSystem to unenable the fireprojectiletag that the player has.
It is put to update last in in simulationsystem group and after EndSimulationEntityCommandBufferSystem,
to make sure all the other systems that might rely on the tag are run and that the buffers have been processed before this, so it unenables after shooting.

#Enemy spawner
Then I made spawner component, authoring and system for the spawner.
I changed the SpawnerSystem so that it randomly generates a point from a circle where the entities spawn to.

#Enemy movement
Lastly I added EnemyMovement component that holds the data and a EnemyMoveSystem.
I simply foreach loop to get the component with its data to transform the position of the enemy to direction which is the middle of the circle.