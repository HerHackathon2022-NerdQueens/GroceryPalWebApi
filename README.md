# GroceryPal Web API

- Rest API for working with Products, ShoppingList and Recipes.
- Additional functionality is finding the best route in the store based on the shopping list.
  - The store layout is presented as a graph, with the distances between the shelves and the freezers as the weights of the branches. A distance matrix was created based on this graph using the Dijkstra algorithm. The best route through the store is determined using the Traveling Salesman Algorithm, which uses the distance matrix.

## Usage:
Docker pull command for Web API
```
docker pull tinadj/grocerypalwebapi
```
Start the container with the following script:
```
docker run -p 8081:80 tinadj/grocerypalwebapi
```
When the container is started, the REST API documentation can be accessed with the following URL:
```
http://localhost:8081/swagger/index.html
```


![alt text](https://github.com/HerHackathon2022-NerdQueens/GroceryPalWebApi/blob/main/1.png?raw=true)
![alt text](https://github.com/HerHackathon2022-NerdQueens/GroceryPalWebApi/blob/main/2.png?raw=true)
