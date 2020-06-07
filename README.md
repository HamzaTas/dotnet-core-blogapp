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
  
# Proje Hakkında

**Projede Kullanılan tasarım desenleri hangileridir? Bu desenler neden kullanıldı?**

 Proje içerisinde Dependency Injection ve Unit of work tasarım desenleri kullanılmıştır. 
 
 **Dependenciy Injection** kullanım sebebi bağımlı olduğumuz sınıfları kendi sınıflarımız ile özdeşleştirerek bunların kullanılmasını sağlamak amacıyla kullanılmıştır. Dependency Injection kullanımındaki temel amaç bir projede yazılmış bir kodu değiştirmemek içindir. Çünkü mevcut kod belli unit testlerden geçmiş sorunsuz çalışan bir kod olabilir. Bu uygulama için örnek vermek gerekirse BlogController içerisindeki Get() methodu cache üzerindeki veya veri tabanındaki mevcut blog kayıtlarını döndermektedir. Bu tasarım deseninin kullanılması ve kullanılmaması durumunda neler olduğunu gözlemleyebilmek adına bu projede bilerek cache yapısı için Dependency Injection tasarım desenini kullanmamıştır. Proje mevcut yapısında Entity Framework üzerinden veritabanı işlemleri yapılmaktadır. Diyelimki bir anda projede kullanılan veri tabanı sistemi mongo DB yapısına geçirme kararı alındı. Bu durumda uygulama da bulunun tüm business işlemlerinde değişikliğe gitmek yerine bunu merkezi yönetmemiz de fayda olacaktır. Örnek olarak BlogController daki Get() methodu testleri yapılmış doğru çalışan bir kod parçacığıdır. Burada değişikliğe uğrayacak kısımda düzenlemleri yapmak hem maliyeti hemde uygulamada oluşacak riskleri azaltmaktadır. Burada repository patterni devreye girmektedir. Burada Business katmanıyla projedeki Repository 'ler iletişim kurarken arka tarafta Dependency Injection ile efRepositor'ler bağdaşlaştırılmıştır. Kullanılan veri erişim teknolojisi değiştiğinde repository ile bağdaşlaştırılan sınıfın değiştirilmesi yeterli olacak ve hiç bir controller da yazılmış olan kodlarda düzenleme yapmak durumunda kalınmayacaktır. Dependency Injection yapısını kullanmayan cache yapısında ise görüldüğü gibi controller seviyesinde controller IMemoryCache sınıfına bağımlı bırakılmış ilerde farklı bir cache teknolojisine geçildiği zaman buradaki kodlarda da düzenlemeye gitmek durumunda bırakılmıştır. Aslında amaç cache deki veriyi almak olsa bile yazılımcının ettiği kötülük nedeniyle cache teknolojisindeki değişiklik nedeniyle çalışan kodlara dokunulmak zorunda kalınacaktır. Veri tabanı teknolojisindeki değişiklik ve cache teknolojisindeki değişiklik senaryolarını gözümüzün önüne getirdiğimizde nasıl korkunç bir tablo olduğunu gözlemlemekteyiz.
 
  **Unit Of Work**  bir amaç doğrultusunda gerçekleştirelen işlemlerin toplu olarak tek bir kanaldan yapılmasıdır. Bu tasarım deseni repository tasarım deseni ile birlikte uygulanır. İşlemler tek bir kanaldan yapıldığı için uygulama performansı olumlu yönde etkilenmektedir. Hazırlanan repositor sınıfları bir bir sınıfa bağlanarak bu sınıf üzerinden repository'lerin yaptığı işlemlerin yapılması sağlanmaktadır. Uygulama içerisinde gereksiz nesne türetmenin önüne geçmektedir. Örnek vermek gerekirse bir yerde hem CategoryRepository hemde BlogRepository 'sine ihtiyacaım oldu. Burada her ikisinden ayrı ayrı nesne türetmek yerine bağlı oldukları UnitUfWork sınıfından tek bir nesne üretilerek istenilen aynı işlemler yapılabilir. Faydası saymakla bitmeyen bir tasarım desenidir.

**Kullanılan teknolojiler ve kütüphaneler hakkında**

Bu proje içerisindeki bir çok teknoloji ile daha profesyonel olarak bir deneyimim olmadı. Düzce Üniversitesi Bilgi İşlem Daire Başkanlığında Part Time Yazılım Geliştirici olarak çalıştığım dönemde .NET MVC üzerinde çeşitli projelerde görev aldım. .NET Core için .NET MVC temel mantığından farklı olmadığı düşüncesiyle yola çıktığım bu projede çoğu yerde farklılıklar olduğu gibi benzerliklerde olduğunu gözlemledim. Sonuç olarak deneyimin araştırmadan ve uygulayarak öğrenmeden geldiğini kavradığım için daha önce geliştirdiğim projelerden çok farklı bir yol izlemedim.  .NET Core için bir deneyimim omadığı için takıldığım kısımlarda "Proje geliştirilmesinde kullanılan kaynaklar" başlığındaki makele ve dokumantasyonlardan faydalandım. Bu makaleleri faydalı bulduğum için paylaşmak istedim. 

**Daha Neler yapılabilirdi?**

07.06.2020
*   Bu proje için yapılabilecek en güzel şey Dependenciy Injection kısmında anlatıldığı gibi Cache yapısını bağımlılıktan doğan faciadan kurtarmak olacaktır. Cache yapısını bu yapıdan kurtardıktan sonra farklı bir cache mimarisi ile cache yapısını yeniden oluşturmak olacaktır. 
*   Projedeki Routing yapısı özelleştirilebilir.
*   API güvenliğinde kullanılan Basic Authentication yerine Token yapısıyla bir kurgu yapılabilirdi.
*   Loglama da yapısı fiziksel dosyaya yazmak yerine bir NoSQL veri tabanına yapılabilir hata loglama yapısı da Dependenciy Injection tasarım deseni ile bağımlılıktan kurtarılabilirdi.
*   Proje üzerinde bir makele sisteminin en temel üç bileşeni mevcut. (Category, Blog ve Yorumlar) Bir makale sisteminde bulunan tag yapısı, arşiv yapısı gibi yapılarda eklenerek veya mevcut endpoint'lerdeki methodlar ve Category, Blog ve Yorum kısımları  zenginleştirilebilirdi.
*   Repository katmanını önüne bir servis katmanı kurularak controller sınıflarının bağımlılığı en aza indirgenebilir oluşan business operasyonları farklı projeler için kullanılabilirdi. (Örnek Web Uygulaması)
*   ReactJS gibi bir UI kütüphanesi ile oluşturulan API için bir web uygulaması eklenebilirdi. 


# Proje geliştirilmesinde kullanılan kaynaklar

  - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1
  - http://www.canertosuner.com/post/asp-net-core-in-memory-cache
  - https://www.blexin.com/en-US/Article/Blog/In-memory-caching-in-ASPNET-Core-45
  - https://jasonwatmore.com/post/2019/10/21/aspnet-core-3-basic-authentication-tutorial-with-example-api
  - https://sandervandevelde.wordpress.com/2018/01/06/adding-basic-authentication-to-asp-net-core-the-right-way/
  - https://docs.microsoft.com/en-us/aspnet/core/performance/caching/memory?view=aspnetcore-3.1
  - https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-3.1
  - https://code-maze.com/swagger-ui-asp-net-core-web-api/
