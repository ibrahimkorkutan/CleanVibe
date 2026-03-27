# CleanVibe

CleanVibe, **Clean Architecture** ilkelerine uygun olarak tasarlanmış bir örnek çözümdür. İş kuralları, uygulama orchestration’ı, altyapı ve sunum katmanları net sınırlarla ayrılır; bağımlılıklar daima **içe doğru** (Domain merkezde) yönelir.

## Teknoloji yığını

| Alan | Seçim |
|------|--------|
| Çerçeve | **.NET 10** |
| API | **ASP.NET Core Minimal API** |
| CQRS / mediator | **MediatR** |
| Doğrulama | **FluentValidation** + pipeline davranışı |
| Veri erişimi | **Entity Framework Core** (örnek: InMemory; SQL Server paketi mevcut) |
| API dokümantasyonu | **OpenAPI** (`MapOpenApi`), **Scalar** (`/scalar/v1`), **Swagger / Swashbuckle** (geliştirme) |

## Çözüm yapısı

### CleanVibe.Domain

Çekirdek iş modeli. Entity’ler, değer nesneleri ve domain arayüzleri burada yer alır. **Harici framework’lere veya altyapıya bağımlılık yoktur**; saf C# ile kalınır.

### CleanVibe.Application

Use case’lerin ve uygulama kurallarının yaşadığı katman. **MediatR** ile komut/sorgu işleyicileri, **FluentValidation** ile doğrulayıcılar ve çapraz kesen **pipeline** davranışları (ör. doğrulama) burada tanımlanır. Kalıcı depolama detayları **yalnızca arayüzler** (ör. `IApplicationDbContext`, `IDateTimeService`) üzerinden soyutlanır.

### CleanVibe.Persistence

EF Core `DbContext`, entity konfigürasyonları, **audit interceptor** ve kalıcılık odaklı **DI** uzantıları bu projededir. **Application** katmanındaki veri erişimi sözleşmelerini uygular; veritabanı sağlayıcısı ve bağlantı/InMemory adı yapılandırmadan gelir.

### CleanVibe.Infrastructure

Uygulamaya yönelik **teknik altyapı** bileşenleri (ör. merkezi `DateTimeService` / `IDateTimeService` uygulaması) bu katmanda toplanır. İleride e-posta, önbellek, harici API istemcileri vb. buraya veya ilgili alt projelere eklenebilir.

### CleanVibe.Api

**Sunum / giriş noktası** katmanı. Minimal API uç noktaları, global hata işleme, OpenAPI/Scalar/Swagger ve `Program.cs` üzerinden servis kayıtları burada bir araya getirilir.

## Bağımlılık yönü (özet)

```text
Api  →  Application, Persistence, Infrastructure
Persistence, Infrastructure  →  Application
Application  →  Domain
```

`Persistence` ve `Infrastructure` birbirini referans almaz; ikisi de **Application** üzerinden Domain ile hizalanır.

## Çalıştırma

```bash
dotnet run --project CleanVibe.Api
```

Geliştirme ortamında:

- **Scalar:** `/scalar/v1`
- **Swagger UI:** `/swagger`
- **OpenAPI JSON:** `/openapi/v1.json`

## Lisans

Bu depo örnek / eğitim amaçlıdır; ihtiyacın olan lisansı ekleyebilirsin.
