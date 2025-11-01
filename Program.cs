using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Minimal_API_day_1;
using Minimal_API_day_1.Data;
using Minimal_API_day_1.Domain;
using Minimal_API_day_1.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

//DI register
builder.Services.AddScoped<ILearningService, Learnings>();
builder.Services.AddScoped<ILearningTaskRepository, LearningTaskRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Data Source=AMIT-DESKTOP\\SQLEXPRESS;Initial Catalog=Learning;Integrated Security=True;Trust Server Certificate=True"));

var app = builder.Build();

//map group
var todoItems = app.MapGroup("/learnings");

//home
app.MapGet("/", () => "Hello World!");

//TypedResults 
static Task<IResult> GetAllTodos(ILearningService learningService)
{
    return System.Threading.Tasks.Task.FromResult<IResult>(TypedResults.Ok(learningService.GetAll()));
}

//get all Learnings
todoItems.MapGet("/", async (ILearningService service) => await service.GetAll());

// get by learning by id
todoItems.MapGet("/{id:int}", async (int id, ILearningService service) =>
{
    var task = await service.GetById(id);
    return task is not null ? Results.Ok(task) : Results.NotFound();
});


todoItems.MapPost("/", async (TaskDto dto, ILearningService svc) =>
{
    if (!dto.IsValid(out var error))
        return Results.BadRequest(new { error });

    LearningTask created = await svc.Add(dto);

    return Results.Created(uri: $"/learnings/{created.TaskId}", created);
});

todoItems.MapPut("/{id}", async (int id, TaskDto dto, ILearningService svc) =>
{
    if (!dto.IsValid(out var error))
        return Results.BadRequest(new { error });

    return await svc.Update(id, dto)
        ? Results.Ok(new { message = "Course updated successfully" })
        : Results.NotFound(new { error = "Course not found" });
});

todoItems.MapDelete("/{id}",async (int id, ILearningService svc) =>
{
    return await svc.Delete(id)
        ? Results.Ok(new { message = "Course deleted successfully" })
        : Results.NotFound(new { error = "Course not found" });
});

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DocumentTitle = "Course API";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Course API v1");
});

app.UseMiddleware<ExceptionMiddleware>();


app.Run();
