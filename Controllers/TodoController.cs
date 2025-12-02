using ASPCoreWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreWebApplication.Controllers
{
    public class TodoController : Controller
    {
        private static List<TodoItem> _items = new List<TodoItem>();
        private static int _nextId = 1;

        [HttpGet]
        public IActionResult TodoIndex() => View(_items);

        [HttpPost]
        public IActionResult Add(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                _items.Add(new TodoItem 
                {
                    Id = ++_nextId, Title = title, isDone = false 
                });
            }
            return RedirectToAction(nameof(TodoIndex));
        }

        [HttpPost]
        public IActionResult Toggle(int id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item != null) item.isDone = !item.isDone;
            return RedirectToAction(nameof(TodoIndex));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item != null) _items.Remove(item);
            return RedirectToAction(nameof(TodoIndex));
        }

    }
}
