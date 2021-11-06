using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
//using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MyClasses
{
    public record Todo(int Id, string Title, bool Completed);

    public abstract class Record
    {
        public abstract void Notify(string messagge);
    }

    public class MyRecord : Record
    {
        public override void Notify(string messagge)
        {
            Console.WriteLine(messagge);
        }
        public void ProxySetup()
        {

        }
    }
    public class TaskService
    {
        public TaskService()
        {
        }
        public async Task<Todo> GetTodo(int id)
        {
            var url = "https://jsonplaceholder.typicode.com/todos/" + id;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                var result = await response.Content.ReadAsStringAsync();

                //return JsonConvert.DeserializeObject<Todo>(result) ?? new Todo(0, "", false);

                return JsonSerializer.Deserialize<Todo>(result) ?? new Todo(0, "", false);
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

            var allTodoTasks = Task.WhenAll(bag);
            var data = await Task.WhenAny(allTodoTasks);
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
            await foreach (var todo in stream)
            {
                collection.Add(todo);
            }
            return collection;

        }

        public async Task<int> DoSomth()
        {
            var todo = Task.Run(() => GetTodo(1));

            var todo2 = todo.ContinueWith(async (res) =>
            {
                var res1 = res.Result;
                var todo2 = await GetTodo(todo.Id + 1);
                return todo.Id + todo2.Id;
            });

            return await todo2.Result;
        }

        public bool IsDoneTodo(object obj) => obj is Todo && obj is Todo(_, _, true);

        public bool IsNotDoneTodo(object obj) => obj is Todo && obj is Todo { Completed: false };

        public bool TodoHas(object obj) => obj switch
        {
            Todo t when t.Completed == true && t.Title == "Learn C#" => true,
            _ => false
        };

        public T CreateRecord<T>() where T : Record, new()
        {
            var record = new T();
            record.Notify($"{nameof(T)} record created");
            return record;
        }

        public void DoSmthWithRecors()
        {
            CreateRecord<MyRecord>().ProxySetup();
        }
        private List<Record> records = new List<Record> { };
        public void SendMessage<T>(string message) where T : Record
        {
            records.OfType<T>().ToList().ForEach(re => re.Notify(message));
        }

        public void TestMessges()
        {
            var messenger = new MessaggeMediatR();
            var jasim = messenger.CreateSender<Developer>("jasim");
            var arif = messenger.CreateSender<Developer>("arif");
            var mamun = messenger.CreateSender<Tester>("mamun");

            jasim.SendMessage<Developer>("Hi there");
            arif.SendMessage<Tester>("How about the sprint?");
            mamun.SendMessage<Developer>("Well Done!");
        }
    }

    public  class MessaggeMediatR
    {
        private List<Sender> senders = new List<Sender>();

        public void SendMessag<T>(string message) where T : Sender
        {
            senders.OfType<T>().ToList().ForEach(s => s.GetMessage(message, s));

        }
        public T CreateSender<T>(string name) where T : Sender, new()
        {
            var sender = new T();
            sender.Name = name;
            senders.Add(sender);
            return sender;
        }
    }

    public abstract class Sender
    {
        public abstract void SendMessage<T>(string message) where T : Sender;
        public MessaggeMediatR? MediatR { get; set; }
        public string? Name { get; set; }
        public abstract void GetMessage(string message, Sender sender);
    }

    public class Developer : Sender
    {
        
        public override void GetMessage(string message, Sender sender)
        {
            Console.WriteLine($"{Name} (Developer) received - {message} from {sender.Name} is a ({nameof(sender)})");
        }

        public override void SendMessage<T>(string message)
        {
            MediatR!.SendMessag<T>(message);    
        }
    }

    public class Tester : Sender
    {
        
        public override void GetMessage(string message, Sender sender)
        {
            Console.WriteLine($"{Name} (Tester) received - {message} from {sender.Name} is a ({nameof(sender)})");
        }

        public override void SendMessage<T>(string message)
        {
            MediatR!.SendMessag<T>(message);
        }
    }

}
