# Net Payroll System

## 📌 Proje Hakkında
Net Payroll System, **.NET 8** tabanlı, mikroservis mimarisi ile geliştirilmiş bir **bordro hesaplama sistemi**dir.  
Proje, personel maaş hesaplamalarını yasal mevzuata uygun şekilde otomatikleştirir ve **parametre bazlı** çalışır.  
Tüm oranlar ve vergi dilimleri **Parameter.API** üzerinden yönetilir, böylece kod değişikliğine gerek kalmadan oran güncellemeleri yapılabilir.

## 🎯 Amaç
- Personel bordrosunu doğru ve güncel yasal oranlarla hesaplamak
- Mikroservis mimarisi ile esnek, modüler ve ölçeklenebilir bir yapı sağlamak
- Parametre yönetimini koddan bağımsız hale getirerek muhasebe birimlerinin oranları panelden güncelleyebilmesi
- Event-driven (RabbitMQ) mimarisi ile servisler arası veri alışverişini asenkron sağlamak

## 🏗 Mimari Bileşenler
Proje aşağıdaki mikroservislerden oluşur:

1. **Parameter.API**
   - Vergi oranları, SGK oranları, yemek yardımı muaf tutarı, gelir vergisi dilimleri vb. parametreleri tutar.
   - Parametreler `ParameterName` enum ile yönetilir ve veritabanı key’lerine eşlenir.
   - REST endpoint ile diğer servislere veri sağlar.

2. **Personel.API**
   - Personel bilgilerini (maaş, çalışma günü, mesai saatleri vb.) tutar.
   - CRUD işlemleri yapılabilir.
   - Bordro hesaplamasında gerekli verileri sağlar.

3. **Calculation.API**
   - Bordro hesaplama adımlarını içerir.
   - **Payroll Steps** mantığıyla hesaplama adım adım yapılır:
     - Brüt gelir hesaplama
     - SGK matrahı ve primleri
     - İşsizlik sigortası primi
     - Gelir vergisi matrahı ve oranları
     - Damga vergisi
     - Net ücret hesaplama
   - Parametreleri **Parameter.API** üzerinden çeker.
   - Event tüketicileri ile gelen hesaplama taleplerini işler.

## ⚙️ Kullanılan Teknolojiler
- **.NET 8 Web API**
- **Entity Framework Core**
- **MSSQL** (Personel, parametre ve bordro verileri için)
- **RabbitMQ + MassTransit** (Servisler arası iletişim)
- **Swagger** (API dokümantasyonu)
- **AutoMapper** (DTO <-> Entity dönüşümleri)
- **Dependency Injection** (Gevşek bağlı servis yönetimi)

## 🔄 Veri Akışı
1. **Parameter.API** → Güncel parametreleri sağlar.
2. **Personel.API** → Personel bilgilerini döner.
3. **Calculation.API** → Parametreleri ve personel verilerini alır, bordro hesaplamasını yapar.
4. Hesaplama adımları `Payroll Steps` zinciri ile yürütülür.
5. Sonuç JSON formatında döndürülür
