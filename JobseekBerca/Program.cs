
using JobseekBerca.Context;
using JobseekBerca.Repositories;
using JobseekBerca.Repositories.Details_Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("JobseekBerca")));
builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<RolesRepository>();
builder.Services.AddScoped<JobsRepository>();
builder.Services.AddScoped<ProfilesRepository>();
builder.Services.AddScoped<CertificatesRepository>();
builder.Services.AddScoped<EducationsRepository>();
builder.Services.AddScoped<ExperiencesRepository>();
builder.Services.AddScoped<SkillsRepository>();
builder.Services.AddScoped<ApplicationsRepository>();
builder.Services.AddScoped<SocialMediaRepository>();
builder.Services.AddScoped<SavedJobsRepository>();



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
