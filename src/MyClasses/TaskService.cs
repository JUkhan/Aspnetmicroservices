using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;


namespace MyClasses
{
    public record Todo(int Id, string Title, bool Completed);
 
    public class TaskService
    {
        public TaskService()
        {
        }
        public async Task<Todo> GetTodo(int id)
        {
            var url = "https://jsonplaceholder.typicode.com/todos/" + id;
            using(var client = new HttpClient())
            {
               var response = await client.GetAsync(url);

               var result = await response.Content.ReadAsStringAsync();

               return JsonConvert.DeserializeObject<Todo>(result);
            }
        }

        public async Task<IEnumerable<Todo>> GeTodos(params int[] ids)
        {
            var bag = new ConcurrentBag<Task<Todo>>();
            ids.Aggregate(bag, (bag, id) =>
            {
                bag.Add(GetTodo(id));
                return bag;
            });

            var allTodoTasks=Task.WhenAll(bag);
            var data= await Task.WhenAny(allTodoTasks);
            return data.Result;
        }

        public async IAsyncEnumerable<Todo> TodoStrream()
        {
            yield return await GetTodo(1);
            yield return await GetTodo(2);
            yield return await GetTodo(3);
            yield return await GetTodo(4);

        }

        public async Task<ObservableCollection<Todo>> GetTodos()
        {
            var collection = new ObservableCollection<Todo>();
            //Here you can update ui with this collection.
            var stream = TodoStrream(); //.WithCancellation(CancellationToken.None);
            await foreach(var todo in stream)
            {
                collection.Add(todo);
            }
            return collection;

        }

        public async Task<int> DoSomth()
        {
            var todo = Task.Run(() => GetTodo(1));

             var todo2= todo.ContinueWith(async (res) =>{
                var res1 = res.Result;
                var todo2 = await GetTodo(todo.Id + 1);
                return todo.Id + todo2.Id;
            });

            return await todo2.Result;
        }

    }
}
