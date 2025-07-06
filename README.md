## Projenin 1. A�amas�nda ��lenen Konular ve Teknolojiler

Bu proje, modern bir .NET full-stack geli�tiricisinin bilmesi gereken temel ve ileri seviye bir�ok konsepti kapsamaktad�r.

### Temel Mimari ve Desenler (Core Architecture & Patterns)
- **Clean Architecture:** Sorumluluklar�n ayr��t�r�ld��� 4 katmanl� (Domain, Application, Infrastructure, Presentation) mimari kurulumu.
- **CQRS (Command Query Responsibility Segregation):** Veri okuma (Query) ve yazma (Command) mant�klar�n�n birbirinden ayr�lmas�.
- **Mediator Pattern:** `MediatR` k�t�phanesi ile katmanlar aras� ba��ml�l��� azaltma ve istek/cevap ak���n� y�netme.
- **Repository & Unit of Work Patterns:** Veri eri�imini soyutlama ve veritaban� i�lemlerini tek bir transaction olarak y�netme.
- **Dependency Injection (DI):** Servislerin ve ba��ml�l�klar�n merkezi bir yerden y�netilmesi (`Program.cs` ve `ServiceExtensions`).

### API Geli�tirme (API Development)
- **ASP.NET Core Web API:** RESTful prensiplerine uygun (GET, POST, PUT, DELETE) endpoint'lerin olu�turulmas�.
- **Swagger (OpenAPI):** API'nin interaktif bir �ekilde dok�mante edilmesi ve test edilmesi.

### Veri Y�netimi ve Performans (Data Management & Performance)
- **Entity Framework Core:** Veritaban� i�lemleri i�in ORM kullan�m� (In-Memory Database ile).
- **Redis ile Da��t�k Caching (Distributed Caching):** S�k eri�ilen verileri `IDistributedCache` aray�z� ile Redis �zerinde �nbelle�e alarak performans� art�rma.
- **Bogus K�t�phanesi ile Veri Tohumlama (Data Seeding):** Testler i�in binlerce ger�ek�i sahte verinin dinamik olarak �retilmesi.

### Kod Kalitesi ve Sa�laml�k (Code Quality & Robustness)
- **FluentValidation:** �� kurallar�n�n zincirleme metotlarla temiz bir �ekilde do�rulanmas�.
- **MediatR Pipeline Behaviors:** Caching ve Validation gibi kesi�en ilgileri (cross-cutting concerns) i� mant���ndan ay�rma.
- **Global Hata Y�netimi (Exception Handling Middleware):** Uygulamada olu�an hatalar� tek bir merkezden yakalay�p standart bir cevap format�na d�n��t�rme.

### Web �stemcisi ve Ger�ek Zamanl� �leti�im (Web Client & Real-time Communication)
- **ASP.NET Core Razor Pages:** Sunucu tarafl�, sayfa odakl� bir web aray�z� geli�tirme.
- **IHttpClientFactory:** Web istemcisinden API'ye g�venli ve verimli HTTP istekleri yapma.
- **�stemci & Sunucu Tarafl� Sayfalama:** Farkl� sayfalama stratejilerinin uygulanmas� ve kar��la�t�r�lmas�.
- **SignalR:** Sunucudan istemciye anl�k veri g�nderimi (server-push) ile ger�ek zamanl� bildirim ve UI g�ncellemesi yapma.


# MiniETicaretAPI Projesi

Bu proje, Clean Architecture prensipleri kullan�larak geli�tirilmi� bir ASP.NET Core Web API'sidir.

## Proje Katmanlar�

### Domain
- Sorumlulu�u: Projenin kalbidir. �� kurallar�n� ve varl�klar� (Entities) i�erir. 
- Hi�bir katmana ba��ml� de�ildir, herkes ona ba��ml�d�r. �rnek: Product s�n�f� burada olacak.

### Application
- Uygulaman�n ne yapaca��n� tan�mlar. D�� d�nyadan gelen istekleri (�rne�in "yeni �r�n ekle") al�r ve Domain katman�ndaki varl�klar� 
- kullanarak i� mant���n� y�netir. 
- �rnek: CreateProductCommand gibi komutlar burada olacak.

### Infrastructure
-  D�� d�nya ile ilgili teknik detaylar� i�erir. Veritaban� ba�lant�s� (Entity Framework Core), 
	- e-posta g�nderimi, d�� servislerle ileti�im gibi i�ler bu katmanda yap�l�r.

### API (Presentation)
- Uygulamay� d�� d�nyaya sunan katmand�r. Gelen HTTP isteklerini kar��lar, 
- Application katman�na g�nderir ve d�nen sonucu kullan�c�ya HTTP cevab� olarak d�ner. 
- �rnek: ProductsController burada olacak.


## Domain Katman� Detaylar�

### Varl�k (Entity) Nedir?
- Bir varl�k (entity), bir yaz�l�m sisteminde ger�ek d�nyadaki bir nesneyi veya kavram� temsil eden bir veri yap�s�d�r. 
- Genellikle bir veritaban� tablosuna kar��l�k gelir ve belirli bir kimlik (�rne�in, bir ID) ile tan�mlan�r.


## Application Katman� ve Kontratlar

Application katman�, projenin i� mant���n� ve kullan�m senaryolar�n� (use cases) bar�nd�r�r. Bu katman, verinin nas�l sakland��� gibi teknik detaylardan soyutlanm��t�r. Bu soyutlamay� **aray�zler (interfaces)** arac�l���yla yapar�z.

### Repository Pattern Nedir?

Repository Pattern, veri eri�im mant���n� soyutlayan bir tasar�m desenidir. Amac�, `Application` katman�n� veritaban� gibi altyap�sal endi�elerden ay�rmakt�r.

- **`IProductRepository` Aray�z�:** `Application` katman�nda tan�mlad���m�z bu aray�z, bir �r�n deposunun sahip olmas� gereken yetenekleri (t�m�n� getir, ID ile getir vb.) bir s�zle�me olarak belirtir.
- **Uygulama (Implementation):** Bu aray�z�n as�l kodunu (`Infrastructure` katman�nda) daha sonra yazaca��z. B�ylece gelecekte veritaban�n� SQL'den MongoDB'ye de�i�tirsek bile `Application` katman�ndaki kodumuz etkilenmez.

### DTO (Data Transfer Object) Nedir?

DTO, katmanlar veya servisler aras�nda veri ta��mak i�in kullan�lan basit nesnelerdir.

- **Ama�:** Domain varl�klar�m�z� (`Product` gibi) do�rudan d�� d�nyaya (API, UI) a�mak g�venlik a��klar� yaratabilir ve gereksiz veri if�as�na neden olabilir.
- **`ProductDto` Record'u:** API'm�z�n d�� d�nyaya sadece `Id`, `Name`, `Stock` ve `Price` alanlar�n� g�sterece�ini tan�mlar. `.NET 8` ile gelen `record` tipi, de�i�mez (immutable) ve basit yap�s�yla DTO'lar i�in m�kemmel bir se�imdir.

### CQRS, MediatR ve AutoMapper ile �� Mant���

`Application` katman�nda i� mant���n� organize etmek i�in modern ve etkili desenler kullan�yoruz.

#### CQRS (Command Query Responsibility Segregation) Deseni

Bu desen, uygulamam�zdaki **veri okuma (Query)** ve **veri yazma (Command)** sorumluluklar�n� birbirinden ay�r�r.
- **Query:** Sistem durumunu de�i�tirmeyen, sadece veri d�nd�ren i�lemlerdir. `GetAllProductsQuery` buna bir �rnektir.
- **Command:** Sistem durumunu de�i�tiren i�lemlerdir (yeni �r�n ekleme, g�ncelleme vb.).
Bu ayr�m, kodun daha odakl�, temiz ve bak�m� kolay olmas�n� sa�lar. Projemizde `Features` klas�r� alt�nda bu ayr�m� fiziksel olarak da yap�yoruz.

#### Mediator Deseni ve `MediatR` K�t�phanesi

Mediator, nesneler aras� do�rudan ileti�imi azaltan bir "arabulucu" desenidir.
- **`MediatR` K�t�phanesi:** .NET'te bu deseni uygulaman�n en pop�ler yoludur. `API Controller` gibi bir istemci, `GetAllProductsQuery` gibi bir "istek" nesnesini MediatR'a g�nderir. MediatR, bu iste�i i�lemekle sorumlu olan `GetAllProductsQueryHandler`'� bularak �al��t�r�r.
- **Faydas�:** Bu, `Controller`'�n `Handler` s�n�f�n� do�rudan bilmesini engeller, b�ylece katmanlar aras�ndaki ba��ml�l�k azal�r.

#### `AutoMapper` K�t�phanesi

`Domain` varl�klar�n� (`Product`) `DTO`'lara (`ProductDto`) veya tam tersi y�nde d�n��t�rme i�lemini otomatikle�tiren bir k�t�phanedir.
- **`GeneralProfile.cs`:** Bu dosyada, hangi tipin hangi tipe d�n��t�r�lece�ini `CreateMap<Kaynak, Hedef>()` metodu ile tan�mlar�z.
- **Faydas�:** Bizi, `productDto.Name = product.Name;` gibi s�k�c� ve hataya a��k manuel e�le�tirme kodlar�n� yazmaktan kurtar�r.

## Par�alar� Birle�tirme: Altyap� ve API

### Infrastructure Katman�: Somut Uygulama

Bu katman, `Application` katman�nda tan�mlanan aray�zlerin (s�zle�melerin) somut uygulamalar�n� i�erir.
- **`ApplicationDbContext`:** Entity Framework Core'un veritaban� oturumunu temsil eden s�n�ft�r. Hangi varl�klar�n hangi tablolara kar��l�k geldi�ini (`DbSet<Product>`) burada belirtiriz.
- **`ProductRepository`:** `IProductRepository` aray�z�n� uygular. `GetAllAsync` gibi metotlar�n i�inde EF Core kullanarak veritaban� sorgular�n� (�rne�in `_context.Products.ToListAsync()`) �al��t�r�r.
- **Veritaban� Se�imi:** Projemizde h�zl� ba�lang�� i�in EF Core'un **In-Memory Database** sa�lay�c�s�n� kulland�k. Bu, herhangi bir veritaban� sunucusu kurmadan, t�m verileri uygulaman�n belle�inde tutarak �al��mam�z� sa�lar.

### Dependency Injection ve Konfig�rasyon (`Program.cs`)

Uygulamam�z�n �al��abilmesi i�in hangi servisin ne i�e yarad���n� bilmesi gerekir. Bu "tan�tma" i�lemine servis kayd� (service registration) denir ve .NET'in Dependency Injection (DI) mekanizmas� ile yap�l�r.
- **`ServiceExtensions.cs`:** `Program.cs` dosyas�n� temiz tutmak i�in servis kay�tlar�m�z� `Application` ve `Infrastructure` katmanlar�nda yazd���m�z geni�letme metotlar� (`AddApplicationServices`, `AddInfrastructureServices`) i�inde toplad�k.
- **Servis Ya�am D�ng�s� (Service Lifetime):** `services.AddScoped<IProductRepository, ProductRepository>();` sat�r�, DI konteynerine "Her bir HTTP iste�i i�in yeni bir `ProductRepository` nesnesi olu�tur ve ayn� istek i�inde `IProductRepository` istendi�inde hep bu nesneyi ver" demektir.

### API Katman�: D�nyaya A��lan Kap�

- **`ProductsController`:** D�� d�nyadan gelen `HTTP` isteklerini kar��layan s�n�ft�r. `[ApiController]` ve `[Route]` gibi niteliklerle (attributes) donat�lm��t�r.
- **�nce Controller Felsefesi:** Controller'lar�m�z i� mant��� i�ermez. Sadece gelen iste�i do�rular, `MediatR` arac�l���yla ilgili `Handler`'a delege eder ve d�nen sonucu `Ok(result)` gibi bir HTTP cevab�na d�n��t�rerek kullan�c�ya sunar.

### Veri Yazma: Command'ler, Validasyon ve Unit of Work

#### CQRS: Command Taraf�
Uygulamam�z�n durumunu de�i�tiren (veri ekleyen, g�ncelleyen, silen) t�m operasyonlar **Command**'ler arac�l���yla yap�l�r. Her Command, tek bir amaca hizmet eden ve kendi i�leyicisi (`Handler`) olan bir s�n�ft�r.
- **`CreateProductCommand`**: Sisteme yeni bir �r�n ekleme niyetini temsil eder. ��erisinde �r�n�n ad�, fiyat� gibi bilgiler bulunur.
- **`Handler` Sorumlulu�u**: �lgili `Handler`, Command'i alarak `Domain` varl���n� olu�turur, `Repository` arac�l���yla veritaban� oturumuna ekler ve son olarak `UnitOfWork` ile de�i�iklikleri kaydeder.

#### `FluentValidation` ile Kurall� Do�rulama
Kullan�c�dan gelen verinin i� kurallar�m�za uygunlu�unu kontrol etmek kritiktir.
- **`FluentValidation`**: Bu k�t�phane, `RuleFor(x => x.Name).NotEmpty()` gibi ak�c� ve okunakl� bir s�zdizimi ile karma��k validasyon kurallar� tan�mlamam�z� sa�lar.
- **Faydas�**: `if-else` bloklar�yla dolu, karma��k `Handler`'lar yerine, validasyon mant���n� kendi �zel s�n�f�na ta��yarak kodumuzu temiz tutar.

#### `FluentValidation` ve MediatR Pipeline Behavior
Uygulamam�zda validasyon, `MediatR` pipeline'�na entegre edilmi� bir `Behavior` arac�l���yla, merkezi ve otomatik bir �ekilde yap�l�r.
- **`ValidationBehavior`**: Bu �zel s�n�f, bir `Command` veya `Query` ilgili `Handler`'�na ula�madan hemen �nce devreye girer. Gelen istek i�in tan�mlanm�� bir `Validator` olup olmad���n� kontrol eder. E�er varsa, validasyon kurallar�n� �al��t�r�r.
- **Faydas�**: Validasyon mant���, `Handler`'lar�n i�inden tamamen soyutlanm�� olur. Yeni bir `Command` ekledi�imizde, sadece onun i�in bir `Validator` s�n�f� yazmam�z yeterlidir; `ValidationBehavior` onu otomatik olarak bulup �al��t�racakt�r.

#### Global Hata Y�netimi (Exception Handling Middleware)
Uygulama i�inde olu�abilecek istisnalar� (hatalar�) tek bir noktadan y�netmek i�in �zel bir `Middleware` kullan�r�z.
- **`ExceptionHandlingMiddleware`**: HTTP istek pipeline'�n�n en ba��nda yer alarak, 
- kendisinden sonraki t�m katmanlarda olu�abilecek hatalar� bir `try-catch` blo�u ile yakalar.
- **��leyi�**: `ValidationBehavior` bir `ValidationException` f�rlatt���nda, bu `Middleware` hatay� yakalar 
- ve kullan�c�ya standart bir `400 Bad Request` format�nda JSON cevab� d�ner. Beklenmedik ba�ka bir hata (`Exception`) olu�tu�unda 
- ise `500 Internal Server Error` cevab� �reterek uygulaman�n ��kmesini engeller ve kullan�c�ya anlams�z hata kodlar� g�stermez. 
- Bu, API'm�z� daha sa�lam ve kullan�c� dostu yapar.

#### Unit of Work Deseni ile Veri B�t�nl���
Bu desen, veritaban�na yap�lacak bir veya daha fazla de�i�ikli�in tek bir atomik i�lem (transaction) olarak ele al�nmas�n� sa�lar.
- **`IUnitOfWork`**: De�i�iklikleri kaydetmek i�in tek bir metot (`SaveChangesAsync`) sunan bir aray�zd�r.
- **��leyi�**: `Command Handler`'lar�m�z, `Repository`'ler arac�l���yla istedikleri kadar ekleme, g�ncelleme, silme i�lemi yapabilirler. Bu i�lemler EF Core'un Change Tracker'�nda birikir. `Handler`'�n sonunda `_unitOfWork.SaveChangesAsync()` �a�r�ld���nda, biriken t�m de�i�iklikler tek seferde veritaban�na g�nderilir. 
- E�er bu esnada bir hata olursa, hi�bir de�i�iklik kaydedilmez ve veri b�t�nl��� korunur.

## Performans Optimizasyonu: Redis ile Caching
S�k eri�ilen veritaban� sorgular�n�n yaratt��� y�k� azaltmak ve API yan�t s�relerini iyile�tirmek i�in da��t�k �nbellekleme (distributed caching) stratejisi kullan�yoruz. Bu ama�la sekt�r standard� olan **Redis**'i tercih ettik.

### Cache-Aside Stratejisi
Uygulad���m�z desen "Cache-Aside" olarak bilinir ve �u ad�mlar� izler:
1.  **Cache'i Kontrol Et:** Bir veri talebi geldi�inde, uygulama �nce Redis'e bakar.
2.  **Cache Hit:** Veri Redis'te mevcutsa, do�rudan buradan okunur ve kullan�c�ya d�nd�r�l�r. Veritaban�na gidilmez.
3.  **Cache Miss:** Veri Redis'te yoksa, uygulama veritaban�na gider, veriyi okur.
4.  **Cache'i Doldur:** Veritaban�ndan okunan veri, bir sonraki istekte kullan�lmak �zere Redis'e yaz�l�r.
5.  **Veriyi D�nd�r:** Veritaban�ndan okunan veri kullan�c�ya d�nd�r�l�r.

### MediatR `CachingBehavior`
Bu caching mant���n�, t�m i�leyicilerden (`Handler`) soyutlamak i�in bir `MediatR Pipeline Behavior` olarak tasarlad�k.
- **`ICacheableRequest`**: Hangi `Query`'lerin cache'lenece�ini belirtmek i�in kullan�lan bir i�aret�i aray�zd�r. Cache anahtar� (`CacheKey`) ve ge�erlilik s�resi (`SlidingExpiration`) gibi bilgileri i�erir.
- **`CachingBehavior`**: `ICacheableRequest` aray�z�n� uygulayan istekleri yakalar ve yukar�da anlat�lan Cache-Aside mant���n� otomatik olarak �al��t�r�r.

### Cache Invalidation (�nbelle�i Ge�ersiz K�lma)
�nbellekte tutulan verinin g�ncelli�ini korumak, caching stratejisinin en kritik par�as�d�r.
- **Problem:** Veritaban�ndaki bir veri de�i�tirildi�inde (yeni �r�n ekleme, g�ncelleme, silme), cache'teki kopya "eski" kal�r.
- **��z�m:** Veriyi de�i�tiren herhangi bir **Command** (`Create`, `Update`, `Delete`) ba�ar�yla tamamland�ktan sonra, ilgili cache anahtar� (`GetAllProducts` gibi) 
- Redis'ten bilin�li olarak silinir. Bu sayede bir sonraki okuma iste�i, en g�ncel veriyi veritaban�ndan �ekmek ve cache'i 
- yeniden doldurmak zorunda kal�r.

# .NET 8 Clean Architecture API ve Razor Pages Projesi

Bu proje, modern .NET teknolojileri kullan�larak geli�tirilmi�, Clean Architecture prensiplerine uygun bir API ve bu API ile haberle�en bir Razor Pages web uygulamas�ndan olu�ur. Proje, bir full-stack .NET geli�tiricisinin kariyerinde bir sonraki ad�m� atmas� i�in gereken temel ve ileri seviye konular� pratik bir �ekilde ��retmeyi ama�lar.

## Proje Mimarisi ve �nemli Konseptler

Proje, bak�m� kolay, test edilebilir ve �l�eklenebilir bir yap� sa�lamak i�in **Clean Architecture** prensiplerine g�re katmanlara ayr�lm��t�r: `Domain`, `Application`, `Infrastructure` ve `Presentation`.

### Sayfalama (Pagination) Yakla��m�
Uygulamada, �ok say�da verinin verimli bir �ekilde sunulmas� i�in **�stemci Tarafl� Sayfalama (Client-Side Pagination)** yakla��m� benimsenmi�tir.

- **Veri Ak���:** `API`, `/api/products` endpoint'inden t�m �r�n verisini tek bir seferde d�nd�r�r. Bu sonu�, `Infrastructure` katman�nda **Redis** ile cache'lenir.
- **Mant�k:** `Presentation` katman�ndaki `Razor Pages` uygulamas�, bu t�m veri setini al�r. Sayfalara b�lme i�lemi, C# taraf�nda (`Index.cshtml.cs` i�inde) **LINQ'in `.Skip()` ve `.Take()` metotlar�** kullan�larak bellek �zerinde ger�ekle�tirilir.
- **Gerek�e:** Bu yakla��m, API ve cache'leme mant���n� son derece basit tutar. Ancak bu, sadece e�itim ama�l� ve y�netilebilir boyuttaki veri setleri i�in tercih edilmi�tir. �ok b�y�k veri setleri (50.000+) i�in end�stri standard�, her sayfa i�in ayr� bir veritaban� sorgusu yapan **Sunucu Tarafl� Sayfalama**'d�r.

### Ak�ll� Sayfalama Kontrol� (Smart Pagination Control)
Kullan�c� deneyimini iyile�tirmek i�in, y�zlerce sayfa numaras�n�n tamam�n� listelemek yerine, sadece mevcut sayfan�n etraf�ndaki birka� sayfa numaras�n� g�steren ak�ll� bir aray�z geli�tirilmi�tir.
- **Hesaplama:** G�sterilecek sayfa aral��� (`StartPage`, `EndPage`), `IndexModel` C# s�n�f� i�inde dinamik olarak hesaplan�r.
- **G�r�n�m:** Kullan�c� aray�z�, `[1] [...] [5] [6] [7] [...] [100]` gibi bir formatta olu�turularak kolay bir gezinti imkan� sunar.

### Ger�ek Zamanl� Bildirimler (SignalR)
Uygulamaya anl�k ileti�im yetene�i kazand�rmak i�in **ASP.NET Core SignalR** kullan�lm��t�r.
- **Hub:** `API` projesi, istemcilerle ileti�imi y�neten bir `ProductHub` i�erir.
- **Soyutlama:** `Application` katman�, SignalR'dan tamamen habersizdir. Bildirim g�nderme i�lemi, `Application` katman�nda tan�mlanan `IProductHubService` aray�z� �zerinden yap�l�r. Bu aray�z�n somut uygulamas� `API` projesinde yer al�r ve `IHubContext` kullanarak istemcilere mesaj g�nderir.
- **Senaryo:** Sisteme yeni bir �r�n eklendi�inde, `CreateProductCommandHandler` bu servis arac�l���yla bir bildirim tetikler. Bildirim, `ProductHub`'a ba�l� olan t�m web istemcilerine an�nda g�nderilir ve ��k bir "Toast" mesaj� olarak ekranda belirir.

Bu proje, modern bir web uygulamas�n�n temel ta�lar�n� olu�turan performans, mimari, kullan�c� deneyimi ve ger�ek zamanl� ileti�im gibi konular� bir araya getiren b�t�nc�l bir �rnektir.