// 1. Using para trabajar con EntityFramework
using Microsoft.EntityFrameworkCore;
using universityApiBackend.DataAccess;
using universityApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// 2. Conexi?n con SQL Server

const string CONNECTIONNAME = "UniversityDb";
var connetionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3. Add Context to Services of builder
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connetionString));

// 7. Add Service of JWT Autorization
// TODO:
// builder.Services.AddJwtTokenServices(builder.Configuration);

// - Add services to the container.
builder.Services.AddControllers();

// 4. Add custom Services (folder Services)
builder.Services.AddScoped<IStudentsServices, StudentsService>();
// TODO: Add rest of services

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//8. TODO: config swagger to take care of Autorization of JWT
builder.Services.AddSwaggerGen();

// 5. CORS Configuration
builder.Services.AddCors( options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
} );


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

// 6. Tell app to use CORS
app.UseCors("CorsPolicy");

app.Run();
