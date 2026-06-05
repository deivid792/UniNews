using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Uninews.API.Controllers;
using Uninews.Application.UseCases.Commands.Login;
using Uninews.Application.UseCases.Courses.Commands.CreateCourse;
using Uninews.Application.UseCases.Courses.Commands.DeleteCourse;
using Uninews.Application.UseCases.Courses.Commands.UpdateCourse;
using Uninews.Application.UseCases.Courses.Queries.GetAllCourses;
using Uninews.Application.UseCases.Courses.Queries.GetCourseById;
using Uninews.Application.UseCases.News.Commands.DeleteNews;
using Uninews.Application.UseCases.News.Commands.UpdateNews;
using Uninews.Application.UseCases.News.Queries.GetAllNews;
using Uninews.Application.UseCases.Ocurrences.Commands.CreateOcurrences;
using Uninews.Application.UseCases.Ocurrences.Commands.DeleteOcurrence;
using Uninews.Application.UseCases.Ocurrences.Commands.UpdateOcurrence;
using Uninews.Application.UseCases.Ocurrences.Queries.GetAllOcurrences;
using Uninews.Application.UseCases.Ocurrences.Queries.GetOcurrenceById;
using Uninews.Application.UseCases.Tags.Commands.CreateTags;
using Uninews.Application.UseCases.Tags.Commands.UpdateTag;
using Uninews.Application.UseCases.Tags.Queries.GetAllTags;
using Uninews.Application.UseCases.Tags.Queries.GetTagById;
using Uninews.Application.UseCases.UnitNews.Commands.CreateNews;
using Uninews.Application.UseCases.UnitNews.Queries.GetNewsById;
using Uninews.Application.UseCases.Users.Commands.CreateUser;
using Uninews.Application.UseCases.Users.Commands.DeleteUser;
using Uninews.Application.UseCases.Users.Commands.UpdatePreferences;
using Uninews.Application.UseCases.Users.Commands.UpdateUser;
using Uninews.Application.UseCases.Users.Queries.GetAllUsers;
using Uninews.Application.UseCases.Users.Queries.GetUserById;
using Uninews.Domain.Interfaces;
using Uninews.Domain.Services;
using Uninews.Infrastructure.Context;
using Uninews.Infrastructure.Repositories;
using Uninews.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IGetAllTagsHandler, GetAllTagsHandler>();
builder.Services.AddScoped<IcreateUserHandler, CreateUserHandler>();
builder.Services.AddScoped<ILoginHandler, LoginHandler>();
builder.Services.AddScoped<IUpdatePreferencesHandler, UpdatePreferencesHandler>();
builder.Services.AddScoped<ICreateNewsHandler, CreateNewsHandler>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IOcurrenceRepository, OcurrenceRepository>();
builder.Services.AddScoped<ICreateTagsHandler, CreateTagsHandler>();
builder.Services.AddScoped<ICreateCourseHandler, CreateCourseHandler>();
builder.Services.AddScoped<ICreateCourseHandler, CreateCourseHandler>();
builder.Services.AddScoped<IGetAllCoursesHandler, GetAllCoursesHandler>();
builder.Services.AddScoped<IGetCourseByIdHandler, GetCourseByIdHandler>();
builder.Services.AddScoped<IUpdateCourseHandler, UpdateCourseHandler>();
builder.Services.AddScoped<IDeleteCourseHandler, DeleteCourseHandler>();
builder.Services.AddScoped<IUpdateOcurrenceHandler, UpdateOcurrenceHandler>();
builder.Services.AddScoped<IDeleteOcurrenceHandler, DeleteOcurrenceHandler>();
builder.Services.AddScoped<IGetAllOcurrencesHandler, GetAllOcurrencesHandler>();
builder.Services.AddScoped<IGetOcurrenceByIdHandler, GetOcurrenceByIdHandler>();
builder.Services.AddScoped<IUpdateTagHandler, UpdateTagHandler>();
builder.Services.AddScoped<IDeleteTagHandler, DeleteTagHandler>();
builder.Services.AddScoped<IGetTagByIdHandler, GetTagByIdHandler>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IDeleteNewsHandler, DeleteNewsHandler>();
builder.Services.AddScoped<IGetAllNewsHandler, GetAllNewsHandler>();
builder.Services.AddScoped<IGetNewsByIdHandler, GetNewsByIdHandler>();
builder.Services.AddScoped<IUpdateUserHandler, UpdateUserHandler>();
builder.Services.AddScoped<IDeleteUserHandler, DeleteUserHandler>();
builder.Services.AddScoped<IGetAllUsersHandler, GetAllUsersHandler>();
builder.Services.AddScoped<IGetUserByIdHandler, GetUserByIdHandler>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IUpdateNewsHandler, UpdateNewsHandler>();
builder.Services.AddScoped<ICreateOcurrenceHandler, CreateOcurrenceHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("FrontendPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();