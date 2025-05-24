using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ToDoList.Controllers;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Tests
{
    public class TodoItemsControllerTests
    {
        private DbContextOptions<ApplicationDbContext> CreateInMemoryDbOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async System.Threading.Tasks.Task Index_ReturnsViewWithTodoItems()
        {
            var dbOptions = CreateInMemoryDbOptions();

            using (var context = new ApplicationDbContext(dbOptions))
            {
                context.TodoItems.AddRange(
                    new TodoItem { Id = 1, Title = "Zadanie 1", Description = "Opis 1", CreatedAt = DateTime.Now },
                    new TodoItem { Id = 2, Title = "Zadanie 2", Description = "Opis 2", CreatedAt = DateTime.Now }
                );
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(dbOptions))
            {
                var controller = new TodoItemsController(context);

                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<TodoItem>>(viewResult.Model);
                Assert.Equal(2, model.Count());
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_WithValidModel_CreatesTodoItemAndRedirects()
        {
            var dbOptions = CreateInMemoryDbOptions();
            using (var context = new ApplicationDbContext(dbOptions))
            {
                var controller = new TodoItemsController(context);
                var newItem = new TodoItem { Title = "Nowe zadanie", Description = "Opis nowego zadania" };

                var result = await controller.Create(newItem);

                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);

                var item = await context.TodoItems.FirstOrDefaultAsync(t => t.Title == "Nowe zadanie");
                Assert.NotNull(item);
                Assert.Equal("Nowe zadanie", item.Title);
                Assert.Equal("Opis nowego zadania", item.Description);
                Assert.False(item.IsCompleted);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task Edit_WithValidModel_UpdatesTodoItemAndRedirects()
        {
            var dbOptions = CreateInMemoryDbOptions();

            using (var context = new ApplicationDbContext(dbOptions))
            {
                context.TodoItems.Add(new TodoItem
                {
                    Id = 1,
                    Title = "Pierwotny tytuł",
                    Description = "Pierwotny opis",
                    CreatedAt = DateTime.Now
                });
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(dbOptions))
            {
                var controller = new TodoItemsController(context);
                var updatedItem = new TodoItem
                {
                    Id = 1,
                    Title = "Zaktualizowany tytuł",
                    Description = "Zaktualizowany opis",
                    IsCompleted = true
                };

                var result = await controller.Edit(1, updatedItem);

                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);

                var item = await context.TodoItems.FindAsync(1);
                Assert.NotNull(item);
                Assert.Equal("Zaktualizowany tytuł", item.Title);
                Assert.Equal("Zaktualizowany opis", item.Description);
                Assert.True(item.IsCompleted);
                Assert.NotNull(item.CompletedAt);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_WithValidId_RemovesTodoItemAndRedirects()
        {
            var dbOptions = CreateInMemoryDbOptions();

            using (var context = new ApplicationDbContext(dbOptions))
            {
                context.TodoItems.Add(new TodoItem
                {
                    Id = 1,
                    Title = "Zadanie do usunięcia",
                    Description = "Opis do usunięcia",
                    CreatedAt = DateTime.Now
                });
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(dbOptions))
            {
                var controller = new TodoItemsController(context);

                var result = await controller.DeleteConfirmed(1);

                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);

                var item = await context.TodoItems.FindAsync(1);
                Assert.Null(item);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task ToggleStatus_ChangesCompletionStatusAndRedirects()
        {
            var dbOptions = CreateInMemoryDbOptions();

            using (var context = new ApplicationDbContext(dbOptions))
            {
                context.TodoItems.Add(new TodoItem
                {
                    Id = 1,
                    Title = "Zadanie do przełączenia",
                    Description = "Opis do przełączenia",
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                });
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(dbOptions))
            {
                var controller = new TodoItemsController(context);

                var result = await controller.ToggleStatus(1);

                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);

                var item = await context.TodoItems.FindAsync(1);
                Assert.NotNull(item);
                Assert.True(item.IsCompleted);
                Assert.NotNull(item.CompletedAt);
            }

            using (var context = new ApplicationDbContext(dbOptions))
            {
                var controller = new TodoItemsController(context);

                var result = await controller.ToggleStatus(1);

                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);

                var item = await context.TodoItems.FindAsync(1);
                Assert.NotNull(item);
                Assert.False(item.IsCompleted);
                Assert.Null(item.CompletedAt);
            }
        }
    }
}
