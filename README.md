# 📦 Stok Takip Programı

Modern arayüzlü, rol bazlı yetkilendirme destekleyen, C# WinForms ve SQL Server ile geliştirilmiş stok takip, depo yönetimi, satış/fatura ve raporlama sistemidir.

---

## 🚀 Projenin Amacı

Bu proje; ürünlerin, depoların, stok hareketlerinin ve satış işlemlerinin tek bir sistem üzerinden yönetilmesini sağlamak amacıyla geliştirilmiştir.

Sistem sayesinde:

- Ürün yönetimi yapılabilir
- Depolar yönetilebilir
- Stok takibi yapılabilir
- Satış/fatura işlemleri gerçekleştirilebilir
- Stok sorgulaması yapılabilir
- Admin kullanıcılar gelişmiş raporlara erişebilir
- Rol bazlı kullanıcı yönetimi uygulanabilir

---

## 🛠 Kullanılan Teknolojiler

- C#
- WinForms
- SQL Server
- ADO.NET
- Git
- GitHub
- Visual Studio

---

## 📌 Sistem Modülleri

### 🔐 Giriş Sistemi

Özellikler:

- Kullanıcı adı / şifre ile giriş
- Enter tuşu ile giriş
- SQL doğrulaması
- Rol bazlı yönlendirme

---

### 👤 Kullanıcı Yönetimi

Özellikler:

- Kullanıcı ekleme
- Güncelleme
- Silme
- Rol belirleme
- Admin / Kullanıcı ayrımı

Roller:

- Admin
- Kullanıcı

---

### 📦 Ürün Yönetimi

Özellikler:

- Ürün ekleme
- Ürün güncelleme
- Ürün silme
- Ürün arama
- Ürün kodu
- Kategori
- Alış fiyatı
- Satış fiyatı

---

### 🏬 Depo Yönetimi

Özellikler:

- Depo ekleme
- Güncelleme
- Silme
- Adres bilgileri

---

### 📊 Stok Yönetimi

Özellikler:

- Ürün seçimi
- Depo seçimi
- Stok miktarı girişi
- Stok güncelleme
- Stok silme
- Kritik stok görüntüleme
- Aynı ürün aynı depoda varsa stok üzerine ekleme

---

### 🔎 Stok Sorgulama

Özellikler:

- Ürün adına göre arama
- Ürün koduna göre arama
- Depoya göre arama
- Kategoriye göre arama
- Hangi depoda ne kadar ürün olduğunu görüntüleme

---

### 🧾 Satış / Fatura Sistemi

İşleyiş:

1. Müşteri adı girilir
2. Depo seçilir
3. Depoya ait ürünler otomatik gelir
4. Ürün seçilir
5. Adet girilir
6. Toplam otomatik hesaplanır
7. Satış sonrası stok otomatik düşer

Özellikler:

- Fatura oluşturma
- Fatura güncelleme
- Fatura silme
- Otomatik toplam hesaplama
- Stok kontrolü

---

### 📈 Raporlama Sistemi

Sadece Admin erişebilir.

Raporlar:

#### Satış Raporu

- Satılan ürünler
- Müşteriler
- Satış miktarı
- Toplam tutar

#### Kâr / Zarar Raporu

- Toplam maliyet
- Satış tutarı
- Tahmini kâr

#### Müşteri Raporu

- Müşteri bazlı satışlar
- Harcama miktarı

#### Stok Raporu

- Ürün bilgileri
- Depo bilgileri
- Stok miktarları

#### Kritik Stok Raporu

- Kritik seviyedeki ürünler

---

## 🔒 Yetkilendirme Sistemi

| Rol | Yetki |
|------|--------|
| Admin | Tüm ekranlar |
| Kullanıcı | Ürün, Depo, Stok, Fatura, Stok Sorgulama |

Admin dışı kullanıcılar:

- Kullanıcı yönetimi ekranını göremez
- Raporlar ekranını göremez

---

## 🎨 Arayüz Özellikleri

Projede modern bir WinForms görünümü tasarlanmıştır:

- Modern giriş ekranı
- Dashboard menü yapısı
- Sol menü sistemi
- Kart yapıları
- Modern DataGrid tasarımları
- Renkli rapor ekranları
- Kullanıcı dostu arayüz

---

## 🗄 Veritabanı Kurulumu

Veritabanı oluşturma kodları proje içerisinde:

```text
projeninSql Kodları.sql
```

dosyası içerisinde bulunmaktadır.

### Gerekli Programlar

Kurulması gerekenler:

- Visual Studio
- SQL Server
- SQL Server Management Studio (SSMS)

### Kurulum Adımları

1. SQL Server Management Studio (SSMS) açın

2. Databases bölümüne sağ tıklayın:

```text
Databases
↓
New Database
```

3. Veritabanı adını girin:

```text
Erensoft
```

4. OK butonuna basın

5. Oluşan veritabanını seçin

6. Üst menüden:

```text
New Query
```

a basın.

7. Proje içindeki:

```text
projeninSql Kodları.sql
```

dosyasını açın.

8. İçindeki SQL kodlarını kopyalayıp Query ekranına yapıştırın.

9. Execute butonuna basın.

---

### Oluşturulacak Tablolar

- Users
- Items
- WareHouses
- Stocks
- Invoices

---

### SQL Bağlantısı

Örnek:

```csharp
SqlConnection connection =
new SqlConnection(
"Data Source=.;Initial Catalog=Erensoft;Integrated Security=True");
```

Eğer SQL Server adı farklıysa:

Örnek:

```text
Data Source=DESKTOP-ABC123
```

veya:

```text
Data Source=.\SQLEXPRESS
```

olarak düzenlenmelidir.

---

### İlk Admin Kullanıcısı

Eğer veritabanında kullanıcı yoksa:

```sql
INSERT INTO Users(UserName,Password,Role)
VALUES('admin','1234','Admin')
```

Giriş:

```text
Kullanıcı Adı: admin
Şifre: 1234
```

---

## ▶ Projeyi Çalıştırma

```bash
git clone https://github.com/erentutkun/StokTakipProgrami.git
```

Sonrasında:

1. Visual Studio ile açın
2. Veritabanını oluşturun
3. Connection String düzenleyin
4. Projeyi çalıştırın


## 👨‍💻 Geliştirici

**Abdullah Eren Tutkun**

---

## 📌 Not

Bu proje eğitim, geliştirme ve portföy amacıyla hazırlanmıştır.
