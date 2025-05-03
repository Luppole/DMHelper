using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Configuration;
using DMHelper.App.Models;

namespace DMHelper.App.Services
{
    public class FirebaseService
    {
        private readonly FirebaseClient _firebaseClient;
        private readonly string _basePath;

        public FirebaseService(IConfiguration configuration)
        {
            try
            {
                var firebaseConfig = configuration.GetSection("Firebase");
                string databaseUrl = firebaseConfig["DatabaseUrl"] ?? "https://dmhelper-b4e40.firebaseio.com/";
                _basePath = firebaseConfig["BasePath"] ?? "dmhelper";

                _firebaseClient = new FirebaseClient(databaseUrl);
                
                // Ensure database is initialized
                InitializeDatabaseAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Firebase initialization error: {ex}");
                throw;
            }
        }

        private async Task InitializeDatabaseAsync()
        {
            // Check if campaigns collection exists, if not, create an empty one
            try
            {
                await _firebaseClient.Child(_basePath).Child("campaigns").OnceSingleAsync<object>();
            }
            catch (Firebase.Database.FirebaseException)
            {
                // Collection doesn't exist, create it with a placeholder
                await _firebaseClient.Child(_basePath).Child("campaigns").PutAsync(new {initialized = true});
            }
        }

        public async Task<List<T>> GetCollectionAsync<T>(string collectionPath) where T : class
        {
            try
            {
                var collection = await _firebaseClient
                    .Child(_basePath)
                    .Child(collectionPath)
                    .OnceAsync<T>();

                var result = new List<T>();
                foreach (var item in collection)
                {
                    if (item.Object != null)
                    {
                        // If your T has an Id property and it's a string, try to set it
                        var idProperty = typeof(T).GetProperty("Id");
                        if (idProperty != null && idProperty.PropertyType == typeof(string))
                        {
                            idProperty.SetValue(item.Object, item.Key);
                        }
                        result.Add(item.Object);
                    }
                }
                return result;
            }
            catch (Firebase.Database.FirebaseException ex) when (ex.Message.Contains("404"))
            {
                // Collection doesn't exist yet
                System.Diagnostics.Debug.WriteLine($"Collection {collectionPath} doesn't exist yet");
                return new List<T>();
            }
        }

        public async Task<string> AddItemAsync<T>(string collectionPath, T item)
        {
            try
            {
                var result = await _firebaseClient
                    .Child(_basePath)
                    .Child(collectionPath)
                    .PostAsync(item);

                return result.Key;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding item: {ex}");
                throw;
            }
        }

        public async Task UpdateItemAsync<T>(string collectionPath, string key, T item)
        {
            await _firebaseClient
                .Child(_basePath)
                .Child(collectionPath)
                .Child(key)
                .PutAsync(item);
        }
    }
}