using GroceryPalWebApi.Code;
using GroceryPalWebApi.Model;

namespace GroceryPalWebApi.Services
{
    public static class UtilService
    {
        public static Store InitStoreLayoutAndDistanceMatrix(Store store)
        {
            store.V = 9;
            store.StoreLayout = new int[,] { { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
                                              { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
                                              { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
                                              { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
                                              { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
                                              { 0, 0, 4, 14, 10, 0, 2, 0, 0 },
                                              { 0, 0, 0, 0, 0, 2, 0, 1, 6 },
                                              { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
                                              { 0, 0, 2, 0, 0, 0, 6, 7, 0 } };

            // Building DistanceMatrix that is used for TSP
            store.DistanceMatrix = new int[9, 9];
            for (var i = 0; i < store.V; i++)
            {
                var distances = DijkstraAlgorithm.Run(store.V, store.StoreLayout, i);

                for (var j = 0; j < store.V; j++)
                    store.DistanceMatrix[i, j] = distances[j];
            };

            return store;
        }
    }
}
