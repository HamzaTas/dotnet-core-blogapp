# BlogApp Uygulaması

Aşağıdaki teknolojiler ve tasarım desenleri kullanılarak hazırlanan makale uygulamaları için Rest Api Projesi

  - .NET Core Framework 
  - ASP.NET Web API
  - Entity Framework Core
  - MSSQL
  - Basic Authentication
  - SwaggerUI
  - Logging
  - In-Memory Cache
  - Dependency Injection
  - Unit Of Work
  - N-Tier architecture

# Projenin Ayağa Kaldırılması

  - Github'tan Repository çektikten sonra eğer .net Core kurulumları bilgisayarınızda mevcut ise F5 ile projenin çalıştırılması yeterlidir. Proje çalıştırıldığında çağrılan Fakedata sınıfı sizin için mevcuttaki migrationı çalıştırıp veritabanının oluşmasını ve test verilerininin doldurulmasını sağlayacaktır. 
  - Veri tabanının oluşması için projedeki appsettings.json dosyasında "DefaultConnection" Connection Strings'nine veri tabanının oluşturulacağı sunucu bilgisi verilmelidir.
  - Ek olarak App_Files Klasörünün için veritabanında script dosyası mevcuttur. (dbScript.sql)

# API Methodlarına Erişim

  - http://localhost:<port>/swagger/index.html şeklinde uygulamada bulunan Swagger aracığıyla arayuz üzerinden API Methodlarına istek yapılabilir.
  - App_File dizininde bulunan BlogApp.postman_collection_v1.json ve BlogApp.postman_collection_v2.json dosyalarını Postmen versiyonunuza göre import ederek hazır şablonlar üzerinden istekler yapılabilir.

# Proje geliştirilmesinde kullanılan kaynaklar

  - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1
  - https://www.blexin.com/en-US/Article/Blog/In-memory-caching-in-ASPNET-Core-45
  - https://jasonwatmore.com/post/2019/10/21/aspnet-core-3-basic-authentication-tutorial-with-example-api
  - https://sandervandevelde.wordpress.com/2018/01/06/adding-basic-authentication-to-asp-net-core-the-right-way/
  - https://docs.microsoft.com/en-us/aspnet/core/performance/caching/memory?view=aspnetcore-3.1
  - https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-3.1
  - https://code-maze.com/swagger-ui-asp-net-core-web-api/
  - https://nblumhardt.com/2016/10/aspnet-core-file-logger/
