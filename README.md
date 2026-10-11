Description and explanation of the code and entire project.

Scripts:

* EventManager: This script subscribes to all events on the scene and start any reactions from any script when a specific event is called. It works as a terminal for all events, making it so other scripts don´t need references from others, but they speak to each  other through the EventManager.
* GameManager:
* FoodGrabbed: This script was created so that the FoodInteractable Prefab can be grabbed and all it's components are properly deactivated, avoiding any issues with the physical world of the project and the inventory.
* Inventory: It's… An inventory, it stores the food you collect.
* FoodElement: A simple script, since the inventory is pretty simple and minimal. It sets a name based on the objects tag
* Pescado: This script has a function that allows the change of the 2 textures from the custom shader (BlendingTextures\_VFX\_ShGrph) to the preferred ones. Another function allows the blending of the 2 textures when the fish (pescado) collides with other objects (cuchillo, descamador y sarten). The function keeps in mind the step of the process through an enum.

