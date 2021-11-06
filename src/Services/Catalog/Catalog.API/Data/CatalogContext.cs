using System;
using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

//"DatabaseSettings": {
//    "ConnectionString": "mongodb://localhost:27017",
//    "DatabaseName": "ProductDb",
//    "CollectionName": "Products"
namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration config)
        {
            var connStr = config.GetValue<string>("DatabaseSettings:ConnectionString");

            Console.WriteLine(connStr);
            var client = new MongoClient(config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var db= client.GetDatabase(config.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = db.GetCollection<Product>(config.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products  {get;}
    }
}
