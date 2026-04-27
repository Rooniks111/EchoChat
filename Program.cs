using proj_Echo.Components;
using proj_Echo.Services; // Додано для доступу до сервісів
using proj_Echo.Data;     // Додано для доступу до обробників даних

var builder = WebApplication.CreateBuilder(args);

// 1. Реєстрація сервісів у контейнері залежностей (DI)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Реєструємо ваші власні сервіси
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<EchoService>();
builder.Services.AddScoped<MessageProcessor>();

var app = builder.Build();

// 2. Налаштування конвеєра HTTP-запитів (Middleware)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // Значення HSTS за замовчуванням становить 30 днів.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();