Description and explanation of the code and entire project.

Scripts: 

  - EventManager: This script subscribes to all events on the scene and start any reactions from any script when a specific event is called. It works as a terminal for all events, making it so other scripts don´t need references from others, but they speak to each  other through the EventManager.
  - GameManager:
  - FoodGrabbed: This script was created so that the FoodInteractable Prefab can be grabbed and all it's components are properly deactivated, avoiding any issues with the physical world of the project and the inventory.
