## Projenin 1. Aþamasýnda Ýþlenen Konular ve Teknolojiler

Bu proje, modern bir .NET full-stack geliþtiricisinin bilmesi gereken temel ve ileri seviye birçok konsepti kapsamaktadýr.

### Temel Mimari ve Desenler (Core Architecture & Patterns)
- **Clean Architecture:** Sorumluluklarýn ayrýþtýrýldýðý 4 katmanlý (Domain, Application, Infrastructure, Presentation) mimari kurulumu.
- **CQRS (Command Query Responsibility Segregation):** Veri okuma (Query) ve yazma (Command) mantýklarýnýn birbirinden ayrýlmasý.
- **Mediator Pattern:** `MediatR` kütüphanesi ile katmanlar arasý baðýmlýlýðý azaltma ve istek/cevap akýþýný yönetme.
- **Repository & Unit of Work Patterns:** Veri eriþimini soyutlama ve veritabaný iþlemlerini tek bir transaction olarak yönetme.
- **Dependency Injection (DI):** Servislerin ve baðýmlýlýklarýn merkezi bir yerden yönetilmesi (`Program.cs` ve `ServiceExtensions`).

### API Geliþtirme (API Development)
- **ASP.NET Core Web API:** RESTful prensiplerine uygun (GET, POST, PUT, DELETE) endpoint'lerin oluþturulmasý.
- **Swagger (OpenAPI):** API'nin interaktif bir þekilde dokümante edilmesi ve test edilmesi.

### Veri Yönetimi ve Performans (Data Management & Performance)
- **Entity Framework Core:** Veritabaný iþlemleri için ORM kullanýmý (In-Memory Database ile).
- **Redis ile Daðýtýk Caching (Distributed Caching):** Sýk eriþilen verileri `IDistributedCache` arayüzü ile Redis üzerinde önbelleðe alarak performansý artýrma.
- **Bogus Kütüphanesi ile Veri Tohumlama (Data Seeding):** Testler için binlerce gerçekçi sahte verinin dinamik olarak üretilmesi.

### Kod Kalitesi ve Saðlamlýk (Code Quality & Robustness)
- **FluentValidation:** Ýþ kurallarýnýn zincirleme metotlarla temiz bir þekilde doðrulanmasý.
- **MediatR Pipeline Behaviors:** Caching ve Validation gibi kesiþen ilgileri (cross-cutting concerns) iþ mantýðýndan ayýrma.
- **Global Hata Yönetimi (Exception Handling Middleware):** Uygulamada oluþan hatalarý tek bir merkezden yakalayýp standart bir cevap formatýna dönüþtürme.

### Web Ýstemcisi ve Gerçek Zamanlý Ýletiþim (Web Client & Real-time Communication)
- **ASP.NET Core Razor Pages:** Sunucu taraflý, sayfa odaklý bir web arayüzü geliþtirme.
- **IHttpClientFactory:** Web istemcisinden API'ye güvenli ve verimli HTTP istekleri yapma.
- **Ýstemci & Sunucu Taraflý Sayfalama:** Farklý sayfalama stratejilerinin uygulanmasý ve karþýlaþtýrýlmasý.
- **SignalR:** Sunucudan istemciye anlýk veri gönderimi (server-push) ile gerçek zamanlý bildirim ve UI güncellemesi yapma.


# MiniETicaretAPI Projesi

Bu proje, Clean Architecture prensipleri kullanýlarak geliþtirilmiþ bir ASP.NET Core Web API'sidir.

## Proje Katmanlarý

### Domain
- Sorumluluðu: Projenin kalbidir. Ýþ kurallarýný ve varlýklarý (Entities) içerir. 
- Hiçbir katmana baðýmlý deðildir, herkes ona baðýmlýdýr. Örnek: Product sýnýfý burada olacak.

### Application
- Uygulamanýn ne yapacaðýný tanýmlar. Dýþ dünyadan gelen istekleri (örneðin "yeni ürün ekle") alýr ve Domain katmanýndaki varlýklarý 
- kullanarak iþ mantýðýný yönetir. 
- Örnek: CreateProductCommand gibi komutlar burada olacak.

### Infrastructure
-  Dýþ dünya ile ilgili teknik detaylarý içerir. Veritabaný baðlantýsý (Entity Framework Core), 
	- e-posta gönderimi, dýþ servislerle iletiþim gibi iþler bu katmanda yapýlýr.

### API (Presentation)
- Uygulamayý dýþ dünyaya sunan katmandýr. Gelen HTTP isteklerini karþýlar, 
- Application katmanýna gönderir ve dönen sonucu kullanýcýya HTTP cevabý olarak döner. 
- Örnek: ProductsController burada olacak.


## Domain Katmaný Detaylarý

### Varlýk (Entity) Nedir?
- Bir varlýk (entity), bir yazýlým sisteminde gerçek dünyadaki bir nesneyi veya kavramý temsil eden bir veri yapýsýdýr. 
- Genellikle bir veritabaný tablosuna karþýlýk gelir ve belirli bir kimlik (örneðin, bir ID) ile tanýmlanýr.


## Application Katmaný ve Kontratlar

Application katmaný, projenin iþ mantýðýný ve kullaným senaryolarýný (use cases) barýndýrýr. Bu katman, verinin nasýl saklandýðý gibi teknik detaylardan soyutlanmýþtýr. Bu soyutlamayý **arayüzler (interfaces)** aracýlýðýyla yaparýz.

### Repository Pattern Nedir?

Repository Pattern, veri eriþim mantýðýný soyutlayan bir tasarým desenidir. Amacý, `Application` katmanýný veritabaný gibi altyapýsal endiþelerden ayýrmaktýr.

- **`IProductRepository` Arayüzü:** `Application` katmanýnda tanýmladýðýmýz bu arayüz, bir ürün deposunun sahip olmasý gereken yetenekleri (tümünü getir, ID ile getir vb.) bir sözleþme olarak belirtir.
- **Uygulama (Implementation):** Bu arayüzün asýl kodunu (`Infrastructure` katmanýnda) daha sonra yazacaðýz. Böylece gelecekte veritabanýný SQL'den MongoDB'ye deðiþtirsek bile `Application` katmanýndaki kodumuz etkilenmez.

### DTO (Data Transfer Object) Nedir?

DTO, katmanlar veya servisler arasýnda veri taþýmak için kullanýlan basit nesnelerdir.

- **Amaç:** Domain varlýklarýmýzý (`Product` gibi) doðrudan dýþ dünyaya (API, UI) açmak güvenlik açýklarý yaratabilir ve gereksiz veri ifþasýna neden olabilir.
- **`ProductDto` Record'u:** API'mýzýn dýþ dünyaya sadece `Id`, `Name`, `Stock` ve `Price` alanlarýný göstereceðini tanýmlar. `.NET 8` ile gelen `record` tipi, deðiþmez (immutable) ve basit yapýsýyla DTO'lar için mükemmel bir seçimdir.

### CQRS, MediatR ve AutoMapper ile Ýþ Mantýðý

`Application` katmanýnda iþ mantýðýný organize etmek için modern ve etkili desenler kullanýyoruz.

#### CQRS (Command Query Responsibility Segregation) Deseni

Bu desen, uygulamamýzdaki **veri okuma (Query)** ve **veri yazma (Command)** sorumluluklarýný birbirinden ayýrýr.
- **Query:** Sistem durumunu deðiþtirmeyen, sadece veri döndüren iþlemlerdir. `GetAllProductsQuery` buna bir örnektir.
- **Command:** Sistem durumunu deðiþtiren iþlemlerdir (yeni ürün ekleme, güncelleme vb.).
Bu ayrým, kodun daha odaklý, temiz ve bakýmý kolay olmasýný saðlar. Projemizde `Features` klasörü altýnda bu ayrýmý fiziksel olarak da yapýyoruz.

#### Mediator Deseni ve `MediatR` Kütüphanesi

Mediator, nesneler arasý doðrudan iletiþimi azaltan bir "arabulucu" desenidir.
- **`MediatR` Kütüphanesi:** .NET'te bu deseni uygulamanýn en popüler yoludur. `API Controller` gibi bir istemci, `GetAllProductsQuery` gibi bir "istek" nesnesini MediatR'a gönderir. MediatR, bu isteði iþlemekle sorumlu olan `GetAllProductsQueryHandler`'ý bularak çalýþtýrýr.
- **Faydasý:** Bu, `Controller`'ýn `Handler` sýnýfýný doðrudan bilmesini engeller, böylece katmanlar arasýndaki baðýmlýlýk azalýr.

#### `AutoMapper` Kütüphanesi

`Domain` varlýklarýný (`Product`) `DTO`'lara (`ProductDto`) veya tam tersi yönde dönüþtürme iþlemini otomatikleþtiren bir kütüphanedir.
- **`GeneralProfile.cs`:** Bu dosyada, hangi tipin hangi tipe dönüþtürüleceðini `CreateMap<Kaynak, Hedef>()` metodu ile tanýmlarýz.
- **Faydasý:** Bizi, `productDto.Name = product.Name;` gibi sýkýcý ve hataya açýk manuel eþleþtirme kodlarýný yazmaktan kurtarýr.

## Parçalarý Birleþtirme: Altyapý ve API

### Infrastructure Katmaný: Somut Uygulama

Bu katman, `Application` katmanýnda tanýmlanan arayüzlerin (sözleþmelerin) somut uygulamalarýný içerir.
- **`ApplicationDbContext`:** Entity Framework Core'un veritabaný oturumunu temsil eden sýnýftýr. Hangi varlýklarýn hangi tablolara karþýlýk geldiðini (`DbSet<Product>`) burada belirtiriz.
- **`ProductRepository`:** `IProductRepository` arayüzünü uygular. `GetAllAsync` gibi metotlarýn içinde EF Core kullanarak veritabaný sorgularýný (örneðin `_context.Products.ToListAsync()`) çalýþtýrýr.
- **Veritabaný Seçimi:** Projemizde hýzlý baþlangýç için EF Core'un **In-Memory Database** saðlayýcýsýný kullandýk. Bu, herhangi bir veritabaný sunucusu kurmadan, tüm verileri uygulamanýn belleðinde tutarak çalýþmamýzý saðlar.

### Dependency Injection ve Konfigürasyon (`Program.cs`)

Uygulamamýzýn çalýþabilmesi için hangi servisin ne iþe yaradýðýný bilmesi gerekir. Bu "tanýtma" iþlemine servis kaydý (service registration) denir ve .NET'in Dependency Injection (DI) mekanizmasý ile yapýlýr.
- **`ServiceExtensions.cs`:** `Program.cs` dosyasýný temiz tutmak için servis kayýtlarýmýzý `Application` ve `Infrastructure` katmanlarýnda yazdýðýmýz geniþletme metotlarý (`AddApplicationServices`, `AddInfrastructureServices`) içinde topladýk.
- **Servis Yaþam Döngüsü (Service Lifetime):** `services.AddScoped<IProductRepository, ProductRepository>();` satýrý, DI konteynerine "Her bir HTTP isteði için yeni bir `ProductRepository` nesnesi oluþtur ve ayný istek içinde `IProductRepository` istendiðinde hep bu nesneyi ver" demektir.

### API Katmaný: Dünyaya Açýlan Kapý

- **`ProductsController`:** Dýþ dünyadan gelen `HTTP` isteklerini karþýlayan sýnýftýr. `[ApiController]` ve `[Route]` gibi niteliklerle (attributes) donatýlmýþtýr.
- **Ýnce Controller Felsefesi:** Controller'larýmýz iþ mantýðý içermez. Sadece gelen isteði doðrular, `MediatR` aracýlýðýyla ilgili `Handler`'a delege eder ve dönen sonucu `Ok(result)` gibi bir HTTP cevabýna dönüþtürerek kullanýcýya sunar.

### Veri Yazma: Command'ler, Validasyon ve Unit of Work

#### CQRS: Command Tarafý
Uygulamamýzýn durumunu deðiþtiren (veri ekleyen, güncelleyen, silen) tüm operasyonlar **Command**'ler aracýlýðýyla yapýlýr. Her Command, tek bir amaca hizmet eden ve kendi iþleyicisi (`Handler`) olan bir sýnýftýr.
- **`CreateProductCommand`**: Sisteme yeni bir ürün ekleme niyetini temsil eder. Ýçerisinde ürünün adý, fiyatý gibi bilgiler bulunur.
- **`Handler` Sorumluluðu**: Ýlgili `Handler`, Command'i alarak `Domain` varlýðýný oluþturur, `Repository` aracýlýðýyla veritabaný oturumuna ekler ve son olarak `UnitOfWork` ile deðiþiklikleri kaydeder.

#### `FluentValidation` ile Kurallý Doðrulama
Kullanýcýdan gelen verinin iþ kurallarýmýza uygunluðunu kontrol etmek kritiktir.
- **`FluentValidation`**: Bu kütüphane, `RuleFor(x => x.Name).NotEmpty()` gibi akýcý ve okunaklý bir sözdizimi ile karmaþýk validasyon kurallarý tanýmlamamýzý saðlar.
- **Faydasý**: `if-else` bloklarýyla dolu, karmaþýk `Handler`'lar yerine, validasyon mantýðýný kendi özel sýnýfýna taþýyarak kodumuzu temiz tutar.

#### `FluentValidation` ve MediatR Pipeline Behavior
Uygulamamýzda validasyon, `MediatR` pipeline'ýna entegre edilmiþ bir `Behavior` aracýlýðýyla, merkezi ve otomatik bir þekilde yapýlýr.
- **`ValidationBehavior`**: Bu özel sýnýf, bir `Command` veya `Query` ilgili `Handler`'ýna ulaþmadan hemen önce devreye girer. Gelen istek için tanýmlanmýþ bir `Validator` olup olmadýðýný kontrol eder. Eðer varsa, validasyon kurallarýný çalýþtýrýr.
- **Faydasý**: Validasyon mantýðý, `Handler`'larýn içinden tamamen soyutlanmýþ olur. Yeni bir `Command` eklediðimizde, sadece onun için bir `Validator` sýnýfý yazmamýz yeterlidir; `ValidationBehavior` onu otomatik olarak bulup çalýþtýracaktýr.

#### Global Hata Yönetimi (Exception Handling Middleware)
Uygulama içinde oluþabilecek istisnalarý (hatalarý) tek bir noktadan yönetmek için özel bir `Middleware` kullanýrýz.
- **`ExceptionHandlingMiddleware`**: HTTP istek pipeline'ýnýn en baþýnda yer alarak, 
- kendisinden sonraki tüm katmanlarda oluþabilecek hatalarý bir `try-catch` bloðu ile yakalar.
- **Ýþleyiþ**: `ValidationBehavior` bir `ValidationException` fýrlattýðýnda, bu `Middleware` hatayý yakalar 
- ve kullanýcýya standart bir `400 Bad Request` formatýnda JSON cevabý döner. Beklenmedik baþka bir hata (`Exception`) oluþtuðunda 
- ise `500 Internal Server Error` cevabý üreterek uygulamanýn çökmesini engeller ve kullanýcýya anlamsýz hata kodlarý göstermez. 
- Bu, API'mýzý daha saðlam ve kullanýcý dostu yapar.

#### Unit of Work Deseni ile Veri Bütünlüðü
Bu desen, veritabanýna yapýlacak bir veya daha fazla deðiþikliðin tek bir atomik iþlem (transaction) olarak ele alýnmasýný saðlar.
- **`IUnitOfWork`**: Deðiþiklikleri kaydetmek için tek bir metot (`SaveChangesAsync`) sunan bir arayüzdür.
- **Ýþleyiþ**: `Command Handler`'larýmýz, `Repository`'ler aracýlýðýyla istedikleri kadar ekleme, güncelleme, silme iþlemi yapabilirler. Bu iþlemler EF Core'un Change Tracker'ýnda birikir. `Handler`'ýn sonunda `_unitOfWork.SaveChangesAsync()` çaðrýldýðýnda, biriken tüm deðiþiklikler tek seferde veritabanýna gönderilir. 
- Eðer bu esnada bir hata olursa, hiçbir deðiþiklik kaydedilmez ve veri bütünlüðü korunur.

## Performans Optimizasyonu: Redis ile Caching
Sýk eriþilen veritabaný sorgularýnýn yarattýðý yükü azaltmak ve API yanýt sürelerini iyileþtirmek için daðýtýk önbellekleme (distributed caching) stratejisi kullanýyoruz. Bu amaçla sektör standardý olan **Redis**'i tercih ettik.

### Cache-Aside Stratejisi
Uyguladýðýmýz desen "Cache-Aside" olarak bilinir ve þu adýmlarý izler:
1.  **Cache'i Kontrol Et:** Bir veri talebi geldiðinde, uygulama önce Redis'e bakar.
2.  **Cache Hit:** Veri Redis'te mevcutsa, doðrudan buradan okunur ve kullanýcýya döndürülür. Veritabanýna gidilmez.
3.  **Cache Miss:** Veri Redis'te yoksa, uygulama veritabanýna gider, veriyi okur.
4.  **Cache'i Doldur:** Veritabanýndan okunan veri, bir sonraki istekte kullanýlmak üzere Redis'e yazýlýr.
5.  **Veriyi Döndür:** Veritabanýndan okunan veri kullanýcýya döndürülür.

### MediatR `CachingBehavior`
Bu caching mantýðýný, tüm iþleyicilerden (`Handler`) soyutlamak için bir `MediatR Pipeline Behavior` olarak tasarladýk.
- **`ICacheableRequest`**: Hangi `Query`'lerin cache'leneceðini belirtmek için kullanýlan bir iþaretçi arayüzdür. Cache anahtarý (`CacheKey`) ve geçerlilik süresi (`SlidingExpiration`) gibi bilgileri içerir.
- **`CachingBehavior`**: `ICacheableRequest` arayüzünü uygulayan istekleri yakalar ve yukarýda anlatýlan Cache-Aside mantýðýný otomatik olarak çalýþtýrýr.

### Cache Invalidation (Önbelleði Geçersiz Kýlma)
Önbellekte tutulan verinin güncelliðini korumak, caching stratejisinin en kritik parçasýdýr.
- **Problem:** Veritabanýndaki bir veri deðiþtirildiðinde (yeni ürün ekleme, güncelleme, silme), cache'teki kopya "eski" kalýr.
- **Çözüm:** Veriyi deðiþtiren herhangi bir **Command** (`Create`, `Update`, `Delete`) baþarýyla tamamlandýktan sonra, ilgili cache anahtarý (`GetAllProducts` gibi) 
- Redis'ten bilinçli olarak silinir. Bu sayede bir sonraki okuma isteði, en güncel veriyi veritabanýndan çekmek ve cache'i 
- yeniden doldurmak zorunda kalýr.

# .NET 8 Clean Architecture API ve Razor Pages Projesi

Bu proje, modern .NET teknolojileri kullanýlarak geliþtirilmiþ, Clean Architecture prensiplerine uygun bir API ve bu API ile haberleþen bir Razor Pages web uygulamasýndan oluþur. Proje, bir full-stack .NET geliþtiricisinin kariyerinde bir sonraki adýmý atmasý için gereken temel ve ileri seviye konularý pratik bir þekilde öðretmeyi amaçlar.

## Proje Mimarisi ve Önemli Konseptler

Proje, bakýmý kolay, test edilebilir ve ölçeklenebilir bir yapý saðlamak için **Clean Architecture** prensiplerine göre katmanlara ayrýlmýþtýr: `Domain`, `Application`, `Infrastructure` ve `Presentation`.

### Sayfalama (Pagination) Yaklaþýmý
Uygulamada, çok sayýda verinin verimli bir þekilde sunulmasý için **Ýstemci Taraflý Sayfalama (Client-Side Pagination)** yaklaþýmý benimsenmiþtir.

- **Veri Akýþý:** `API`, `/api/products` endpoint'inden tüm ürün verisini tek bir seferde döndürür. Bu sonuç, `Infrastructure` katmanýnda **Redis** ile cache'lenir.
- **Mantýk:** `Presentation` katmanýndaki `Razor Pages` uygulamasý, bu tüm veri setini alýr. Sayfalara bölme iþlemi, C# tarafýnda (`Index.cshtml.cs` içinde) **LINQ'in `.Skip()` ve `.Take()` metotlarý** kullanýlarak bellek üzerinde gerçekleþtirilir.
- **Gerekçe:** Bu yaklaþým, API ve cache'leme mantýðýný son derece basit tutar. Ancak bu, sadece eðitim amaçlý ve yönetilebilir boyuttaki veri setleri için tercih edilmiþtir. Çok büyük veri setleri (50.000+) için endüstri standardý, her sayfa için ayrý bir veritabaný sorgusu yapan **Sunucu Taraflý Sayfalama**'dýr.

### Akýllý Sayfalama Kontrolü (Smart Pagination Control)
Kullanýcý deneyimini iyileþtirmek için, yüzlerce sayfa numarasýnýn tamamýný listelemek yerine, sadece mevcut sayfanýn etrafýndaki birkaç sayfa numarasýný gösteren akýllý bir arayüz geliþtirilmiþtir.
- **Hesaplama:** Gösterilecek sayfa aralýðý (`StartPage`, `EndPage`), `IndexModel` C# sýnýfý içinde dinamik olarak hesaplanýr.
- **Görünüm:** Kullanýcý arayüzü, `[1] [...] [5] [6] [7] [...] [100]` gibi bir formatta oluþturularak kolay bir gezinti imkaný sunar.

### Gerçek Zamanlý Bildirimler (SignalR)
Uygulamaya anlýk iletiþim yeteneði kazandýrmak için **ASP.NET Core SignalR** kullanýlmýþtýr.
- **Hub:** `API` projesi, istemcilerle iletiþimi yöneten bir `ProductHub` içerir.
- **Soyutlama:** `Application` katmaný, SignalR'dan tamamen habersizdir. Bildirim gönderme iþlemi, `Application` katmanýnda tanýmlanan `IProductHubService` arayüzü üzerinden yapýlýr. Bu arayüzün somut uygulamasý `API` projesinde yer alýr ve `IHubContext` kullanarak istemcilere mesaj gönderir.
- **Senaryo:** Sisteme yeni bir ürün eklendiðinde, `CreateProductCommandHandler` bu servis aracýlýðýyla bir bildirim tetikler. Bildirim, `ProductHub`'a baðlý olan tüm web istemcilerine anýnda gönderilir ve þýk bir "Toast" mesajý olarak ekranda belirir.

Bu proje, modern bir web uygulamasýnýn temel taþlarýný oluþturan performans, mimari, kullanýcý deneyimi ve gerçek zamanlý iletiþim gibi konularý bir araya getiren bütüncül bir örnektir.